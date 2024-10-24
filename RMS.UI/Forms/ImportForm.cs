using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.VisualBasic.FileIO;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ImportForm : XtraForm
    {
        /// <summary>
        /// Информация о файле.
        /// </summary>
        public class FileInformation
        {
            /// <summary>
            /// Получение информации о файле.
            /// </summary>
            /// <param name="fullPathToFile">Полный путь к файлу.</param>
            public FileInformation(string fullPathToFile)
            {
                if (string.IsNullOrWhiteSpace(fullPathToFile))
                {
                    throw new ArgumentNullException(nameof(fullPathToFile), "Путь к файлу не может быть пустым.");
                }

                if (!System.IO.File.Exists(fullPathToFile))
                {
                    throw new ArgumentNullException(nameof(fullPathToFile), "Файл по заданному пути не существует.");
                }

                FileExtension = GetFileExtension(fullPathToFile);
                FileEncoding = GetFileEncoding(fullPathToFile);
                FullPathToFile = fullPathToFile;
                PathToFile = Path.GetDirectoryName(fullPathToFile);
                FullFileName = Path.GetFileName(fullPathToFile);
                FileName = Path.GetFileNameWithoutExtension(fullPathToFile);
                FileCreationDate = System.IO.File.GetCreationTime(fullPathToFile);
            }

            private Encoding GetFileEncoding(string fullPathToFile)
            {
                if (string.IsNullOrWhiteSpace(fullPathToFile))
                {
                    throw new ArgumentNullException(nameof(fullPathToFile), "Путь к файлу не может быть пустым.");
                }

                var encoding = default(Encoding);

                var tempFile = Path.GetTempFileName();
                System.IO.File.Copy(fullPathToFile, tempFile, true);

                using (var fileStream = new FileStream(tempFile, FileMode.Open))
                {
                    using (var streaReader = new StreamReader(fileStream, true))
                    {
                        encoding = streaReader.CurrentEncoding;
                    }
                }

                System.IO.File.Delete(tempFile);
                return encoding;
            }

            /// <summary>
            /// Получить расширение из перечисления.
            /// </summary>
            /// <param name="fullPathToFile">Полный путь к файл.</param>
            /// <returns>Объект FileExtension.</returns>
            private static FileExtension GetFileExtension(string fullPathToFile)
            {
                if (string.IsNullOrWhiteSpace(fullPathToFile))
                {
                    throw new ArgumentNullException(nameof(fullPathToFile), "Путь к файлу не может быть пустым.");
                }

                var fileExtension = Path.GetExtension(fullPathToFile);

                if (string.IsNullOrWhiteSpace(fileExtension))
                {
                    throw new ArgumentNullException(nameof(fileExtension), "Файл не имеет расширения.");
                }

                foreach (FileExtension item in Enum.GetValues(typeof(FileExtension)))
                {
                    if (item.GetEnumDescription().Equals(fileExtension))
                    {
                        return item;
                    }
                }

                throw new Exception($"Перечислитель {nameof(FileExtension)} не содержит текущего расширения. Обратитесь к разработчикам ПО.");
            }

            /// <summary>
            /// Полный путь к файлу [Пример: С:\test\text.txt].
            /// </summary>
            public string FullPathToFile { get; }

            /// <summary>
            /// Путь до каталога с файлом [Пример: С:\test].
            /// </summary>
            public string PathToFile { get; }

            /// <summary>
            /// Полное имя файла [Пример: text.txt].
            /// </summary>
            public string FullFileName { get; }

            /// <summary>
            /// Имя файла [Пример: text].
            /// </summary>
            public string FileName { get; }

            /// <summary>
            /// Расширение файла [Пример: .txt].
            /// </summary>
            public FileExtension FileExtension { get; }

            /// <summary>
            /// Дата создания файла.
            /// </summary>
            public DateTime FileCreationDate { get; set; }

            /// <summary>
            /// Кодировка файла.
            /// </summary>
            public Encoding FileEncoding { get; }
        }

        /// <summary>
        /// Считанный объект.
        /// </summary>
        public class ReadObject
        {
            public ReadObject()
            {
                ListObject = new List<object[]>();
            }

            /// <summary>
            /// Добавление считываемого объекта в список.
            /// </summary>
            /// <param name="readerObj"></param>
            public void AddObjectToList(object[] readerObj)
            {
                if (readerObj.Length <= 0)
                {
                    return;
                }

                var obj = new object[readerObj.Length];

                if (readerObj.Length > valueCount)
                {
                    valueCount = readerObj.Length;
                }

                for (int i = 0; i < readerObj.Length; i++)
                {
                    obj[i] = readerObj[i];
                }

                ListObject.Add(obj);
            }

            private int valueCount;
            /// <summary>
            /// Количество значений в объекте.
            /// </summary>
            public int ValueCount
            {
                get
                {
                    return valueCount;
                }
            }

            public List<object[]> ListObject { get; }
        }

        private Session Session { get; }

        public ImportForm(Session session)
        {
            InitializeComponent();
            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        private static OleDbConnection OleDbConnectionFoxPro { get; set; }
        private static OleDbConnection OleDbConnectionExcel { get; set; }

        private void txt_PathToDir_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var filter = "Книга Excel (*.xlsx)|*.xlsx|" +
                        "Книга Excel 97-2003 (*.xls)|*.xls|" +
                        "CSV*|*.csv|" +
                        "Все файлы|*.*";

            using (var dlg = new OpenFileDialog() { Filter = filter })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = dlg.FileName;
                }
            }
        }

        private void btn_StartImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                XtraMessageBox.Show("Для начала импорта укажите путь к файлу.", "Не указан путь к файлу", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPath.Focus();
                return;
            }

            var fileInformation = new FileInformation(txtPath.Text);
            var readObject = default(ReadObject);

            switch (fileInformation.FileExtension)
            {
                case FileExtension.CSV:

                    readObject = GetReadObjectCSV(fileInformation, Encoding.GetEncoding(1251));
                    break;

                case FileExtension.XLS:
                    //readObject = GetReadObjectXLS(fileInformation);

                    if (!ConnectionExcel(txtPath.Text))
                    {
                        return;
                    }

                    System.Threading.Tasks.Task.Run(() => ImportExcelKazna(GetTableName(txtPath.Text)));
                    return;

                case FileExtension.XLSX:

                    if (!ConnectionExcel(txtPath.Text))
                    {
                        return;
                    }

                    System.Threading.Tasks.Task.Run(() => ImportExcelKazna(GetTableName(txtPath.Text)));
                    return;

                default:
                    throw new Exception("Выбранный вами формат файла не поддерживается для импорта.");
            }

            System.Threading.Tasks.Task.Run(() => ImportCustomer(readObject));
        }

        /// <summary>
        /// Импорт клиентов в БД RMS.
        /// </summary>
        /// <param name="readObject">Список считанных объектов.</param>
        private void ImportCustomer(ReadObject readObject)
        {
            Invoke((Action)delegate { progressBarControl.Visible = true; });

            var customerList = readObject.ListObject.Where(r => r.Length == readObject.ValueCount).ToList();

            WriteToMemoEdit(Convert.ToInt32(customerList.Count()));
            WriteToMemoEdit($"\n->> Для обработки доступно: {customerList.Count()} <<-\n");

            foreach (var obj in customerList)
            {
                var customerName = obj[0]?.ToString().Trim() ?? string.Empty;
                var customerAccountResponsible = GetSurname(obj[1]?.ToString().Trim() ?? string.Empty);
                var customerTaxSystem = obj[2]?.ToString().Trim() ?? string.Empty;
                var customerformCorporation = obj[3]?.ToString().Trim() ?? string.Empty;
                var customerInn = obj[6]?.ToString().Trim() ?? string.Empty;
                var customerPrimaryResponsible = GetSurname(obj[7]?.ToString().Trim() ?? string.Empty);
                _ = obj[8]?.ToString().Trim() ?? string.Empty;
                var customerContract = obj[9]?.ToString().Trim() ?? string.Empty;
                var customerNotes = GetNotes(obj[4], obj[5], obj[9]);

                var accountantResponsible = Session.FindObject<Staff>(new BinaryOperator(nameof(Staff.Surname), customerAccountResponsible));
                if (accountantResponsible is null)
                {
                    accountantResponsible = new Staff(Session)
                    {
                        Surname = customerAccountResponsible
                    };
                    accountantResponsible.Save();
                }

                var taxSystem = default(TaxSystem);
                if (!string.IsNullOrWhiteSpace(customerTaxSystem))
                {
                    taxSystem = Session.FindObject<TaxSystem>(new BinaryOperator(nameof(TaxSystem.Name), customerTaxSystem));
                    if (taxSystem is null)
                    {
                        taxSystem = new TaxSystem(Session)
                        {
                            Name = customerTaxSystem,
                            Description = customerTaxSystem
                        };
                        taxSystem.Save();
                    }
                }

                var formCorporation = default(FormCorporation);
                if (!string.IsNullOrWhiteSpace(customerformCorporation))
                {
                    formCorporation = Session.FindObject<FormCorporation>(new BinaryOperator(nameof(FormCorporation.AbbreviatedName), customerformCorporation));
                    if (formCorporation is null)
                    {
                        formCorporation = new FormCorporation(Session)
                        {
                            AbbreviatedName = customerformCorporation
                        };
                        formCorporation.Save();
                    }
                }

                var primaryResponsible = default(Staff);

                if (!string.IsNullOrWhiteSpace(customerPrimaryResponsible))
                {
                    primaryResponsible = Session.FindObject<Staff>(new BinaryOperator(nameof(Staff.Surname), customerPrimaryResponsible));

                    if (primaryResponsible is null)
                    {
                        primaryResponsible = new Staff(Session)
                        {
                            Surname = customerPrimaryResponsible
                        };
                        primaryResponsible.Save();
                    }
                }

                var groupCriteriaCustomer = new GroupOperator(GroupOperatorType.Or);

                if (!string.IsNullOrWhiteSpace(customerInn))
                {
                    var criteriaCusimerInn = new BinaryOperator(nameof(Customer.INN), customerInn);
                    groupCriteriaCustomer.Operands.Add(criteriaCusimerInn);
                }
                else if (!string.IsNullOrWhiteSpace(customerName))
                {
                    var criteriaCusimerName = new BinaryOperator(nameof(Customer.Name), customerName);
                    groupCriteriaCustomer.Operands.Add(criteriaCusimerName);
                }
                else
                {
                    continue;
                }

                var customer = Session.FindObject<Customer>(groupCriteriaCustomer);

                if (customer is null)
                {
                    customer = new Customer(Session)
                    {
                        Name = customerName,
                        AccountantResponsible = accountantResponsible,
                        AccountantResponsibleDate = (accountantResponsible is null) ? default : DateTime.Now.Date,
                        INN = customerInn,
                        FormCorporation = formCorporation,
                        PrimaryResponsible = primaryResponsible,
                        PrimaryResponsibleDate = (primaryResponsible is null) ? default : DateTime.Now.Date,
                        Notes = customerNotes,
                        //StateDate = DateTime.Now.Date,
                        DateActuality = DateTime.Now.Date,
                        //Contract = new Contract(Session)
                        //{
                        //    Number = customerContract,
                        //    Date = GetContractDate(customerContract)
                        //},
                        TaxSystemCustomer = new TaxSystemCustomer(Session)
                        {
                            //Date = DateTime.Now,
                            //CurrentTaxSystem = taxSystem
                        }
                    };

                    customer.Contracts.Add(new Contract(Session)
                    {
                        Number = customerContract,
                        Date = GetContractDate(customerContract)
                    });

                    customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.IMPORT_FROM_EXCEL,
                        Date = DateTime.Now,
                        Description = Act.IMPORT_FROM_EXCEL.GetEnumDescription(),
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
                customer.Save();

                StepProgressBar();
            }

            Invoke((Action)delegate { progressBarControl.Visible = false; });

            XtraMessageBox.Show(String.Format("Импорт окончен!"), "Окончание импорта", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Получение даты договора из строки.
        /// </summary>
        /// <param name="customerContract">Строка в которой может находиться дата.</param>
        /// <returns>Дата контракта.</returns>
        private DateTime? GetContractDate(string customerContract)
        {
            var contractDate = default(DateTime?);

            try
            {
                Match match = Regex.Match(customerContract, @"\d\d[.]\d\d[.]\d\d\d\d");
                contractDate = Convert.ToDateTime(match.Captures[0].Value);
            }
            catch (Exception) { }

            return contractDate;
        }

        /// <summary>
        /// Получение заметок.
        /// </summary>
        /// <param name="mark">Метки.</param>
        /// <param name="advanceAndSalary">Дата аванса и ЗП.</param>
        /// <param name="taxSystem">Смена системы Н/О.</param>
        /// <returns>Заметки.</returns>
        private string GetNotes(object mark, object advanceAndSalary, object taxSystem)
        {
            var result = default(string);
            var markString = mark?.ToString().Trim() ?? string.Empty;
            var advanceAndSalaryString = advanceAndSalary?.ToString().Trim() ?? string.Empty;
            var taxSystemString = taxSystem?.ToString().Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(markString))
            {
                result += $"Метки: {markString}{Environment.NewLine}";
            }

            if (!string.IsNullOrWhiteSpace(advanceAndSalaryString))
            {
                result += $"Дата аванса и ЗП: {advanceAndSalaryString}{Environment.NewLine}";
            }

            if (!string.IsNullOrWhiteSpace(taxSystemString))
            {
                result += $"Смена системы Н/О: {taxSystemString}{Environment.NewLine}";
            }

            return result;
        }

        /// <summary>
        /// Возвращает строку до проблема [Например: 'Иванов Иван' вернет Иванов].
        /// </summary>
        /// <param name="customerAccountResponsible">Строка с ФИО.</param>
        /// <returns>Первый элемент Split().</returns>
        private string GetSurname(string customerAccountResponsible)
        {
            if (string.IsNullOrWhiteSpace(customerAccountResponsible))
            {
                return string.Empty;
            }

            return customerAccountResponsible.Split()[0];
        }

        /// <summary>
        /// Считывание файла CSV.
        /// </summary>
        /// <param name="fileInformation">Информация о файле.</param>
        /// <param name="encoding">Кодировка.</param>
        /// <returns>Считанный объект [ReadObject].</returns>
        private ReadObject GetReadObjectCSV(FileInformation fileInformation, Encoding encoding)
        {
            using (TextFieldParser parser = new TextFieldParser(fileInformation.FullPathToFile, encoding))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                var readObject = new ReadObject();
                bool firstLine = true;

                while (!parser.EndOfData)
                {
                    object[] fields = parser.ReadFields();

                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    readObject.AddObjectToList(fields);
                }
                return readObject;
            }
        }

        /// <summary>
        /// Считывание файла XLS.
        /// </summary>
        /// <param name="fileInformation">Информация о файле.</param>
        /// <returns>Считанный объект [ReadObject].</returns>
        private ReadObject GetReadObjectXLS(FileInformation fileInformation)
        {
            throw new NotImplementedException();
        }

        private static bool ConnectionFoxPro(string path)
        {
            string connectionstring = string.Empty;
            try
            {
                connectionstring = String.Format(@"Provider=Microsoft OLE DB Provider for Visual FoxPro;Data Source={0}", path);
                OleDbConnectionFoxPro = new OleDbConnection(connectionstring);
                OleDbConnectionFoxPro.Open();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool ConnectionExcel(string path)
        {
            string connectionstring = string.Empty;
            var expansionFile = Path.GetExtension(path);
            try
            {
                if (expansionFile == ".xls")
                {
                    connectionstring = $"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = '{path}'; Extended Properties=\"Excel 8.0;HDR=YES;\"";
                }
                if (expansionFile == ".xlsx")
                {
                    connectionstring = $"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = '{path}'; Extended Properties=\"Excel 12.0;HDR=YES;\"";
                }

                OleDbConnectionExcel = new OleDbConnection(connectionstring);
                OleDbConnectionExcel.Open();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private string GetTableName(string strPath)
        {
            Microsoft.Office.Interop.Excel.Application ExcelObj = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                WriteToMemoEdit("\n->> Проверка файла Excel <<-\n");

                WriteToMemoEdit($"\n->> Путь до файла: {strPath} <<-\n");

                Microsoft.Office.Interop.Excel.Workbook theWorkbook = null;

                theWorkbook = ExcelObj.Workbooks.Open($"{strPath}", Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Microsoft.Office.Interop.Excel.Sheets sheets = theWorkbook.Worksheets;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(spinEditListNumber.Value);

                var worksheetName = worksheet.Name;

                WriteToMemoEdit($"\n->> Рабочий лист: {worksheetName} <<-\n");

                theWorkbook.Close(true);

                if (string.IsNullOrWhiteSpace(worksheetName))
                {
                    return default;
                }
                return worksheetName;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                ExcelObj.Quit();
            }
        }

        private async void ImportExcelKazna(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return;
            }

            Invoke((Action)delegate { progressBarControl.Visible = true; });

            var sql = $"SELECT * FROM [{tableName}$]";
            var count = $"SELECT COUNT(*) FROM [{tableName}$]";

            if (checkEditCellRange.Checked == true)
            {
                sql = $"SELECT * FROM [{tableName}${txtCellRangeSince.Text}:{txtCellRangeTo.Text}]";
                count = $"SELECT COUNT(*) FROM [{tableName}${txtCellRangeSince.Text}:{txtCellRangeTo.Text}]";
                WriteToMemoEdit($"\n->> Заданный диапазон поиска [{txtCellRangeSince.Text}:{txtCellRangeTo.Text}] <<-\n");
            }

            using (var command = new OleDbCommand { Connection = OleDbConnectionExcel, CommandText = count })
            {
                var rowCount = await command.ExecuteScalarAsync();
                WriteToMemoEdit(Convert.ToInt32(rowCount));
                WriteToMemoEdit($"\n->> Для обработки доступно: {rowCount} <<-\n");
            }

            using (var command = new OleDbCommand { Connection = OleDbConnectionExcel, CommandText = sql })
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var customerColection = new XPCollection<Customer>(Session);

                    while (await reader.ReadAsync())
                    {
                        var inn = reader[6].ToString().Trim();

                        var taxSystem = default(TaxSystem);
                        if (!string.IsNullOrWhiteSpace(reader[2].ToString()))
                        {
                            taxSystem = Session.FindObject<TaxSystem>(new BinaryOperator(nameof(TaxSystem.Name), reader[2].ToString()));
                            if (taxSystem is null)
                            {
                                taxSystem = new TaxSystem(Session)
                                {
                                    Name = reader[2].ToString().Trim(),
                                    Description = reader[2].ToString().Trim()
                                };
                                taxSystem.Save();
                            }
                        }

                        var formCorporation = default(FormCorporation);
                        if (!string.IsNullOrWhiteSpace(reader[3].ToString()))
                        {
                            formCorporation = Session.FindObject<FormCorporation>(new BinaryOperator(nameof(FormCorporation.AbbreviatedName), reader[3].ToString()));
                            if (formCorporation is null)
                            {
                                formCorporation = new FormCorporation(Session)
                                {
                                    AbbreviatedName = reader[3].ToString().Trim()
                                };
                                formCorporation.Save();
                            }
                        }

                        var customer = customerColection.Where(w => w.INN.Equals(inn)).FirstOrDefault();

                        var surnameAccountResponsible = reader[1].ToString().Trim().Split();
                        var accountantResponsible = Session.FindObject<Staff>(new BinaryOperator(nameof(Staff.Surname), surnameAccountResponsible[0]));
                        if (accountantResponsible is null)
                        {
                            accountantResponsible = new Staff(Session)
                            {
                                Surname = surnameAccountResponsible[0]
                            };

                            if (surnameAccountResponsible[1] != null)
                            {
                                accountantResponsible.Name = surnameAccountResponsible[1].Trim();
                            }
                            accountantResponsible.Save();
                        }

                        var surnamePrimaryResponsible = reader[7].ToString().Trim().Split();
                        var primaryResponsible = default(Staff);

                        if (surnamePrimaryResponsible[0] != null && !string.IsNullOrWhiteSpace(surnamePrimaryResponsible[0]))
                        {
                            primaryResponsible = Session.FindObject<Staff>(new BinaryOperator(nameof(Staff.Surname), surnamePrimaryResponsible[0]));

                            if (primaryResponsible is null)
                            {
                                primaryResponsible = new Staff(Session)
                                {
                                    Surname = surnamePrimaryResponsible[0].Trim()
                                };
                                primaryResponsible.Save();
                            }
                        }

                        var contractDate = default(DateTime?);

                        try
                        {
                            Match match = Regex.Match(reader[9].ToString().Trim(), @"\d\d[.]\d\d[.]\d\d\d\d");
                            contractDate = Convert.ToDateTime(match.Captures[0].Value);
                        }
                        catch (Exception) { }

                        var contact = new Contract(Session) { Number = reader[9].ToString().Trim(), Date = contractDate };

                        if (reader[9].ToString().Trim().Contains("нет") || string.IsNullOrWhiteSpace(reader[9].ToString()))
                        {
                            contact = null;
                        }

                        if (customer is null)
                        {
                            customer = new Customer(Session)
                            {
                                Name = reader[0].ToString(),
                                AccountantResponsible = accountantResponsible,
                                INN = inn,
                                FormCorporation = formCorporation,
                                PrimaryResponsible = primaryResponsible,
                                //Contract = contact,                                
                                Notes = $"Метки: {reader[3].ToString().Trim()}{Environment.NewLine}" +
                                        $"Дата аванса и ЗП: {reader[4].ToString().Trim()}{Environment.NewLine}" +
                                        $"Смена системы Н/О: {reader[8].ToString().Trim()}",
                                //StateDate = DateTime.Now.Date,
                                DateActuality = DateTime.Now.Date,
                                TaxSystemCustomer = new TaxSystemCustomer(Session)
                                {
                                    //Date = DateTime.Now,
                                    //CurrentTaxSystem = taxSystem
                                }
                            };

                            customer.Contracts.Add(contact);

                            if (accountantResponsible != null)
                            {
                                customer.AccountantResponsibleDate = DateTime.Now.Date;
                            }

                            if (primaryResponsible != null)
                            {
                                customer.PrimaryResponsibleDate = DateTime.Now.Date;
                            }

                            customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                            {
                                Act = Act.IMPORT_FROM_EXCEL,
                                Date = DateTime.Now,
                                Description = Act.IMPORT_FROM_EXCEL.GetEnumDescription(),
                                User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                            });

                            customer.Save();
                        }
                        else
                        {
                            break;
                        }

                        StepProgressBar();
                    }
                }
            }

            Invoke((Action)delegate { progressBarControl.Visible = false; });

            XtraMessageBox.Show(String.Format("Импорт окончен!"), "Окончание импорта", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void WriteToMemoEdit(string msg)
        {
            try
            {
                if (!IsHandleCreated)
                {
                    return;
                }

                Invoke((Action)delegate
                {
                    txtLogger.Text += msg + Environment.NewLine;
                    txtLogger.SelectionStart = Int32.MaxValue;
                    txtLogger.ScrollToCaret();
                });
            }
            catch (Exception)
            { }
        }

        private void WriteToMemoEdit(int allRecord)
        {
            try
            {
                if (!IsHandleCreated)
                {
                    return;
                }

                Invoke((Action)delegate
                {
                    lbl_Out.Text = $"Всего записей: {allRecord}";
                    progressBarControl.EditValue = null;
                    progressBarControl.Properties.Step = 1;
                    progressBarControl.Properties.PercentView = true;
                    progressBarControl.Properties.Maximum = allRecord;
                    progressBarControl.Properties.Minimum = 0;
                });
            }
            catch (Exception)
            { }
        }

        private void StepProgressBar()
        {
            try
            {
                if (!IsHandleCreated)
                {
                    return;
                }

                Invoke((Action)delegate
                {
                    progressBarControl.PerformStep();
                    progressBarControl.Update();
                });
            }
            catch (Exception)
            { }
        }

        private void rgOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radioGroup = sender as RadioGroup;

            if (radioGroup.SelectedIndex == 0)
            {
                groupControlSettings.Visible = false;
            }

            if (radioGroup.SelectedIndex == 1)
            {
                groupControlSettings.Visible = true;
                spinEditListNumber.Value = 2;

            }

            if (radioGroup.SelectedIndex == 2)
            {
                groupControlSettings.Visible = true;
                spinEditListNumber.Value = 1;
            }

            if (radioGroup.SelectedIndex == 3)
            {
                groupControlSettings.Visible = true;
                spinEditListNumber.Value = 11;
            }
        }

        private void checkEditCellRange_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditCellRange.Checked == true)
            {
                txtCellRangeTo.Visible = true;
                txtCellRangeSince.Visible = true;
            }
            else
            {
                txtCellRangeTo.Visible = false;
                txtCellRangeSince.Visible = false;
            }
        }
    }
}