using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

namespace RMS.Core.Model.Mail
{
    public class IdleMailClient : IDisposable
    {
		readonly string host, username, password;
		readonly SecureSocketOptions sslOptions;
		readonly int port;
		List<IMessageSummary> messages;
		CancellationTokenSource cancel;
		CancellationTokenSource done;
		bool messagesArrived;
		ImapClient client;

		public IdleMailClient(string host, int port, SecureSocketOptions sslOptions, string username, string password)
		{
			this.client = new ImapClient(new ProtocolLogger(Console.OpenStandardError()));
			this.messages = new List<IMessageSummary>();
			this.cancel = new CancellationTokenSource();
			this.sslOptions = sslOptions;
			this.username = username;
			this.password = password;
			this.host = host;
			this.port = port;
		}

		async System.Threading.Tasks.Task ReconnectAsync()
		{
			if (!client.IsConnected)
				await client.ConnectAsync(host, port, sslOptions, cancel.Token);

			if (!client.IsAuthenticated)
			{
				await client.AuthenticateAsync(username, password, cancel.Token);

				await client.Inbox.OpenAsync(FolderAccess.ReadOnly, cancel.Token);
			}
		}

		async System.Threading.Tasks.Task FetchMessageSummariesAsync(bool print)
		{
			IList<IMessageSummary> fetched = null;

			do
			{
				try
				{
					int startIndex = messages.Count;

					fetched = client.Inbox.Fetch(startIndex, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId, cancel.Token);
					break;
				}
				catch (ImapProtocolException)
				{
					await ReconnectAsync();
				}
				catch (IOException)
				{
					await ReconnectAsync();
				}
			} while (true);

			foreach (var message in fetched)
			{
				if (print)
					Console.WriteLine("{0}: new message: {1}", client.Inbox, message.Envelope.Subject);
				messages.Add(message);
			}
		}

		async System.Threading.Tasks.Task WaitForNewMessagesAsync()
		{
			do
			{
				try
				{
					if (client.Capabilities.HasFlag(ImapCapabilities.Idle))
					{
						done = new CancellationTokenSource(new TimeSpan(0, 9, 0));
						try
						{
							await client.IdleAsync(done.Token, cancel.Token);
						}
						finally
						{
							done.Dispose();
							done = null;
						}
					}
					else
					{
						await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 1, 0), cancel.Token);
						await client.NoOpAsync(cancel.Token);
					}
					break;
				}
				catch (ImapProtocolException)
				{
					await ReconnectAsync();
				}
				catch (IOException)
				{
					await ReconnectAsync();
				}
			} while (true);
		}

		async System.Threading.Tasks.Task IdleAsync()
		{
			do
			{
				try
				{
					await WaitForNewMessagesAsync();

					if (messagesArrived)
					{
						await FetchMessageSummariesAsync(true);
						messagesArrived = false;
					}
				}
				catch (OperationCanceledException)
				{
					break;
				}
			} while (!cancel.IsCancellationRequested);
		}

		public async System.Threading.Tasks.Task RunAsync()
		{
			try
			{
				await ReconnectAsync();
				await FetchMessageSummariesAsync(false);
			}
			catch (OperationCanceledException)
			{
				await client.DisconnectAsync(true);
				return;
			}
			
			var inbox = client.Inbox;
			inbox.CountChanged += OnCountChanged;
			inbox.MessageExpunged += OnMessageExpunged;
			inbox.MessageFlagsChanged += OnMessageFlagsChanged;

			await IdleAsync();

			inbox.MessageFlagsChanged -= OnMessageFlagsChanged;
			inbox.MessageExpunged -= OnMessageExpunged;
			inbox.CountChanged -= OnCountChanged;

			await client.DisconnectAsync(true);
		}
		
		void OnCountChanged(object sender, EventArgs e)
		{
			var folder = (ImapFolder)sender;
			if (folder.Count > messages.Count)
			{
				int arrived = folder.Count - messages.Count;

				if (arrived > 1)
					Console.WriteLine("\t{0} new messages have arrived.", arrived);
				else
					Console.WriteLine("\t1 new message has arrived.");
				messagesArrived = true;
				done?.Cancel();
			}
		}

		void OnMessageExpunged(object sender, MessageEventArgs e)
		{
			var folder = (ImapFolder)sender;

			if (e.Index < messages.Count)
			{
				var message = messages[e.Index];

				Console.WriteLine("{0}: message #{1} has been expunged: {2}", folder, e.Index, message.Envelope.Subject);
				messages.RemoveAt(e.Index);
			}
			else
			{
				Console.WriteLine("{0}: message #{1} has been expunged.", folder, e.Index);
			}
		}

		void OnMessageFlagsChanged(object sender, MessageFlagsChangedEventArgs e)
		{
			var folder = (ImapFolder)sender;

			Console.WriteLine("{0}: flags have changed for message #{1} ({2}).", folder, e.Index, e.Flags);
		}

		public void Exit()
		{
			cancel.Cancel();
		}

		public void Dispose()
		{
			client.Dispose();
			cancel.Dispose();
		}
	}
}

