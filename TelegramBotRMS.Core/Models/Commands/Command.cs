using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotRMS.Core.Models.Commands
{
    public abstract class Command
    {
        public abstract bool IsCallbackCommand { get; }        
        public abstract string[] Names { get; }
        public abstract string Text { get; }
        
        public virtual InlineKeyboardMarkup InlineKeyboardMarkup { get; }
        
        public virtual ReplyKeyboardMarkup ReplyKeyboardMarkupStaff 
        {
            get
            {
                var buttons = new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("📑 Мои задачи"),
                        new KeyboardButton("⚡ Мои задачи (сегодня)")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton("🤝 Мои сделки"),
                        new KeyboardButton("🌟 Список контроля")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton(@"📨 Электронная отчетность"),
                        new KeyboardButton(@"📃 Патенты")
                    },
                    
                    new KeyboardButton[]
                    {
                        new KeyboardButton(@"🤼 Мои клиенты")
                    }
                };

                var keyboardMarkup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
                    
                return keyboardMarkup;
            } 
        }

        public virtual ReplyKeyboardMarkup ReplyKeyboardMarkupCustomer
        {
            get
            {
                var buttons = new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("🏡 Мои организации")
                    }
                };

                var keyboardMarkup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                return keyboardMarkup;
            }
        }

        public CallbackQuery CallbackQuery { get; private set; }
        public virtual int? MessageOid { get; private set; }

        public abstract Task Execute(Message message, TelegramBotClient telegramBotClient);
        public virtual Task ExecuteCallback(Message message, TelegramBotClient telegramBotClient)
        {
            return default;
        }

        public void GetCallbackQuery(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery;
        }

        public void GetCallbackMessage(int messageOid)
        {
            MessageOid = messageOid;
        }

        public virtual bool Contains(string command)
        {
            var result = false;
            foreach (var name in Names)
            {
                if (command.Contains(name))
                {
                    result = true;
                    break;
                }
            }
            
            return result;
        }
        
        public virtual bool Contains(Message message)
        {
            var result = false;

            if (message.Type != MessageType.Text)
            {
                return false;
            }

            foreach (var name in Names)
            {
                if (message.Text.Contains(name))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        
        public virtual bool Contains(CallbackQuery callbackQuery)
        {
            var result = false;

            foreach (var name in Names)
            {
                if (callbackQuery.Data.Contains(name))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static IReplyMarkup GetInlineKeyboardMarkup(IDictionary<string, string> buttonList, int columns = -1)
        {
            var count = buttonList.Count;

            //TODO: сделать универсально, чтобы выводилось либо с переключателем, либо все.
            if (count > 120)
            {
                count = 60;
            }

            if (columns == -1)
            {
                if (count <= 10)
                {
                    columns = 2;
                }
                else
                {
                    columns = 3;
                }
            }

            var rows = (int)Math.Ceiling((double)count / (double)columns);
            var buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = buttonList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData($"{direction.Value}", $"{direction.Key}"))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }
    }
}
