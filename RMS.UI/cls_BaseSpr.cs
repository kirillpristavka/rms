using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.Core.Model.Accounts;
using RMS.Core.Model.Calculator;
using RMS.Core.Model.CourierService;
using RMS.Core.Model.ElectronicDocumentsManagement;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoContract.ContractAttachments;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.InfoCustomer.Billing;
using RMS.Core.Model.Mail;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.PackagesDocument;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.Setting.Model.ColorSettings;
using RMS.Setting.Model.CustomerSettings;
using RMS.UI.Control.Customers;
using RMS.UI.Forms.Calculator;
using RMS.UI.Forms.CourierService.v1;
using RMS.UI.Forms.CourierService.v2;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Mail;
using RMS.UI.Forms.ReferenceBooks;
using RMS.UI.Forms.Salary;
using RMS.UI.Forms.Vacations;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI
{
    public class set_SprShablonView : XPObject
    {
        public set_SprShablonView() { }
        public set_SprShablonView(Session session) : base(session) { }

        public string g_id { get; set; }
        public int Task { get; set; }                      // Номер задачи (1-Отчетность, 2-Имущество, ...) нужен для обновления с серверной стороны
        public int SprVariant { get; set; }               // Enum cls_App.SprVariants (у каждой Task - свой)
        public int NumVar { get; set; }                   // Номер варианта представления (может быть несколько для различных случаев) default = 1
        public string Soder { get; set; }                // Описание представления (что это (какая таблица) и место вызова)
        public string SprText { get; set; }                // Caption справочника
        public bool IsFormEnumeration { get; set; }        // Форма редактирования "formEdit_BaseSprEnumeration" (определяем таблицу заполнения полей, true - set_SprShablonViewEnumeration, false - set_SprShablonViewDetail)
        public bool EditButtons { get; set; }              // Активность кнопок добавления/редактирования/копирования/удаления

        public string FieldGuid { get; set; }              // поле "g_id"
        public string FieldDef { get; set; }               // поле "fl_def"
        public string FieldId { get; set; }                // Поле "Oid"
        public string FieldComment { get; set; }           // Поле Комментария
        public string FieldImage { get; set; }             // Поле "ImageIndex"

        public int SizeW { get; set; }                     // Ширина справочника (если 0 - не изменять)
        public int SizeH { get; set; }                     // Высота справочника (если 0 - не изменять)

        public string SortField1 { get; set; }             // Поле сортировки 1
        public string SortField2 { get; set; }            // Поле сортировки 2
        public string SortField3 { get; set; }            // Поле сортировки 3

        public bool ModifyUsers { get; set; }              // доступность полей "user_create" "user_update"
        public bool ModifyDates { get; set; }             // доступность полей "date_create" "date_update"
    }

    public class set_SprShablonViewEnumeration : XPObject
    {
        public set_SprShablonViewEnumeration() { }
        public set_SprShablonViewEnumeration(Session session) : base(session) { }

        public string root_g_id { get; set; }
        //public string FieldGuid;              // поле "g_id"
        public string FieldKod { get; set; }               // поле Код
        public string FieldKodCaption { get; set; }       // поле Код Caption
        public bool FlKodIsInt32 { get; set; }            // поле Код является числом
        public string FieldKodMaska { get; set; }          // поле Код Mask.EditMask
        public string FieldName { get; set; }             // поле Name
        public string FieldNameCaption { get; set; }      // поле Name Caption
        public string FieldSoder { get; set; }           // поле Soder
        public string FieldSoderCaption { get; set; }     // поле Soder Caption
        public string FieldItog { get; set; }              // поле "fl_itog" ("calc_mark")
        public string FieldItogCaption { get; set; }       // поле "fl_itog" Caption

        public string FieldGroupI1 { get; set; }          // поле Группы 1 (int "Group") обязательно у "set_SprEnumeration"
        public string FieldGroupI2 { get; set; }          // поле Группы 2 (int "Year") - не используется пока
        public string FieldGroupS1 { get; set; }          // поле Группы 4 (str "User") - не используется пока
        public string FieldGroupS2 { get; set; }          // поле Группы 5 (str "Org")  - не используется пока
    }

    public class set_SprShablonViewDetail : XPObject
    {
        public set_SprShablonViewDetail() { }
        public set_SprShablonViewDetail(Session session) : base(session) { }

        public string root_g_id { get; set; }

        public int Index { get; set; }                     // Позиция Поля (для сортировки)
        public string Name { get; set; }                  // Поле
        public string Caption { get; set; }                // Название колонки
        public int Width { get; set; }                   // Ширина
        public bool FixedWidth { get; set; }               // Фиксированная ширина, или нет
        public string FormatType { get; set; }             // FormatType (потом преобразовывать в перечисление) DevExpress.Utils.FormatType. ... .ToString()
        public string FormatString { get; set; }          // FormatString  "dd.MM.yyyy"; "d"; "yy.MM.dd  HH:mm";
        public int HAlignment { get; set; }               // DevExpress.Utils.HorzAlignment: Default = 0, Near = 1, Center = 2, Far = 3
        public bool Visible { get; set; }                 // Видима колонка, или скрыта
    }

    public class cls_BaseSpr
    {
        public cls_BaseSpr() { }
        public cls_BaseSpr(int spr_var, int num_var = 1) { Init(null, spr_var, num_var); }
        public cls_BaseSpr(Session sess, int spr_var, int num_var = 1) { Init(sess, spr_var, num_var); }

        private cls_App.ReferenceBooks _SprVaiant;
        private Type _TypeSpr;

        private set_SprShablonView _SprShablonView;
        private set_SprShablonViewEnumeration _SprShablonViewEnumeration;
        private XPCollection<set_SprShablonViewDetail> _SprShablonViewDetail;

        public int SprVariant { get { return (int)_SprVaiant; } set { _SprVaiant = (cls_App.ReferenceBooks)value; } }
        public set_SprShablonView SprShablonView { get { return _SprShablonView; } }
        public set_SprShablonViewEnumeration SprShablonViewEnumeration { get { return _SprShablonViewEnumeration; } }
        public Type GetTypeSpr() { return _TypeSpr; }
        public string FieldId { get; set; }
        public string FieldComment { get; set; }
        public string FieldImageIndex { get; set; }

        public DevExpress.Utils.ImageCollection ImageCollection { get; set; }


        public bool FlagSprDateRange { get; set; }
        public bool FlagSprEnumeration { get; set; }


        public int GetSizeW()
        { return (_SprShablonView != null && _SprShablonView.SizeW > 0) ? _SprShablonView.SizeW : 420; }
        public int GetSizeH()
        { return (_SprShablonView != null && _SprShablonView.SizeH > 0) ? _SprShablonView.SizeH : 480; }

        #region FirstFillSprShablonTables

        /// <summary>
        /// Первоначальное заполнение Таблиц для Отображения Справочников
        /// </summary>
        /// <param name="sess">Session</param>
        public static void FirstFillSprShablonTables(Session sess)
        {
            if (sess == null) sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

            string l_g_id = string.Empty;

            // + set_SprShablonView
            if (sess.FindObject<set_SprShablonView>(null) == null)
            {
                // .SprVariants.Spr_Bank
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Bank, NumVar = 1, Soder = "Банки", SprText = "Справочник - Банки", IsFormEnumeration = false, EditButtons = true, FieldId = nameof(Bank.Oid), FieldComment = nameof(Bank.PaymentName), FieldImage = string.Empty, SortField1 = nameof(Bank.Town), SortField2 = nameof(Bank.BIC), SortField3 = string.Empty, SizeW = 700, SizeH = 650 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Bank.BIC), Caption = "БИК", Visible = true, Width = 100, FixedWidth = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Bank.PaymentName), Caption = "Наименование", Visible = true, Width = 425, FixedWidth = false, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(Bank.Town), Caption = "Город", Visible = true, Width = 175, FixedWidth = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 5, Name = nameof(Bank.CorrespondentAccount), Caption = "Корреспондентский счет", Visible = false, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Position), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Position, NumVar = 1, Soder = "Должности", SprText = "Справочник - Должности", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Position.Oid), FieldId = nameof(Position.Oid), FieldComment = nameof(Position.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Position.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Position.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Position.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Document), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Document, NumVar = 1, Soder = "Документы", SprText = "Справочник - Документы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Document.Oid), FieldId = nameof(Document.Oid), FieldComment = nameof(Document.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Document.Name), SortField3 = string.Empty, SizeW = 750, SizeH = 550 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Document.Name), Caption = "Наименование", Width = 250, FixedWidth = true, Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Document.Description), Caption = "Описание", Width = 500, FixedWidth = true, Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CustomerStaff), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CustomerStaff, NumVar = 1, Soder = "Сотрудники клиента", SprText = "Справочник - Сотрудники клиента", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CustomerStaff.Oid), FieldId = nameof(CustomerStaff.Oid), FieldComment = nameof(CustomerStaff.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CustomerStaff.Name), SortField3 = string.Empty, SizeW = 750, SizeH = 550 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CustomerStaff.Surname), Caption = "Фамилия", Visible = true, HAlignment = 0, Width = 200, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CustomerStaff.Name), Caption = "Имя", Visible = true, HAlignment = 0, Width = 200, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(CustomerStaff.Patronymic), Caption = "Отчество", Visible = true, HAlignment = 0, Width = 200, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(CustomerStaff.DateBirth), Caption = "Дата рождения", Visible = true, HAlignment = 2, Width = 125, FixedWidth = true });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.StatusAccrual), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.StatusAccrual, NumVar = 1, Soder = "Статус платежей", SprText = "Справочник - Статус платежей", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Position.Oid), FieldId = nameof(Position.Oid), FieldComment = nameof(Position.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Position.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(StatusAccrual.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(StatusAccrual.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(StatusAccrual.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PayoutDictionary), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PayoutDictionary, NumVar = 1, Soder = "Выплаты и удержания", SprText = "Справочник - Выплаты и удержания", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PayoutDictionary.Oid), FieldId = nameof(PayoutDictionary.Oid), FieldComment = nameof(PayoutDictionary.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PayoutDictionary.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PayoutDictionary.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PayoutDictionary.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.StatutoryDocument), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.StatutoryDocument, NumVar = 1, Soder = "Уставные документы", SprText = "Справочник - Уставные документы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(StatutoryDocument.Oid), FieldId = nameof(StatutoryDocument.Oid), FieldComment = nameof(StatutoryDocument.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(StatutoryDocument.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(StatutoryDocument.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(StatutoryDocument.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TitleDocument), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TitleDocument, NumVar = 1, Soder = "Право устанавливающий документ", SprText = "Справочник - Право устанавливающий документ", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TitleDocument.Oid), FieldId = nameof(TitleDocument.Oid), FieldComment = nameof(TitleDocument.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TitleDocument.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TitleDocument.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TitleDocument.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaxReportingDocument), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaxReportingDocument, NumVar = 1, Soder = " Документы по налоговой отчетности", SprText = "Справочник -  Документы по налоговой отчетности", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaxReportingDocument.Oid), FieldId = nameof(TaxReportingDocument.Oid), FieldComment = nameof(TaxReportingDocument.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TaxReportingDocument.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TaxReportingDocument.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TaxReportingDocument.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.SourceDocument), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.SourceDocument, NumVar = 1, Soder = "Первичные документы", SprText = "Справочник - Первичные документы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(SourceDocument.Oid), FieldId = nameof(SourceDocument.Oid), FieldComment = nameof(SourceDocument.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(SourceDocument.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(SourceDocument.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(SourceDocument.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.EmployeeDetailsDocument), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.EmployeeDetailsDocument, NumVar = 1, Soder = "Анкетные данные сотрудников", SprText = "Справочник - Анкетные данные сотрудников", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(EmployeeDetailsDocument.Oid), FieldId = nameof(EmployeeDetailsDocument.Oid), FieldComment = nameof(EmployeeDetailsDocument.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(EmployeeDetailsDocument.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(EmployeeDetailsDocument.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(EmployeeDetailsDocument.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CustomerFilter), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CustomerFilter, NumVar = 1, Soder = "Фильтры модуля клиенты", SprText = "Справочник - Фильтр модуля клиенты", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CustomerFilter.Oid), FieldId = nameof(CustomerFilter.Oid), FieldComment = nameof(CustomerFilter.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CustomerFilter.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CustomerFilter.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CustomerFilter.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.SectionOKVED2), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.SectionOKVED2, NumVar = 1, Soder = "Раздел ОКВЭД", SprText = "Справочник - Раздел ОКВЭД", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(SectionOKVED2.Oid), FieldId = nameof(SectionOKVED2.Oid), FieldComment = nameof(SectionOKVED2.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(SectionOKVED2.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(SectionOKVED2.Code), Caption = "Код", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(SectionOKVED2.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(SectionOKVED2.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ClassOKVED2), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ClassOKVED2, NumVar = 1, Soder = "Класс ОКВЭД", SprText = "Справочник - Класс ОКВЭД", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ClassOKVED2.Oid), FieldId = nameof(ClassOKVED2.Oid), FieldComment = nameof(ClassOKVED2.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ClassOKVED2.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(SectionOKVED2.Code), Caption = "Код", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(SectionOKVED2.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(SectionOKVED2.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.EntrepreneurialActivityCodesUTII), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.EntrepreneurialActivityCodesUTII, NumVar = 1, Soder = "Коды предпринимательской деятельности ЕНВД", SprText = "Справочник - Коды предпринимательской деятельности ЕНВД", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(EntrepreneurialActivityCodesUTII.Oid), FieldId = nameof(EntrepreneurialActivityCodesUTII.Oid), FieldComment = nameof(EntrepreneurialActivityCodesUTII.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(EntrepreneurialActivityCodesUTII.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(EntrepreneurialActivityCodesUTII.Code), Caption = "Код", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(EntrepreneurialActivityCodesUTII.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.GroupPerformanceIndicator), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.GroupPerformanceIndicator, NumVar = 1, Soder = "Группы показателей работы", SprText = "Справочник - Группы показателей работы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(GroupPerformanceIndicator.Oid), FieldId = nameof(GroupPerformanceIndicator.Oid), FieldComment = nameof(GroupPerformanceIndicator.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(GroupPerformanceIndicator.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(GroupPerformanceIndicator.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(GroupPerformanceIndicator.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CalculationScale), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CalculationScale, NumVar = 1, Soder = "Шкалы расчета", SprText = "Справочник - Шкалы расчета", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CalculationScale.Oid), FieldId = nameof(CalculationScale.Oid), FieldComment = nameof(CalculationScale.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CalculationScale.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CalculationScale.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CalculationScale.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CostItem), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CostItem, NumVar = 1, Soder = "Статьи расходов", SprText = "Справочник - Статьи расходов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CostItem.Oid), FieldId = nameof(CostItem.Oid), FieldComment = nameof(CostItem.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CostItem.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CostItem.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CostItem.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.LetterFilter), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.LetterFilter, NumVar = 1, Soder = "Фильтры для писем", SprText = "Фильтры для писем", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(LetterFilter.Oid), FieldId = nameof(LetterFilter.Oid), FieldComment = nameof(LetterFilter.Email), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(LetterFilter.Email), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(LetterFilter.Email), Caption = "Email", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = $"{nameof(LetterFilter.LetterCatalog)}.{nameof(LetterFilter.LetterCatalog.DisplayName)}", Caption = "Каталог", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Organization), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Organization, NumVar = 1, Soder = "Организация", SprText = "Справочник - Организации", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Organization.Oid), FieldId = nameof(Organization.Oid), FieldComment = nameof(Organization.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Organization.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Organization.INN), Caption = "ИНН", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Organization.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.FAQCatalog), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.FAQCatalog, NumVar = 1, Soder = "Каталог FAQ", SprText = "Справочник - Каталог FAQ", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(FAQCatalog.Oid), FieldId = nameof(FAQCatalog.Oid), FieldComment = nameof(FAQCatalog.DisplayName), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(FAQCatalog.DisplayName), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(FAQCatalog.DisplayName), Caption = "Наименование", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PerformanceIndicator), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PerformanceIndicator, NumVar = 1, Soder = "Показатели работы", SprText = "Справочник - Показатели работы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PerformanceIndicator.Oid), FieldId = nameof(PerformanceIndicator.Oid), FieldComment = nameof(PerformanceIndicator.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PerformanceIndicator.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PerformanceIndicator.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PerformanceIndicator.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(PerformanceIndicator.TypePerformanceIndicator), Caption = "Тип", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(PerformanceIndicator.ValueString), Caption = "Значение", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Staff), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Staff, NumVar = 1, Soder = "Сотрудники", SprText = "Справочник - Сотрудники", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Staff.Oid), FieldId = nameof(Staff.Oid), FieldComment = nameof(Staff.Surname), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Staff.Surname), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Staff.Surname), Caption = "Фамилия", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Staff.Name), Caption = "Имя", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Staff.Patronymic), Caption = "Отчество", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = $"{nameof(Staff.Position)}.{nameof(Staff.Position.Name)}", Caption = "Должность", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Individual), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Individual, NumVar = 1, Soder = "Физическое лицо", SprText = "Справочник - Физические лица", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Individual.Oid), FieldId = nameof(Individual.Oid), FieldComment = nameof(Individual.Surname), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Individual.Surname), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Individual.Surname), Caption = "Фамилия", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Individual.Name), Caption = "Имя", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Individual.Patronymic), Caption = "Отчество", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaskCourier), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaskCourier, NumVar = 1, Soder = "Задача курьера", SprText = "Справочник - Курьерские задачи", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaskCourier.Oid), FieldId = nameof(TaskCourier.Oid), FieldComment = nameof(TaskCourier.PurposeTrip), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = nameof(TaskCourier.Date), SortField2 = nameof(TaskCourier.Date), SortField3 = string.Empty, SizeW = 800, SizeH = 600 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = $"{nameof(TaskCourier.Courier)}.{nameof(TaskCourier.Courier.IndividualString)}", Caption = "Курьер", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TaskCourier.Date), Caption = "Дата", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = $"{nameof(TaskCourier.Customer)}.{nameof(TaskCourier.Customer.Name)}", Caption = "Клиент", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(TaskCourier.StatusTaskCourier), Caption = "Состояние", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaskRouteSheetv2), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaskRouteSheetv2, NumVar = 1, Soder = "Задача маршрутного листа", SprText = "Справочник - Курьерские задачи", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaskRouteSheetv2.Oid), FieldId = nameof(TaskRouteSheetv2.Oid), FieldComment = nameof(TaskRouteSheetv2.PurposeTrip), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = nameof(TaskRouteSheetv2.Date), SortField2 = nameof(TaskRouteSheetv2.Date), SortField3 = string.Empty, SizeW = 800, SizeH = 600 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TaskRouteSheetv2.Date), Caption = "Дата", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = $"{nameof(TaskRouteSheetv2.Customer)}.{nameof(TaskRouteSheetv2.Customer.Name)}", Caption = "Клиент", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(TaskRouteSheetv2.StatusTaskCourier), Caption = "Состояние", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Mailbox), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Mailbox, NumVar = 1, Soder = "Почтовый ящик", SprText = "Справочник - Почтовые ящики", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Mailbox.Oid), FieldId = nameof(Mailbox.Oid), FieldComment = nameof(Mailbox.MailingAddress), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Mailbox.MailingAddress), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Mailbox.MailingAddress), Caption = "Почтовый адрес", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Mailbox.Login), Caption = "Логин", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Mailbox.StateMailbox), Caption = "Состояние", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(Mailbox.Comment), Caption = "Комментарий", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.LetterTemplate), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.LetterTemplate, NumVar = 1, Soder = "Шаблон почтового сообщения", SprText = "Справочник - Шаблоны почтовых сообщений", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(LetterTemplate.Oid), FieldId = nameof(LetterTemplate.Oid), FieldComment = nameof(LetterTemplate.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(LetterTemplate.Name), SortField3 = string.Empty, SizeW = 800, SizeH = 600 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(LetterTemplate.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(LetterTemplate.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ColorStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ColorStatus, NumVar = 1, Soder = "Цветовые схемы пользователей", SprText = "Справочник - Цветовые схемы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ColorStatus.Oid), FieldId = nameof(ColorStatus.Oid), FieldComment = nameof(ColorStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ColorStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ColorStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ColorStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CustomerSettings), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CustomerSettings, NumVar = 1, Soder = "Отображение таблицы клиентов", SprText = "Справочник - Отображение таблицы клиентов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CustomerSettings.Oid), FieldId = nameof(CustomerSettings.Oid), FieldComment = nameof(CustomerSettings.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CustomerSettings.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CustomerSettings.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CustomerSettings.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PlateTemplate), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PlateTemplate, NumVar = 1, Soder = "Шаблон печатной формы договора", SprText = "Справочник - Шаблон печатной формы договора", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PlateTemplate.Oid), FieldId = nameof(PlateTemplate.Oid), FieldComment = nameof(PlateTemplate.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PlateTemplate.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PlateTemplate.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PlateTemplate.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.RouteSheet), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.RouteSheet, NumVar = 1, Soder = "Маршрутный лист", SprText = "Справочник - Маршрутные листы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(RouteSheet.Oid), FieldId = nameof(RouteSheet.Oid), FieldComment = nameof(RouteSheet.Date), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(RouteSheet.Date), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(RouteSheet.Date), Caption = "Дата", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = $"{nameof(RouteSheet.Courier)}.{nameof(RouteSheet.Courier.IndividualString)}", Caption = "ФИО", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.RouteSheetv2), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.RouteSheetv2, NumVar = 1, Soder = "Маршрутный лист", SprText = "Справочник - Маршрутные листы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(RouteSheet.Oid), FieldId = nameof(RouteSheet.Oid), FieldComment = nameof(RouteSheet.Date), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(RouteSheet.Date), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(RouteSheetv2.Date), Caption = "Дата", Visible = true, HAlignment = 0 });
                //sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = $"{nameof(RouteSheetv2.Courier)}.{nameof(RouteSheet.Courier.IndividualString)}", Caption = "ФИО", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Customer), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Customer, NumVar = 1, Soder = "Клиенты", SprText = "Справочник - Клиенты", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Customer.Oid), FieldId = nameof(Customer.Oid), FieldComment = nameof(Customer.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = nameof(Customer.Name), SortField2 = nameof(Customer.Name), SortField3 = string.Empty, SizeW = 700, SizeH = 650 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Customer.INN), Caption = "ИНН", Width = 150, FixedWidth = true, Visible = true, HAlignment = 2 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Customer.Name), Caption = "Наименование", Width = 550, FixedWidth = true, Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.User), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.User, NumVar = 1, Soder = "Пользователи", SprText = "Справочник - Пользователи", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(User.Oid), FieldId = nameof(User.Oid), FieldComment = nameof(User.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Staff.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(User.Login), Caption = "Логин", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(User.Surname), Caption = "Фамилия", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(User.Name), Caption = "Имя", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(User.Patronymic), Caption = "Отчество", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.UserGroup), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.UserGroup, NumVar = 1, Soder = "Группы пользователей", SprText = "Справочник - Групп пользователей", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(UserGroup.Oid), FieldId = nameof(UserGroup.Oid), FieldComment = nameof(UserGroup.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(UserGroup.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(UserGroup.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(UserGroup.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Status), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Status, NumVar = 1, Soder = "Статусы", SprText = "Справочник - Статусы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Status.Oid), FieldId = nameof(Status.Oid), FieldComment = nameof(Status.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Status.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Status.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Status.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Status.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ContractStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ContractStatus, NumVar = 1, Soder = "Статусы договоров", SprText = "Справочник - Статусы договоров", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ContractStatus.Oid), FieldId = nameof(ContractStatus.Oid), FieldComment = nameof(ContractStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ContractStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ContractStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ContractStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(ContractStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PatentStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PatentStatus, NumVar = 1, Soder = "Статусы патентов", SprText = "Справочник - Статусы патентов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PatentStatus.Oid), FieldId = nameof(PatentStatus.Oid), FieldComment = nameof(PatentStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PatentStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PatentStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PatentStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(PatentStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaskStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaskStatus, NumVar = 1, Soder = "Статусы задачи", SprText = "Справочник - Статусы задачи", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaskStatus.Oid), FieldId = nameof(TaskStatus.Oid), FieldComment = nameof(TaskStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TaskStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TaskStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TaskStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(TaskStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.GeneralVocabulary), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.GeneralVocabulary, NumVar = 1, Soder = "Общий справочник", SprText = "Словарь - Общий справочник", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(GeneralVocabulary.Oid), FieldId = nameof(GeneralVocabulary.Oid), FieldComment = nameof(GeneralVocabulary.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(GeneralVocabulary.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(GeneralVocabulary.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(GeneralVocabulary.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CalculatorTaxSystem), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CalculatorTaxSystem, NumVar = 1, Soder = "Система налогообложения для калькулятора", SprText = "Словарь - Система налогообложения для калькулятора", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CalculatorTaxSystem.Oid), FieldId = nameof(CalculatorTaxSystem.Oid), FieldComment = nameof(CalculatorTaxSystem.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CalculatorTaxSystem.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CalculatorTaxSystem.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CalculatorTaxSystem.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.CalculatorIndicator), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.CalculatorIndicator, NumVar = 1, Soder = "Показатели для калькулятора", SprText = "Словарь - Показатели для калькулятора", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(CalculatorIndicator.Oid), FieldId = nameof(CalculatorIndicator.Oid), FieldComment = nameof(CalculatorIndicator.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(CalculatorIndicator.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(CalculatorIndicator.IsUseWhenForming), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(CalculatorIndicator.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.VacationType), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.VacationType, NumVar = 1, Soder = "Виды отпусков", SprText = "Словарь - Виды отпусков", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(VacationType.Oid), FieldId = nameof(VacationType.Oid), FieldComment = nameof(VacationType.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(VacationType.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(VacationType.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(VacationType.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.DealStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.DealStatus, NumVar = 1, Soder = "Статусы сделки", SprText = "Справочник - Статусы сделки", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(DealStatus.Oid), FieldId = nameof(DealStatus.Oid), FieldComment = nameof(DealStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(DealStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(DealStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(DealStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(DealStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PackageDocumentStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PackageDocumentStatus, NumVar = 1, Soder = "Статусы пакетов документов", SprText = "Справочник - Статусы пакетов документов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PackageDocumentStatus.Oid), FieldId = nameof(PackageDocumentStatus.Oid), FieldComment = nameof(PackageDocumentStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PackageDocumentStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PackageDocumentStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PackageDocumentStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(PackageDocumentStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.AccountStatementStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.AccountStatementStatus, NumVar = 1, Soder = "Статусы счетов", SprText = "Справочник - Статусы счетов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(AccountStatementStatus.Oid), FieldId = nameof(AccountStatementStatus.Oid), FieldComment = nameof(AccountStatementStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(AccountStatementStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(AccountStatementStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(AccountStatementStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(AccountStatementStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.AccountingInsuranceStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.AccountingInsuranceStatus, NumVar = 1, Soder = "Статусы страховых", SprText = "Справочник - Статусы страховых", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(AccountingInsuranceStatus.Oid), FieldId = nameof(AccountingInsuranceStatus.Oid), FieldComment = nameof(AccountingInsuranceStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(AccountingInsuranceStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(AccountingInsuranceStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(AccountingInsuranceStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(AccountingInsuranceStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.IndividualEntrepreneursTaxStatus), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.IndividualEntrepreneursTaxStatus, NumVar = 1, Soder = "Статусы отчетов ИП", SprText = "Справочник - Статусы отчетов ИП", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(AccountingInsuranceStatus.Oid), FieldId = nameof(AccountingInsuranceStatus.Oid), FieldComment = nameof(AccountingInsuranceStatus.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(AccountingInsuranceStatus.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(IndividualEntrepreneursTaxStatus.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(IndividualEntrepreneursTaxStatus.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(IndividualEntrepreneursTaxStatus.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.StatusPreTax), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.StatusPreTax, NumVar = 1, Soder = "Статусы предварительных налогов", SprText = "Справочник - Статусы предварительных налогов", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(StatusPreTax.Oid), FieldId = nameof(StatusPreTax.Oid), FieldComment = nameof(StatusPreTax.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(StatusPreTax.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(StatusPreTax.IsDefault), Caption = "v", Visible = true, HAlignment = 0, Width = 15, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(StatusPreTax.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(StatusPreTax.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaxSystem), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaxSystem, NumVar = 1, Soder = "Системы налогообложения", SprText = "Справочник - Системы налогообложения", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaxSystem.Oid), FieldId = nameof(TaxSystem.Oid), FieldComment = nameof(TaxSystem.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TaxSystem.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TaxSystem.Name), Caption = "Система налогообложения", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TaxSystem.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.FormCorporation), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.FormCorporation, NumVar = 1, Soder = "Организационно-правовые формы", SprText = "Справочник - Организационно-правовые формы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(FormCorporation.Oid), FieldId = nameof(FormCorporation.Oid), FieldComment = nameof(FormCorporation.AbbreviatedName), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(FormCorporation.AbbreviatedName), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(FormCorporation.Kod), Caption = "Код", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(FormCorporation.AbbreviatedName), Caption = "Сокращенное наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(FormCorporation.FullName), Caption = "Полное наименование", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PhysicalIndicator), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PhysicalIndicator, NumVar = 1, Soder = "Физические показатели", SprText = "Справочник - Физические показатели", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PhysicalIndicator.Oid), FieldId = nameof(PhysicalIndicator.Oid), FieldComment = nameof(PhysicalIndicator.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PhysicalIndicator.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PhysicalIndicator.Name), Caption = "Физический показатель", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PhysicalIndicator.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ArchiveFolder), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ArchiveFolder, NumVar = 1, Soder = "Архивные папки", SprText = "Справочник - Архивные папки", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ArchiveFolder.Oid), FieldId = nameof(ArchiveFolder.Oid), FieldComment = nameof(ArchiveFolder.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ArchiveFolder.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ArchiveFolder.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ArchiveFolder.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TypePayment), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TypePayment, NumVar = 1, Soder = "Виды платежей", SprText = "Справочник - Виды платежей", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TypePayment.Oid), FieldId = nameof(TypePayment.Oid), FieldComment = nameof(TypePayment.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TypePayment.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TypePayment.Name), Caption = "Вид платежей", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TypePayment.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.PriceList), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.PriceList, NumVar = 1, Soder = "Прайс", SprText = "Справочник - Прайс", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(PriceList.Oid), FieldId = nameof(PriceList.Oid), FieldComment = nameof(PriceList.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(PriceList.Name), SortField3 = string.Empty, SizeW = 750, SizeH = 350 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(PriceList.Kod), Caption = "Код", Visible = true, HAlignment = 0, FixedWidth = true, Width = 100 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(PriceList.Name), Caption = "Наименование", Visible = true, HAlignment = 0, FixedWidth = true, Width = 250 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(PriceList.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 4, Name = nameof(PriceList.Price), Caption = "Цена", Visible = true, HAlignment = 0, FixedWidth = true, Width = 100, FormatType = "1", FormatString = "n2" });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.TaskObject), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.TaskObject, NumVar = 1, Soder = "Отчеты", SprText = "Справочник - Отчеты", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(TaskObject.Oid), FieldId = nameof(TaskObject.Oid), FieldComment = nameof(TaskObject.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(TaskObject.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(TaskObject.IsUse), Caption = "!", Visible = true, HAlignment = 0, Width = 25, FixedWidth = true });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(TaskObject.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(TaskObject.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Report), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Report, NumVar = 1, Soder = "Отчеты", SprText = "Справочник - Отчеты", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Report.Oid), FieldId = nameof(Report.Oid), FieldComment = nameof(Report.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Report.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Report.FormIndex), Caption = "Индекс формы", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Report.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 3, Name = nameof(Report.OKUD), Caption = "ОКУД", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ReportV2), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ReportV2, NumVar = 1, Soder = "Отчеты", SprText = "Справочник - Отчеты", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ReportV2.Oid), FieldId = nameof(ReportV2.Oid), FieldComment = nameof(ReportV2.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ReportV2.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ReportV2.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ReportV2.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ElectronicReporting), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ElectronicReporting, NumVar = 1, Soder = "Электронная отчетность", SprText = "Справочник - Электронная отчетность", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ElectronicReporting.Oid), FieldId = nameof(ElectronicReporting.Oid), FieldComment = nameof(ElectronicReporting.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ElectronicReporting.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ElectronicReporting.Name), Caption = "Электронная отчетность", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ElectronicReporting.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.ElectronicDocumentManagement), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.ElectronicDocumentManagement, NumVar = 1, Soder = "ЭДО", SprText = "Справочник - ЭДО", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(ElectronicDocumentManagement.Oid), FieldId = nameof(ElectronicDocumentManagement.Oid), FieldComment = nameof(ElectronicDocumentManagement.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(ElectronicDocumentManagement.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(ElectronicDocumentManagement.Name), Caption = "Наименование", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(ElectronicDocumentManagement.Description), Caption = "Описание", Visible = true, HAlignment = 0 });
            }

            if (sess.FindObject<set_SprShablonView>(new BinaryOperator("SprVariant", (int)cls_App.ReferenceBooks.Privilege), false) == null)
            {
                l_g_id = Guid.NewGuid().ToString();
                sess.Save(new set_SprShablonView(sess) { g_id = l_g_id, Task = 2, SprVariant = (int)cls_App.ReferenceBooks.Privilege, NumVar = 1, Soder = "Льготы", SprText = "Справочник - Льготы", IsFormEnumeration = false, EditButtons = true, FieldGuid = nameof(Privilege.Oid), FieldId = nameof(Privilege.Oid), FieldComment = nameof(Privilege.Name), FieldDef = string.Empty, FieldImage = string.Empty, SortField1 = "Index", SortField2 = nameof(Privilege.Name), SortField3 = string.Empty, SizeW = 0, SizeH = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 1, Name = nameof(Privilege.Kod), Caption = "Код", Visible = true, HAlignment = 0 });
                sess.Save(new set_SprShablonViewDetail(sess) { root_g_id = l_g_id, Index = 2, Name = nameof(Privilege.Description), Caption = "Наименование", Visible = true, HAlignment = 0 });
            }
        }
        #endregion

        public bool Init(Session sess, int spr_var, int num_var = 1)
        {
            if (sess == null) sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

            _SprVaiant = (cls_App.ReferenceBooks)spr_var;
            _TypeSpr = GetTypeSprVariants(spr_var);

            bool find_shablon = false;

            if (ImageCollection == null) ImageCollection = GetImageCollection(spr_var);

            if (FieldId == null || FieldId == String.Empty) FieldId = "Oid";                         // По умолчанию
            if (FieldComment == null || FieldComment == String.Empty) FieldComment = "FullName";     // По умолчанию

            FlagSprDateRange = IsSprDateRanges(spr_var);
            FlagSprEnumeration = IsSprEnumeration(spr_var);

            _SprShablonView = sess.FindObject<set_SprShablonView>(new GroupOperator(GroupOperatorType.And, new BinaryOperator("SprVariant", spr_var), new BinaryOperator("NumVar", num_var)));

            if (_SprShablonView != null)
            {
                if (_SprShablonView.FieldId != null && _SprShablonView.FieldId != String.Empty) FieldId = _SprShablonView.FieldId;
                if (_SprShablonView.FieldComment != null && _SprShablonView.FieldComment != String.Empty) FieldComment = _SprShablonView.FieldComment;
                //if (_SprShablonView.FieldImage != null && _SprShablonView.FieldImage != String.Empty) FieldImageIndex = _SprShablonView.FieldImage;

                if (_SprShablonView.IsFormEnumeration)
                { // форма редактирования "formEdit_BaseSprEnumeration"
                    _SprShablonViewEnumeration = sess.FindObject<set_SprShablonViewEnumeration>(new BinaryOperator("root_g_id", _SprShablonView.g_id));
                    if (_SprShablonViewEnumeration != null)
                    {

                        find_shablon = true;
                    }

                    if (_SprShablonView.FieldImage != null && _SprShablonView.FieldImage == "ImageIndex")
                    {
                        if (ImageCollection == null)
                        { // если ImageCollection еще не определена в GetImageCollection(), но в шаблоне должны быть картинки - инициализируем стандартную imageCollectionSprEnumeration
                            ImageCollection = cls_App.ImgCol.imageCollectionSprEnumeration;
                            //FieldImageIndex = "ImageIndex";
                        }
                    }
                    else
                    {
                        ImageCollection = null; // Зануляем, если показывать не нужно, а ранее была уже определена
                    }
                }
                else
                {
                    _SprShablonViewDetail = new XPCollection<set_SprShablonViewDetail>(sess, new BinaryOperator("root_g_id", _SprShablonView.g_id), new SortProperty("Index", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    if (_SprShablonViewDetail != null && _SprShablonViewDetail.Count > 0) find_shablon = true;
                }
            }
            else
            {
                if (FlagSprEnumeration)
                {
                    _SprShablonView = new set_SprShablonView();

                    _SprShablonView.FieldGuid = "g_id";
                    _SprShablonView.FieldDef = "fl_def";
                    _SprShablonView.FieldId = "Oid";
                    _SprShablonView.Task = 0;
                    _SprShablonView.SprVariant = (int)_SprVaiant;
                    _SprShablonView.NumVar = 1;
                    _SprShablonView.Soder = "Справочник Base";
                    _SprShablonView.SprText = "Справочник Base";
                    _SprShablonView.IsFormEnumeration = true;
                    _SprShablonView.EditButtons = true;
                    _SprShablonView.FieldComment = "FullName";
                    _SprShablonView.SortField1 = string.Empty;
                    _SprShablonView.SortField2 = string.Empty;
                    _SprShablonView.SortField3 = string.Empty;
                    _SprShablonView.SizeW = 0;
                    _SprShablonView.SizeH = 0;

                    if (ImageCollection != null) _SprShablonView.FieldImage = "ImageIndex";
                }
            }

            if ((FlagSprEnumeration) && _SprShablonViewEnumeration == null && _SprShablonViewDetail == null)
            {
                _SprShablonViewEnumeration = new set_SprShablonViewEnumeration();

                _SprShablonViewEnumeration.FieldKod = string.Empty;
                _SprShablonViewEnumeration.FieldName = "Name";
                _SprShablonViewEnumeration.FieldNameCaption = "Наименование";
                _SprShablonViewEnumeration.FieldSoder = "FullName";
                _SprShablonViewEnumeration.FieldSoderCaption = "Содержание";
                _SprShablonViewEnumeration.FieldGroupI1 = "Group";
            }

            if (ImageCollection != null)
            {
                if (_SprShablonView.FieldImage != null && _SprShablonView.FieldImage != String.Empty) FieldImageIndex = _SprShablonView.FieldImage;
                else { _SprShablonView.FieldImage = "ImageIndex"; FieldImageIndex = "ImageIndex"; } // Если ImageCollection определена, а Поле не задано
            }
            else FieldImageIndex = string.Empty;

            return find_shablon;
        }

        public string GetFieldsString()
        {
            string l_return = string.Empty;

            l_return += FieldId;

            if (_SprShablonView.IsFormEnumeration)
            {
                if (_SprShablonView.FieldDef != null && _SprShablonView.FieldDef != String.Empty)
                {
                    l_return += $";{_SprShablonView.FieldDef}";
                }

                if (FieldImageIndex != String.Empty)
                {
                    l_return += $";{FieldImageIndex}";
                }

                if (_SprShablonViewEnumeration.FieldKod != null && _SprShablonViewEnumeration.FieldKod != String.Empty)
                {
                    l_return += $";{_SprShablonViewEnumeration.FieldKod}";
                }

                l_return += $";{_SprShablonViewEnumeration.FieldName};{_SprShablonViewEnumeration.FieldSoder}";
            }
            else
            {
                if (_SprShablonViewDetail != null && _SprShablonViewDetail.Count > 0)
                {
                    foreach (set_SprShablonViewDetail o_svd in _SprShablonViewDetail)
                    {
                        l_return += $";{o_svd.Name}";
                    }
                }
            }

            return l_return;
        }

        public CriteriaOperator GetCriteria()
        {
            CriteriaOperator l_criteria = null;

            if (FlagSprEnumeration) l_criteria = new BinaryOperator("Group", (int)_SprVaiant);
            else
            {
                if (_SprShablonView.IsFormEnumeration)
                {
                    GroupOperator l_groupcriteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { null });

                    if (_SprShablonViewEnumeration.FieldGroupI1 != null && _SprShablonViewEnumeration.FieldGroupI1 != String.Empty) // "Group"
                        l_groupcriteria.Operands.Add(new BinaryOperator(_SprShablonViewEnumeration.FieldGroupI1, (int)_SprVaiant));

                    if (_SprShablonViewEnumeration.FieldGroupI2 != null && _SprShablonViewEnumeration.FieldGroupI2 != String.Empty) // "Year"
                        l_groupcriteria.Operands.Add(new BinaryOperator(_SprShablonViewEnumeration.FieldGroupI2, BVVGlobal.oApp.WorkDate.Year));

                    if (_SprShablonViewEnumeration.FieldGroupS1 != null && _SprShablonViewEnumeration.FieldGroupS1 != String.Empty) // "User"
                        l_groupcriteria.Operands.Add(new BinaryOperator(_SprShablonViewEnumeration.FieldGroupS1, BVVGlobal.oApp.User));


                    l_criteria = l_groupcriteria;
                }
            }

            return l_criteria;
        }

        public SortingCollection GetSorting()
        {
            SortingCollection l_sorting = null;

            if (_SprShablonView != null)
            {
                if (_SprShablonView.SortField1 != null && _SprShablonView.SortField1 != String.Empty)
                {
                    l_sorting = new SortingCollection();

                    if (_SprShablonView.SortField1 != null && _SprShablonView.SortField1 != String.Empty) // "SortField1"
                        l_sorting.Add(new SortProperty(_SprShablonView.SortField1, DevExpress.Xpo.DB.SortingDirection.Ascending));

                    if (_SprShablonView.SortField2 != null && _SprShablonView.SortField2 != String.Empty) // "SortField2"
                        l_sorting.Add(new SortProperty(_SprShablonView.SortField2, DevExpress.Xpo.DB.SortingDirection.Ascending));

                    if (_SprShablonView.SortField3 != null && _SprShablonView.SortField3 != String.Empty) // "SortField3"
                        l_sorting.Add(new SortProperty(_SprShablonView.SortField3, DevExpress.Xpo.DB.SortingDirection.Ascending));
                }
                else
                {
                    if (_SprShablonView.IsFormEnumeration) // _SprShablonViewEnumeration
                    {
                        l_sorting = new SortingCollection();

                        if (_SprShablonViewEnumeration.FieldKod != null && _SprShablonViewEnumeration.FieldKod != String.Empty) // "Kod"
                            l_sorting.Add(new SortProperty(_SprShablonViewEnumeration.FieldKod, DevExpress.Xpo.DB.SortingDirection.Ascending));

                        if (_SprShablonViewEnumeration.FieldName != null && _SprShablonViewEnumeration.FieldName != String.Empty) // "Name"
                            l_sorting.Add(new SortProperty(_SprShablonViewEnumeration.FieldName, DevExpress.Xpo.DB.SortingDirection.Ascending));
                    }
                }
            }

            if (l_sorting == null && (FlagSprEnumeration)) { l_sorting = new SortingCollection(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending)); }

            return l_sorting;
        }

        public void FillGridView(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            if (!ReferenceEquals(ImageCollection, null))
            {
                DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = grid.GridControl.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
                imageCombo.SmallImages = ImageCollection;
                for (int i = 0; i < ImageCollection.Images.Count; i++) imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i));
                imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
                grid.Columns[FieldImageIndex].ColumnEdit = imageCombo;
            }

            grid.Columns[FieldId].Visible = false;

            if (_SprShablonViewEnumeration != null)
            {
                if (_SprShablonView.FieldDef != null && _SprShablonView.FieldDef != String.Empty)
                {
                    grid.Columns[_SprShablonView.FieldDef].Caption = "v";
                    grid.Columns[_SprShablonView.FieldDef].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    grid.Columns[_SprShablonView.FieldDef].Width = 20;
                    grid.Columns[_SprShablonView.FieldDef].OptionsColumn.FixedWidth = true;
                }

                if (_SprShablonViewEnumeration.FieldKod != String.Empty)
                {
                    grid.Columns[_SprShablonViewEnumeration.FieldKod].Caption = _SprShablonViewEnumeration.FieldKodCaption;
                    grid.Columns[_SprShablonViewEnumeration.FieldKod].Width = 30;
                }
                if (_SprShablonViewEnumeration.FieldName != String.Empty)
                {
                    grid.Columns[_SprShablonViewEnumeration.FieldName].Caption = _SprShablonViewEnumeration.FieldNameCaption;
                    grid.Columns[_SprShablonViewEnumeration.FieldName].Width = 60;
                }
                if (_SprShablonViewEnumeration.FieldSoder != String.Empty)
                {
                    grid.Columns[_SprShablonViewEnumeration.FieldSoder].Caption = _SprShablonViewEnumeration.FieldSoderCaption;
                    grid.Columns[_SprShablonViewEnumeration.FieldSoder].Width = 100;
                }
            }

            if (_SprShablonViewDetail != null && _SprShablonViewDetail.Count > 0)
            {
                foreach (set_SprShablonViewDetail o_svd in _SprShablonViewDetail)
                {
                    grid.Columns[o_svd.Name].Visible = o_svd.Visible;
                    if (o_svd.Caption != null && o_svd.Caption != String.Empty) grid.Columns[o_svd.Name].Caption = o_svd.Caption;
                    if (o_svd.Width > 0) grid.Columns[o_svd.Name].Width = o_svd.Width;
                    if (o_svd.FixedWidth) grid.Columns[o_svd.Name].OptionsColumn.FixedWidth = true;

                    if (o_svd.HAlignment > 0) grid.Columns[o_svd.Name].AppearanceHeader.TextOptions.HAlignment = (DevExpress.Utils.HorzAlignment)o_svd.HAlignment;

                    if (o_svd.FormatType != null && o_svd.FormatType != String.Empty)
                    {
                        if ("123".Contains(o_svd.FormatType)) // DevExpress.Utils.FormatType .None (0) .Numeric (1) .DateTime (2) .Custom (3)
                        {
                            int i_ft = Convert.ToInt32(o_svd.FormatType);
                            grid.Columns[o_svd.Name].DisplayFormat.FormatType = (DevExpress.Utils.FormatType)i_ft; // DevExpress.Utils.FormatType.DateTime;
                            grid.Columns[o_svd.Name].DisplayFormat.FormatString = o_svd.FormatString; // "dd.MM.yyyy"; // "d"; // "yy.MM.dd  HH:mm";
                        }
                    }
                }
            }

            if (!ReferenceEquals(ImageCollection, null))
            {
                grid.Columns[FieldImageIndex].Caption = "!";
                grid.Columns[FieldImageIndex].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grid.Columns[FieldImageIndex].Width = 20;
                grid.Columns[FieldImageIndex].OptionsColumn.FixedWidth = true;
            }
        }

        /// <summary>
        /// Классическое перечисление использующее таблицу set_SprEnumeration (вынесена из таблиц для оперативного доступа)
        /// </summary>
        /// <param name="iSprVariant">Вариант справочника по перечислению cls_App.SprVariants</param>
        /// <returns>true/false</returns>
        public static bool IsSprEnumeration(int iSprVariant)
        {
            bool fl_return = false;

            int[] enum_list = {

                              };

            if (enum_list.Contains<int>(iSprVariant)) fl_return = true;

            return fl_return;
        }

        /// <summary>
        /// Отбор в перечисленных справочниках по периоду (вынесена из таблиц для оперативного доступа)
        /// </summary>
        /// <param name="iSprVariant">Вариант справочника по перечислению cls_App.SprVariants</param>
        /// <returns>true/false</returns>
        public static bool IsSprDateRanges(int iSprVariant)
        {
            bool fl_return = false;
            return fl_return;
        }

        /// <summary>
        /// Возвращает Type справочника
        /// </summary>
        /// <param name="iSprVariant">Вариант справочника по перечислению cls_App.SprVariants</param>
        /// <returns>Type</returns>
        public static Type GetTypeSprVariants(int iSprVariant)
        {
            Type type = null;

            if (IsSprEnumeration(iSprVariant)) type = typeof(set_SprEnumeration);
            else
            {
                switch ((cls_App.ReferenceBooks)iSprVariant)
                {
                    case cls_App.ReferenceBooks.PayoutDictionary:

                        type = typeof(PayoutDictionary);
                        break;

                    case cls_App.ReferenceBooks.User:

                        type = typeof(User);
                        break;

                    case cls_App.ReferenceBooks.Status:

                        type = typeof(Status);
                        break;

                    case cls_App.ReferenceBooks.ContractStatus:

                        type = typeof(ContractStatus);
                        break;

                    case cls_App.ReferenceBooks.PatentStatus:

                        type = typeof(PatentStatus);
                        break;

                    case cls_App.ReferenceBooks.TaskStatus:

                        type = typeof(TaskStatus);
                        break;

                    case cls_App.ReferenceBooks.DealStatus:
                        type = typeof(DealStatus);
                        break;

                    case cls_App.ReferenceBooks.PackageDocumentStatus:
                        type = typeof(PackageDocumentStatus);
                        break;

                    case cls_App.ReferenceBooks.AccountStatementStatus:
                        type = typeof(AccountStatementStatus);
                        break;

                    case cls_App.ReferenceBooks.GeneralVocabulary:

                        type = typeof(GeneralVocabulary);
                        break;

                    case cls_App.ReferenceBooks.CalculatorTaxSystem:

                        type = typeof(CalculatorTaxSystem);
                        break;

                    case cls_App.ReferenceBooks.CalculatorIndicator:

                        type = typeof(CalculatorIndicator);
                        break;

                    case cls_App.ReferenceBooks.VacationType:

                        type = typeof(VacationType);
                        break;

                    case cls_App.ReferenceBooks.AccountingInsuranceStatus:

                        type = typeof(AccountingInsuranceStatus);
                        break;

                    case cls_App.ReferenceBooks.IndividualEntrepreneursTaxStatus:

                        type = typeof(IndividualEntrepreneursTaxStatus);
                        break;

                    case cls_App.ReferenceBooks.StatusPreTax:

                        type = typeof(StatusPreTax);
                        break;

                    case cls_App.ReferenceBooks.FormCorporation:

                        type = typeof(FormCorporation);
                        break;

                    case cls_App.ReferenceBooks.PhysicalIndicator:

                        type = typeof(PhysicalIndicator);
                        break;

                    case cls_App.ReferenceBooks.ArchiveFolder:

                        type = typeof(ArchiveFolder);
                        break;

                    case cls_App.ReferenceBooks.TypePayment:

                        type = typeof(TypePayment);
                        break;

                    case cls_App.ReferenceBooks.PriceList:

                        type = typeof(PriceList);
                        break;

                    case cls_App.ReferenceBooks.Privilege:

                        type = typeof(Privilege);
                        break;

                    case cls_App.ReferenceBooks.Report:

                        type = typeof(Report);
                        break;

                    case cls_App.ReferenceBooks.ReportV2:

                        type = typeof(ReportV2);
                        break;

                    case cls_App.ReferenceBooks.TaskObject:

                        type = typeof(TaskObject);
                        break;

                    case cls_App.ReferenceBooks.ElectronicReporting:
                        type = typeof(ElectronicReporting);
                        break;

                    case cls_App.ReferenceBooks.ElectronicDocumentManagement:
                        type = typeof(ElectronicDocumentManagement);
                        break;

                    case cls_App.ReferenceBooks.TaxSystem:

                        type = typeof(TaxSystem);
                        break;

                    case cls_App.ReferenceBooks.UserGroup:

                        type = typeof(UserGroup);
                        break;

                    case cls_App.ReferenceBooks.Staff:

                        type = typeof(Staff);
                        break;

                    case cls_App.ReferenceBooks.Individual:

                        type = typeof(Individual);
                        break;

                    case cls_App.ReferenceBooks.TaskCourier:

                        type = typeof(TaskCourier);
                        break;

                    case cls_App.ReferenceBooks.TaskRouteSheetv2:

                        type = typeof(TaskRouteSheetv2);
                        break;

                    case cls_App.ReferenceBooks.Mailbox:

                        type = typeof(Mailbox);
                        break;

                    case cls_App.ReferenceBooks.LetterTemplate:

                        type = typeof(LetterTemplate);
                        break;

                    case cls_App.ReferenceBooks.ColorStatus:

                        type = typeof(ColorStatus);
                        break;

                    case cls_App.ReferenceBooks.CustomerSettings:

                        type = typeof(CustomerSettings);
                        break;

                    case cls_App.ReferenceBooks.PlateTemplate:

                        type = typeof(PlateTemplate);
                        break;

                    case cls_App.ReferenceBooks.RouteSheet:

                        type = typeof(RouteSheet);
                        break;

                    case cls_App.ReferenceBooks.RouteSheetv2:

                        type = typeof(RouteSheetv2);
                        break;

                    case cls_App.ReferenceBooks.Bank:

                        type = typeof(Bank);
                        break;

                    case cls_App.ReferenceBooks.Position:

                        type = typeof(Position);
                        break;

                    case cls_App.ReferenceBooks.StatusAccrual:

                        type = typeof(StatusAccrual);
                        break;

                    case cls_App.ReferenceBooks.Document:
                        type = typeof(Document);
                        break;

                    case cls_App.ReferenceBooks.CustomerStaff:
                        type = typeof(CustomerStaff);
                        break;

                    case cls_App.ReferenceBooks.StatutoryDocument:

                        type = typeof(StatutoryDocument);
                        break;

                    case cls_App.ReferenceBooks.TitleDocument:

                        type = typeof(TitleDocument);
                        break;

                    case cls_App.ReferenceBooks.TaxReportingDocument:

                        type = typeof(TaxReportingDocument);
                        break;

                    case cls_App.ReferenceBooks.SourceDocument:

                        type = typeof(SourceDocument);
                        break;

                    case cls_App.ReferenceBooks.EmployeeDetailsDocument:

                        type = typeof(EmployeeDetailsDocument);
                        break;

                    case cls_App.ReferenceBooks.CustomerFilter:

                        type = typeof(CustomerFilter);
                        break;

                    case cls_App.ReferenceBooks.SectionOKVED2:

                        type = typeof(SectionOKVED2);
                        break;

                    case cls_App.ReferenceBooks.ClassOKVED2:

                        type = typeof(ClassOKVED2);
                        break;

                    case cls_App.ReferenceBooks.EntrepreneurialActivityCodesUTII:

                        type = typeof(EntrepreneurialActivityCodesUTII);
                        break;

                    case cls_App.ReferenceBooks.GroupPerformanceIndicator:

                        type = typeof(GroupPerformanceIndicator);
                        break;

                    case cls_App.ReferenceBooks.CalculationScale:

                        type = typeof(CalculationScale);
                        break;

                    case cls_App.ReferenceBooks.CostItem:

                        type = typeof(CostItem);
                        break;

                    case cls_App.ReferenceBooks.LetterFilter:

                        type = typeof(LetterFilter);
                        break;

                    case cls_App.ReferenceBooks.Organization:

                        type = typeof(Organization);
                        break;

                    case cls_App.ReferenceBooks.FAQCatalog:

                        type = typeof(FAQCatalog);
                        break;

                    case cls_App.ReferenceBooks.PerformanceIndicator:

                        type = typeof(PerformanceIndicator);
                        break;

                    case cls_App.ReferenceBooks.Customer:

                        type = typeof(Customer);
                        break;

                }
            }
            return type;
        }

        /// <summary>
        /// Возвращает ImageCollection справочника
        /// </summary>
        /// <param name="iSprVariant">Вариант справочника по перечислению cls_App.SprVariants</param>
        /// <returns>DevExpress.Utils.ImageCollection</returns>
        public static DevExpress.Utils.ImageCollection GetImageCollection(int iSprVariant)
        {
            DevExpress.Utils.ImageCollection image_collection = null;

            switch ((cls_App.ReferenceBooks)iSprVariant)
            {
                default:
                    break;
            }

            return image_collection;
        }

        /// <summary>
        /// Возвращает Форму редактирования справочника
        /// </summary>
        /// <param name="iSprVariant">Вариант справочника по перечислению cls_App.SprVariants</param>
        /// <returns>formEdit_BaseSpr</returns>
        public formEdit_BaseSpr GetFormEdit(int id = -1, object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            formEdit_BaseSpr form = null;

            if (IsSprEnumeration(SprVariant) || (_SprShablonViewEnumeration != null && (_SprShablonView.IsFormEnumeration)))
            {
                form = new formEdit_BaseSprEnumeration(id) { BaseSpr = this };
            }
            else
            {
                switch ((cls_App.ReferenceBooks)SprVariant)
                {
                    case cls_App.ReferenceBooks.Customer:
                        form = new CustomerEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Staff:
                        form = new StaffEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Individual:
                        form = new IndividualEdit(id);
                        break;

                    case cls_App.ReferenceBooks.TaskCourier:
                        form = new TaskCourierEdit(id);
                        break;


                    case cls_App.ReferenceBooks.TaskRouteSheetv2:
                        form = new TaskRouteSheetv2Edit(id);
                        break;

                    case cls_App.ReferenceBooks.Mailbox:
                        form = new MailboxEdit(id);
                        break;

                    case cls_App.ReferenceBooks.LetterTemplate:
                        form = new LetterTemplateEdit(id);
                        break;

                    case cls_App.ReferenceBooks.ColorStatus:
                        form = new ColorStatusEdit(id);
                        break;

                    case cls_App.ReferenceBooks.RouteSheet:
                        form = new RouteSheetEdit(id);
                        break;

                    case cls_App.ReferenceBooks.RouteSheetv2:
                        form = new RouteSheetv2Edit(id);
                        break;

                    case cls_App.ReferenceBooks.Position:
                        form = new PositionEdit(id);
                        break;

                    case cls_App.ReferenceBooks.PayoutDictionary:
                        form = new PayoutDictionaryEdit(id);
                        break;

                    case cls_App.ReferenceBooks.StatutoryDocument:
                        form = new ReferenceEdit<StatutoryDocument>(id);
                        break;

                    case cls_App.ReferenceBooks.TitleDocument:
                        form = new ReferenceEdit<TitleDocument>(id);
                        break;

                    case cls_App.ReferenceBooks.TaxReportingDocument:
                        form = new ReferenceEdit<TaxReportingDocument>(id);
                        break;

                    case cls_App.ReferenceBooks.SourceDocument:
                        form = new ReferenceEdit<SourceDocument>(id);
                        break;

                    case cls_App.ReferenceBooks.EmployeeDetailsDocument:
                        form = new ReferenceEdit<EmployeeDetailsDocument>(id);
                        break;

                    case cls_App.ReferenceBooks.CustomerFilter:
                        form = new CustomersFilterForm(id);
                        break;

                    case cls_App.ReferenceBooks.SectionOKVED2:
                        form = new SectionOKVED2Edit(id);
                        break;

                    case cls_App.ReferenceBooks.ClassOKVED2:
                        form = new ClassOKVED2Edit(id);
                        break;

                    case cls_App.ReferenceBooks.EntrepreneurialActivityCodesUTII:
                        form = new EntrepreneurialActivityCodesUTIIEdit(id);
                        break;

                    case cls_App.ReferenceBooks.PerformanceIndicator:
                        form = new PerformanceIndicatorEdit(id);
                        break;

                    case cls_App.ReferenceBooks.GroupPerformanceIndicator:
                        form = new GroupPerformanceIndicatorEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CalculationScale:
                        form = new CalculationScaleEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CostItem:
                        form = new CostItemEdit(id);
                        break;

                    case cls_App.ReferenceBooks.LetterFilter:
                        form = new LetterFilterEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Organization:
                        form = new OrganizationEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CustomerSettings:
                        form = new CustomerSettingsEdit(id);
                        break;

                    case cls_App.ReferenceBooks.PlateTemplate:
                        form = new PlateTemplateEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Status:
                        form = new StatusEdit<Status>(id);
                        break;

                    case cls_App.ReferenceBooks.StatusAccrual:
                        form = new StatusEdit<StatusAccrual>(id);
                        break;

                    case cls_App.ReferenceBooks.Document:
                        form = new DocumentEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CustomerStaff:
                        form = new CustomerStaffEdit(id, oPar3);
                        break;

                    case cls_App.ReferenceBooks.ContractStatus:
                        form = new StatusEdit<ContractStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.PatentStatus:
                        form = new StatusEdit<PatentStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.TaskStatus:
                        form = new StatusEdit<TaskStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.DealStatus:
                        form = new StatusEdit<DealStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.PackageDocumentStatus:
                        form = new StatusEdit<PackageDocumentStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.AccountStatementStatus:
                        form = new StatusEdit<AccountStatementStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.AccountingInsuranceStatus:
                        form = new StatusEdit<AccountingInsuranceStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.IndividualEntrepreneursTaxStatus:
                        form = new StatusEdit<IndividualEntrepreneursTaxStatus>(id);
                        break;

                    case cls_App.ReferenceBooks.StatusPreTax:
                        form = new StatusEdit<StatusPreTax>(id);
                        break;

                    case cls_App.ReferenceBooks.FormCorporation:
                        form = new FormCorporationEdit(id);
                        break;

                    case cls_App.ReferenceBooks.GeneralVocabulary:
                        form = new GeneralVocabularyEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CalculatorTaxSystem:
                        form = new CalculatorTaxSystemEdit(id);
                        break;

                    case cls_App.ReferenceBooks.CalculatorIndicator:
                        form = new CalculatorIndicatorEdit(id);
                        break;

                    case cls_App.ReferenceBooks.VacationType:
                        form = new VacationTypeEdit(id);
                        break;

                    case cls_App.ReferenceBooks.PhysicalIndicator:
                        form = new PhysicalIndicatorEdit(id);
                        break;

                    case cls_App.ReferenceBooks.ArchiveFolder:
                        form = new ArchiveFolderEdit(id);
                        break;

                    case cls_App.ReferenceBooks.TypePayment:
                        form = new TypePaymentEdit(id);
                        break;

                    case cls_App.ReferenceBooks.PriceList:
                        form = new PriceListEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Privilege:
                        form = new PrivilegeEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Report:
                        form = new ReportEdit(id);
                        break;

                    case cls_App.ReferenceBooks.ReportV2:
                        form = new ReportEditV2(id);
                        break;

                    case cls_App.ReferenceBooks.TaskObject:
                        form = new TaskObjectEdit(id);
                        break;

                    case cls_App.ReferenceBooks.ElectronicReporting:
                        form = new ElectronicReportingEdit(id);
                        break;

                    case cls_App.ReferenceBooks.ElectronicDocumentManagement:
                        form = new ElectronicDocumentManagementEdit(id);
                        break;

                    case cls_App.ReferenceBooks.TaxSystem:
                        form = new TaxSystemEdit(id);
                        break;

                    case cls_App.ReferenceBooks.User:
                        form = new UserEdit(id);
                        break;

                    case cls_App.ReferenceBooks.UserGroup:
                        form = new UserGroupEdit(id);
                        break;

                    case cls_App.ReferenceBooks.Bank:
                        form = new BankEdit(id);
                        break;
                }
            }
            return form;
        }

        /// <summary>
        /// Проверяет на возможность удаления строки Справочника
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="id">Oid</param>
        /// <param name="isMessageNotEmpty">флаг отображения сообщения об ошибке</param>
        /// <returns>true/false (удалять/не удалять)</returns>
        public async System.Threading.Tasks.Task<bool> CheckForDeleteAsync(Session session, int id, bool isMessageNotEmpty = true)
        {
            if (session == null)
            {
                session = DatabaseConnection.GetWorkSession();
            }

            bool isDelete = true;
            var message = default(string);

            using (var uof = new UnitOfWork())
            {
                var currentUser = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
                if (currentUser != null && currentUser.flagAdministrator is false)
                {
                    message = $"Удаление объектов из словарей разрешено только администраторам.{Environment.NewLine}" +
                        $"У пользователя [{currentUser}] недостаточно прав для совершения данной операции.";
                }
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                message = await CheckingElementBeforeDelete(session, id, message);
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                isDelete = false;

                if (isMessageNotEmpty)
                {
                    XtraMessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return isDelete;
        }

        /// <summary>
        /// Проверка элемента перед удалением из словаря. 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task<string> CheckingElementBeforeDelete(Session session, int id, string message)
        {
            switch ((cls_App.ReferenceBooks)SprVariant)
            {
                case cls_App.ReferenceBooks.CustomerStaff:
                    message = await CheckingCustomerStaff(id, message);
                    break;

                case cls_App.ReferenceBooks.PackageDocumentStatus:
                    message = await CheckingPackageDocumentStatus(id, message);
                    break;

                case cls_App.ReferenceBooks.Document:
                    message = await CheckingDocument(id, message);
                    break;

                case cls_App.ReferenceBooks.Report:
                    message = await CheckingReport(id, message);
                    break;

                case cls_App.ReferenceBooks.ReportV2:
                    //TODO: проверка на удаление
                    //message = await CheckingReport(id, message);
                    break;

                case cls_App.ReferenceBooks.TaxSystem:
                    message = await CheckingTaxSystem(id, message);
                    break;

                case cls_App.ReferenceBooks.User:
                    message = await CheckingUser(id, message);
                    break;

                case cls_App.ReferenceBooks.Staff:
                    message = await CheckingStaff(id, message);
                    break;

                case cls_App.ReferenceBooks.ElectronicReporting:
                    message = await CheckingElectronicReporting(id, message);
                    break;

                case cls_App.ReferenceBooks.UserGroup:
                    var userGroup = session.GetObjectByKey<UserGroup>(id);
                    if (userGroup != null)
                    {
                        if (session.FindObject<UserGroups>(new BinaryOperator(nameof(UserGroups.UserGroup), id), false) != null)
                        {
                            if (XtraMessageBox.Show($"Некоторые пользователи принадлежат группе {userGroup}. Продолжить удаление?",
                                    "Удаление группы пользователей",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                var xpCollectionUser = new XPCollection<User>(session);

                                foreach (var itemUser in xpCollectionUser)
                                {
                                    var userGroups = itemUser.UserGroups.FirstOrDefault(f => f.UserGroup == userGroup);
                                    if (userGroups != null)
                                    {
                                        userGroups.Delete();
                                        itemUser.Save();
                                    }
                                }
                            }
                            else
                            {
                                message = $"Удаление группы пользователей {userGroup} отменено.";
                                break;
                            }
                        }
                    }
                    else
                    {
                        message = $"Ошибка удаления группы пользователей. Группа не найдена.";
                    }
                    break;

                case cls_App.ReferenceBooks.TaskStatus:
                    var taskStatus = session.GetObjectByKey<TaskStatus>(id);
                    if (taskStatus != null && taskStatus.IsProtectionDelete)
                    {
                        message = $"Запись защищена от удаления.{Environment.NewLine}Удаление запрещено.";
                    }
                    break;
            }

            return message;
        }

        public static async System.Threading.Tasks.Task<string> CheckingStaff(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var staff = await uof.GetObjectByKeyAsync<Staff>(id);
                if (staff != null)
                {
                    var customerObj = await new XPQuery<Customer>(uof)
                        .Where(w => (w.AccountantResponsible != null && w.AccountantResponsible.Oid == staff.Oid)
                                    || (w.BankResponsible != null && w.BankResponsible.Oid == staff.Oid)
                                    || (w.PrimaryResponsible != null && w.PrimaryResponsible.Oid == staff.Oid)
                                    || (w.SalaryResponsible != null && w.SalaryResponsible.Oid == staff.Oid))
                        .FirstOrDefaultAsync();

                    if (customerObj != null)
                    {
                        message = $"Удаление сотрудника [{staff}] невозможно.{Environment.NewLine}Запись используется у следующего клиента: {customerObj}";
                    }
                }
            }

            return message;
        }

        public static async System.Threading.Tasks.Task<string> CheckingElectronicReporting(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var obj = await uof.GetObjectByKeyAsync<ElectronicReporting>(id);
                if (obj != null)
                {
                    var electronicReportingСustomerObject = await new XPQuery<ElectronicReportingСustomerObject>(uof)
                        ?.FirstOrDefaultAsync(w => w.ElectronicReporting != null && w.ElectronicReporting.Oid == id);

                    if (electronicReportingСustomerObject != null)
                    {
                        message = $"Удаление электронной отчетности [{obj}] невозможно.{Environment.NewLine}" +
                            $"Запись используется в объектах электронной отчетности у клиента(ов).";
                        var customerName = electronicReportingСustomerObject?.ElectronicReportingCustomer?.Customer?.ToString();
                        if (!string.IsNullOrWhiteSpace(customerName))
                        {
                            message += $"{Environment.NewLine}{Environment.NewLine}Клиент: {customerName}";
                        }
                        return message;
                    }

                    var customerFilterElectronicReporting = await new XPQuery<CustomerFilterElectronicReporting>(uof)
                        ?.FirstOrDefaultAsync(w => w.ElectronicReporting != null && w.ElectronicReporting.Oid == id);

                    if (customerFilterElectronicReporting != null)
                    {
                        message = $"Удаление электронной отчетности [{obj}] невозможно.{Environment.NewLine}" +
                            $"Запись используется в настройке клиентских фильтров.";
                        return message;
                    }                    
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingUser(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var user = await uof.GetObjectByKeyAsync<User>(id);
                if (user != null && user.Staff != null)
                {
                    var staff = user.Staff;
                    if (staff != null)
                    {
                        var customerObj = await new XPQuery<Customer>(uof)
                            .Where(w => (w.AccountantResponsible != null && w.AccountantResponsible.Oid == staff.Oid)
                                        || (w.BankResponsible != null && w.BankResponsible.Oid == staff.Oid)
                                        || (w.PrimaryResponsible != null && w.PrimaryResponsible.Oid == staff.Oid)
                                        || (w.SalaryResponsible != null && w.SalaryResponsible.Oid == staff.Oid))
                            .FirstOrDefaultAsync();

                        if (customerObj != null)
                        {
                            message = $"Удаление пользователя [{user}] невозможно.{Environment.NewLine}Запись используется у следующего клиента: {customerObj}";
                        }
                    }
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingTaxSystem(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var taxSystem = await uof.GetObjectByKeyAsync<TaxSystem>(id);
                if (taxSystem != null)
                {
                    var taxSystemObj = await new XPQuery<TaxSystemCustomerObject>(uof)
                        .Where(w => w.TaxSystem != null && w.TaxSystem.Oid == taxSystem.Oid)
                        .FirstOrDefaultAsync();
                    if (taxSystemObj != null)
                    {
                        message = $"Удаление системы налогообложения [{taxSystem}] невозможно.";

                        var taxSystemCustomer = taxSystemObj.TaxSystemCustomer;
                        if (taxSystemCustomer != null)
                        {
                            var customer = await new XPQuery<Customer>(uof)
                                .FirstOrDefaultAsync(f => f.TaxSystemCustomer != null && f.TaxSystemCustomer.Oid == taxSystemCustomer.Oid);
                            if (customer != null)
                            {
                                message += $"{Environment.NewLine}Запись используется у следующего клиента: {customer}";
                            }
                        }
                    }
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingReport(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var report = await uof.GetObjectByKeyAsync<Report>(id);
                if (report != null)
                {
                    var customerReport = await new XPQuery<CustomerReport>(uof)
                        .Where(w => w.Report != null && w.Report.Oid == report.Oid)
                        .FirstOrDefaultAsync();
                    if (customerReport != null)
                    {
                        message = $"Удаление отчета [{report}] невозможно.";

                        var customer = customerReport.Customer;
                        if (customer != null)
                        {
                            message += $"{Environment.NewLine}Запись используется у следующего клиента: {customer}";
                        }
                    }

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        var reportChange = await new XPQuery<ReportChange>(uof)
                        .Where(w => w.Report != null && w.Report.Oid == report.Oid)
                        .LastOrDefaultAsync();

                        if (reportChange != null)
                        {
                            message = $"Удаление отчета [{report}] невозможно. {Environment.NewLine}Запись используется в отчетах для сдачи за {reportChange.Year} год.";
                        }
                    }

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        var statisticalReport = await new XPQuery<StatisticalReport>(uof)
                        .Where(w => w.Report != null && w.Report.Oid == report.Oid)
                        .LastOrDefaultAsync();

                        if (statisticalReport != null)
                        {
                            message = $"Удаление отчета [{report}] невозможно. {Environment.NewLine}Запись используется в статистической отчетности.";
                        }
                    }
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingCustomerStaff(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var customerStaff = await uof.GetObjectByKeyAsync<CustomerStaff>(id);
                if (customerStaff != null)
                {
                    var packageDocumentType = await new XPQuery<PackageDocumentType>(uof)
                        .Where(w => w.CustomerStaff != null && w.CustomerStaff.Oid == customerStaff.Oid)
                        .FirstOrDefaultAsync();
                    if (packageDocumentType != null)
                    {
                        message = $"Удаление сотрудника [{customerStaff}] невозможно.";
                        message += $"{Environment.NewLine}Запись используется для пакета документов.";
                    }
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingPackageDocumentStatus(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var packageDocumentStatus = await uof.GetObjectByKeyAsync<PackageDocumentStatus>(id);
                if (packageDocumentStatus != null)
                {
                    var packageDocumentType = await new XPQuery<PackageDocumentType>(uof)
                        .Where(w => w.PackageDocumentStatus != null && w.PackageDocumentStatus.Oid == packageDocumentStatus.Oid)
                        .FirstOrDefaultAsync();
                    if (packageDocumentType != null)
                    {
                        message = $"Удаление статуса [{packageDocumentStatus}] невозможно.";
                        message += $"{Environment.NewLine}Запись используется для пакета документов.";
                    }
                }
            }

            return message;
        }

        private static async System.Threading.Tasks.Task<string> CheckingDocument(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var document = await uof.GetObjectByKeyAsync<Document>(id);
                if (document != null)
                {
                    var packageDocumentType = await new XPQuery<PackageDocumentType>(uof)
                        .Where(w => w.Document != null && w.Document.Oid == document.Oid)
                        .FirstOrDefaultAsync();
                    if (packageDocumentType != null)
                    {
                        message = $"Удаление документа [{document}] невозможно.";
                        message += $"{Environment.NewLine}Запись используется для пакета документов.";
                    }
                }
            }

            return message;
        }

        public static int RunBaseSpr(int iSprVariant, int id = -1, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null,
            DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            int index = -1;

            form_BaseSpr frm = new form_BaseSpr(iSprVariant, id, iVerSpr, criteria, sort, image_collection, image_field, oPar1, oPar2, oPar3);

            frm.ShowDialog();
            if (frm.FlagOK)
            {
                index = frm.Id;
            }

            return index;
        }

        public static IEnumerable<int> GetNumbersObjFromDirectory(int iSprVariant, int id = -1, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null,
            DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            var list = new List<int>();

            var form = new form_BaseSpr(iSprVariant, id, iVerSpr, criteria, sort, image_collection, image_field, oPar1, oPar2, oPar3);
            form.SetMultiSelectGridView();
            form.ShowDialog();
            if (form.FlagOK)
            {
                list = form.Oids;
            }

            return list;
        }

        public static CriteriaOperator GetCriteriaDateRange(DateTime date = new DateTime(), string field_date_beg = "date_beg", string field_date_end = "date_end")
        {
            CriteriaOperator criteria = null;

            DateTime dd = new DateTime();
            dd = (date != DateTime.MinValue) ? date.Date : new DateTime(BVVGlobal.oApp.WorkDate.Year, BVVGlobal.oApp.WorkDate.Month, 1).AddMonths(1).AddDays(-1).Date; // последний день отчетного месяца отчетного года

            criteria = CriteriaOperator.Parse("(GetDate(" + field_date_beg + ")<=? or IsNullOrEmpty(" + field_date_beg + ")) and (GetDate(" + field_date_end + ")>=? or IsNullOrEmpty(" + field_date_end + "))", dd, dd);

            return criteria;
        }

        public static CriteriaOperator GetCriteriaDateCheckPeriod(DateTime d1, DateTime d2, string field_date_beg = "date_beg", string field_date_end = "date_end")
        {
            // Плохие ситуации:                (ищем их)
            // isnull(beg)    - isnull(end)
            //  d1 <= beg     -      end <= d2
            //        beg  <= d1 <=  end
            //        beg  <= d2 <=  end
            CriteriaOperator criteria = CriteriaOperator.Parse($"((IsNullOrEmpty({field_date_beg}) and IsNullOrEmpty({field_date_end})) or (GetDate({field_date_beg})>=? and GetDate({field_date_end})<=?) or ((GetDate({field_date_beg})<=? or IsNullOrEmpty({field_date_beg})) and (GetDate({field_date_end})>=? or IsNullOrEmpty({field_date_end}))) or ((GetDate({field_date_beg})<=? or IsNullOrEmpty({field_date_beg})) and (GetDate({field_date_end})>=? or IsNullOrEmpty({field_date_end}))))",
                                       d1, d2, d1, d1, d2, d2);

            return criteria;
        }

        public static SortProperty[] ConvertSortingCollectionToSortPropertyArray(SortingCollection sort_coll)
        {
            SortProperty[] sort_arr = null;
            if (sort_coll != null && sort_coll.Count > 0)
            {
                sort_arr = new SortProperty[sort_coll.Count];
                for (int i = 0; i < sort_coll.Count; i++) sort_arr[i] = sort_coll[i];
            }
            return sort_arr;
        }

        /// <summary>
        /// Base version: Заполняет ComboBoxEdit
        /// </summary>
        /// <typeparam name="T">таблица</typeparam>
        /// <param name="sess">сессия</param>
        /// <param name="combo">что заполнить</param>
        /// <param name="find_oid">искомый элемент (для позиционирования)</param>
        /// <param name="iSprVariant">если set_SprEnumeration - то Group</param>
        /// <param name="criteria">критерий, если есть</param>
        /// <param name="sort">сортировка</param>
        /// <returns>Oid, либо по default-у, либо найденный при заданном поисковом</returns>
        public static int FillComboBoxBase<T>(Session sess, ComboBoxEdit combo, int find_oid = -1, int iSprVariant = -1, CriteriaOperator criteria = null, SortingCollection sort = null) where T : IXPObject
        { return cls_BaseSpr.FillComboBoxBase(sess, typeof(T), combo, find_oid, iSprVariant, criteria, sort); }
        public static int FillComboBoxBase(Session sess, Type type, ComboBoxEdit combo, int find_oid = -1, int iSprVariant = -1, CriteriaOperator criteria = null, SortingCollection sort = null)
        {
            if (ReferenceEquals(criteria, null) && (type.Name == "set_SprEnumeration")) criteria = new BinaryOperator("Group", iSprVariant);

            int i_count = 0, i_default = 0;
            combo.Properties.Items.Clear();

            XPCollection coll = new XPCollection(sess, type, criteria, cls_BaseSpr.ConvertSortingCollectionToSortPropertyArray(sort));
            foreach (var o_o in coll)
            {
                combo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ComboBoxItem(o_o));

                if (find_oid == -1)
                {
                    var fldef = false;

                    try
                    {
                        fldef = (bool)o_o.GetType().InvokeMember("fl_def", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                    }
                    catch (Exception)
                    {
                        fldef = false;
                    }

                    if (fldef == true)
                    {
                        i_default = i_count;
                    }
                }
                else
                {
                    var oid = (int)o_o.GetType().InvokeMember(nameof(XPObject.Oid), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);

                    if (find_oid == oid)
                    {
                        i_default = i_count;
                    }
                }
                i_count++;
            }
            return i_default;
        }

        /// <summary>
        /// Base version: Заполняет ImageComboBoxEdit
        /// </summary>
        /// <typeparam name="T">таблица</typeparam>
        /// <param name="sess">сессия</param>
        /// <param name="img_combo">что заполнить</param>
        /// <param name="find_oid">искомый элемент (для позиционирования)</param>
        /// <param name="iSprVariant">если set_SprEnumeration - то Group</param>
        /// <param name="criteria">критерий, если есть</param>
        /// <param name="sort">сортировка</param>
        /// <returns>Oid, либо по default-у, либо найденный при заданном поисковом</returns>
        public static int FillImageComboBoxBase<T>(Session sess, ImageComboBoxEdit img_combo, int find_oid = -1, int iSprVariant = -1, CriteriaOperator criteria = null, SortingCollection sort = null) where T : IXPObject
        { return cls_BaseSpr.FillImageComboBoxBase(sess, typeof(T), img_combo, find_oid, iSprVariant, criteria, sort); }
        public static int FillImageComboBoxBase(Session sess, Type type, ImageComboBoxEdit img_combo, int find_oid = -1, int iSprVariant = -1, CriteriaOperator criteria = null, SortingCollection sort = null)
        {
            if (ReferenceEquals(criteria, null) && (type.Name == "set_SprEnumeration")) criteria = new BinaryOperator("Group", iSprVariant);

            int i_count = 0, i_default = 0;
            img_combo.Properties.Items.Clear();

            XPCollection coll = new XPCollection(sess, type, criteria, cls_BaseSpr.ConvertSortingCollectionToSortPropertyArray(sort));
            foreach (var o_o in coll)
            {
                var imageIndex = (int)o_o.GetType().InvokeMember("ImageIndex", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);

                img_combo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(o_o, imageIndex));

                if (find_oid == -1)
                {
                    /*
                     *  В новых классах нет fl_def, поэтому ошибку пропустим.
                     */

                    var fldef = false;

                    try
                    {
                        fldef = (bool)o_o.GetType().InvokeMember("fl_def", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                    }
                    catch (Exception)
                    {
                        fldef = false;
                    }

                    if (fldef == true)
                    {
                        i_default = i_count;
                    }
                }
                else
                {
                    var oid = (int)o_o.GetType().InvokeMember(nameof(XPObject.Oid), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);

                    if (find_oid == oid)
                    {
                        i_default = i_count;
                    }
                }
                i_count++;
            }
            return i_default;
        }

        public static void ComboBoxButtonClickBase<T>(Session sess, ComboBoxEdit combo, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null) where T : IXPObject
        { cls_BaseSpr.ComboBoxButtonClickBase(sess, typeof(T), combo, iSprVariant, iVerSpr, criteria, sort, flNoFillSet, image_collection, image_field, oPar1, oPar2, oPar3); }
        public static void ComboBoxButtonClickBase(Session sess, Type type, ComboBoxEdit combo, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            try
            {
                int id = -1;

                if (combo.EditValue != null)
                {
                    if (combo.SelectedIndex >= 0)
                    {
                        id = ((XPObject)combo.Properties.Items[combo.SelectedIndex]).Oid;
                    }
                    else
                    {
                        id = ((XPObject)combo.EditValue).Oid;
                    }
                }

                int id_return = cls_BaseSpr.RunBaseSpr(iSprVariant, id, iVerSpr, criteria, sort, image_collection, image_field, oPar1, oPar2, oPar3);

                if (id_return != -1 && !(flNoFillSet))
                {
                    if ((sess == null) || (!sess.IsConnected))
                    {
                        sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                    }

                    var o_o = sess.GetObjectByKey(type, id_return, true);

                    if (o_o != null)
                    {
                        var oid = (int)o_o.GetType().InvokeMember(nameof(XPObject.Oid), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);
                        combo.SelectedIndex = cls_BaseSpr.FillComboBoxBase(sess, type, combo, oid, iSprVariant, criteria, sort);
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ImageComboBoxButtonClickBase<T>(Session sess, ImageComboBoxEdit img_combo, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null) where T : IXPObject
        { cls_BaseSpr.ImageComboBoxButtonClickBase(sess, typeof(T), img_combo, iSprVariant, iVerSpr, criteria, sort, flNoFillSet, image_collection, image_field, oPar1, oPar2, oPar3); }
        public static void ImageComboBoxButtonClickBase(Session sess, Type type, ImageComboBoxEdit img_combo, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            try
            {
                int id = -1;

                if (img_combo.EditValue != null)
                { // Может вообще не нужно проверять на SelectedIndex, достаточно и EditValue
                    if (img_combo.SelectedIndex >= 0)
                    {
                        id = ((XPObject)img_combo.Properties.Items[img_combo.SelectedIndex].Value).Oid;
                    }
                    else
                    {
                        id = ((XPObject)img_combo.EditValue).Oid;
                    }
                }

                int id_return = cls_BaseSpr.RunBaseSpr(iSprVariant, id, iVerSpr, criteria, sort, image_collection, image_field, oPar1, oPar2, oPar3);

                if (id_return != -1 && !(flNoFillSet))
                {
                    if ((sess == null) || (!sess.IsConnected))
                    {
                        sess = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                    }

                    var o_o = sess.GetObjectByKey(type, id_return, true);

                    var oid = (int)o_o.GetType().InvokeMember(nameof(XPObject.Oid), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, o_o, null);

                    if (o_o != null)
                    {
                        img_combo.SelectedIndex = cls_BaseSpr.FillImageComboBoxBase(sess, type, img_combo, oid, iSprVariant, criteria, sort);
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ButtonEditButtonClickBase<T>(T obj, Session sess, ButtonEdit btnedit, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null) where T : IXPObject
        { ButtonEditButtonClickBase(sess, obj.GetType(), btnedit, iSprVariant, iVerSpr, criteria, sort, flNoFillSet, image_collection, image_field, oPar1, oPar2, oPar3); }

        public static void ButtonEditButtonClickBase<T>(Session sess, ButtonEdit btnedit, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null) where T : IXPObject
        { ButtonEditButtonClickBase(sess, typeof(T), btnedit, iSprVariant, iVerSpr, criteria, sort, flNoFillSet, image_collection, image_field, oPar1, oPar2, oPar3); }

        public async static void ButtonEditButtonClickBase(Session sess, Type type, ButtonEdit btnedit, int iSprVariant, int iVerSpr = 1, CriteriaOperator criteria = null, SortingCollection sort = null, bool flNoFillSet = false, DevExpress.Utils.ImageCollection image_collection = null, string image_field = "", object oPar1 = null, object oPar2 = null, object oPar3 = null)
        {
            try
            {
                int id = -1;

                if (iSprVariant == (int)cls_App.ReferenceBooks.Customer)
                {
                    criteria = await GetCustomerCriteria(criteria);
                }

                if (btnedit.EditValue != null && !string.IsNullOrWhiteSpace(btnedit.Text))
                {
                    id = (int)btnedit.EditValue.GetType().InvokeMember(nameof(XPObject.Oid), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, btnedit.EditValue, null);
                }

                int id_return = cls_BaseSpr.RunBaseSpr(iSprVariant, id, iVerSpr, criteria, sort, image_collection, image_field, oPar1, oPar2, oPar3);
                if (id_return != -1 && !(flNoFillSet))
                {
                    if ((sess == null) || (!sess.IsConnected))
                    {
                        sess = DatabaseConnection.GetWorkSession();
                    }

                    var o_o = sess.GetObjectByKey(type, id_return, true);

                    if (o_o != null && btnedit.Properties.ReadOnly == false)
                    {
                        btnedit.EditValue = o_o;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public async static System.Threading.Tasks.Task<GroupOperator> GetStaffCriteria(Session session, GroupOperator criteria, string xpObject)
        {
            if (string.IsNullOrWhiteSpace(xpObject))
            {
                return criteria;
            }

            try
            {
                var user = await session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
                user?.Reload();
                if (user != null)
                {
                    var staff = user?.Staff;
                    if (staff != null)
                    {
                        var groupCiretiaStaff = new GroupOperator(GroupOperatorType.Or);
                        GetGroupCriteriaStaff(groupCiretiaStaff, staff, xpObject);
                        await GetReplacementWorker(session, staff, groupCiretiaStaff, xpObject);

                        if (string.IsNullOrWhiteSpace(criteria?.LegacyToString()) || criteria?.LegacyToString() == "()")
                        {
                            criteria = groupCiretiaStaff;
                        }
                        else
                        {
                            var group = new GroupOperator(GroupOperatorType.Or);
                            group.Operands.Add(criteria);
                            group.Operands.Add(groupCiretiaStaff);
                            criteria = group;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return criteria;
        }

        /// <summary>
        /// Возвращает только клиентов сотрудников.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async static System.Threading.Tasks.Task<CriteriaOperator> GetCustomerCriteria(CriteriaOperator criteria, string xpObject = default, string xpCollection = default)
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
                    user?.Reload();
                    if (user != null && !user.flagAdministrator)
                    {
                        var staff = user?.Staff;
                        if (staff != null)
                        {
                            var groupCiretiaStaff = new GroupOperator(GroupOperatorType.Or);
                            if (!string.IsNullOrWhiteSpace(xpObject) && !xpObject.EndsWith("."))
                            {
                                xpObject += ".";
                            }

                            if (string.IsNullOrWhiteSpace(xpCollection))
                            {
                                GetGroupCriteriaStaff(xpObject, staff, groupCiretiaStaff);
                                await GetReplacementWorker(xpObject, staff, groupCiretiaStaff);
                                groupCiretiaStaff = await GetCriteriaStatusCustomer(xpObject, groupCiretiaStaff);
                            }
                            else
                            {
                                GetGroupCriteriaStaffContains(xpObject, xpCollection, staff, groupCiretiaStaff);
                                await GetReplacementWorker(xpObject, staff, groupCiretiaStaff, xpCollection);
                                groupCiretiaStaff = await GetCriteriaStatusCustomer(xpObject, groupCiretiaStaff, xpCollection);
                            }

                            if (string.IsNullOrWhiteSpace(criteria?.LegacyToString()) || criteria?.LegacyToString() == "()")
                            {
                                criteria = groupCiretiaStaff;
                            }
                            else
                            {
                                var group = new GroupOperator(GroupOperatorType.And);
                                group.Operands.Add(criteria);
                                group.Operands.Add(groupCiretiaStaff);
                                criteria = group;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return criteria;
        }

        private static async System.Threading.Tasks.Task<GroupOperator> GetCriteriaStatusCustomer(string xpObject, GroupOperator groupCiretiaStaff, string xpCollection = default)
        {
            using (var uof = new UnitOfWork())
            {
                var customerStatus = await uof.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Не работаем"));
                if (customerStatus != null)
                {
                    var groupOperatorCustomer = new GroupOperator(GroupOperatorType.And);
                    groupOperatorCustomer.Operands.Add(groupCiretiaStaff);

                    if (string.IsNullOrWhiteSpace(xpCollection))
                    {
                        var criteriaCustomerStatus = new NotOperator(new BinaryOperator($"{xpObject}{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.Oid)}", customerStatus.Oid));
                        groupOperatorCustomer.Operands.Add(criteriaCustomerStatus);
                    }
                    else
                    {
                        var criteriaCustomerStatus = new ContainsOperator(xpCollection, new NotOperator(new BinaryOperator($"{xpObject}{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.Oid)}", customerStatus.Oid)));
                        groupOperatorCustomer.Operands.Add(criteriaCustomerStatus);
                    }

                    groupCiretiaStaff = groupOperatorCustomer;
                }
            }

            return groupCiretiaStaff;
        }

        /// <summary>
        /// Сотрудники, которых подменяет текущий в отпуске.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="xpObject"></param>
        /// <param name="staff"></param>
        /// <param name="groupCiretiaStaff"></param>
        /// <returns></returns>
        private static async System.Threading.Tasks.Task GetReplacementWorker(Session session, Staff staff, GroupOperator groupCiretiaStaff, string xpObject, string xpCollection = default)
        {
            var currentDateTime = DateTime.Now.Date;
            var vacations = await new XPQuery<Vacation>(session)
                .Where(w => w.IsConfirm
                    && w.DateSince <= currentDateTime
                    && w.DateTo >= currentDateTime)
                .ToListAsync();

            vacations = vacations
                .Where(w => w.VacationReplacementStaffs != null
                    && w.VacationReplacementStaffs
                    .Where(wS => wS.Staff != null && wS.Staff == staff)
                    .ToList().Count > 0)
                .ToList();

            if (vacations != null)
            {
                var staffs = vacations.Select(s => s.Staff).Distinct();

                if (staffs != null)
                {
                    foreach (var obj in staffs)
                    {
                        if (string.IsNullOrWhiteSpace(xpCollection))
                        {
                            GetGroupCriteriaStaff(groupCiretiaStaff, obj, xpObject);
                        }
                        else
                        {
                            GetGroupCriteriaStaffContains(groupCiretiaStaff, obj, xpObject, xpCollection);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сотрудники, которых подменяет текущий в отпуске.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="xpObject"></param>
        /// <param name="staff"></param>
        /// <param name="groupCiretiaStaff"></param>
        /// <returns></returns>
        private static async System.Threading.Tasks.Task GetReplacementWorker(string xpObject, Staff staff, GroupOperator groupCiretiaStaff, string xpCollection = default)
        {
            using (var uof = new UnitOfWork())
            {
                var currentDateTime = DateTime.Now.Date;
                var vacations = await new XPQuery<Vacation>(uof)
                    .Where(w => w.IsConfirm
                        && w.DateSince <= currentDateTime
                        && w.DateTo >= currentDateTime)
                    .ToListAsync();

                vacations = vacations
                    .Where(w => w.VacationReplacementStaffs != null
                        && w.VacationReplacementStaffs
                        .Where(wS => wS.Staff != null && wS.Staff.Oid == staff.Oid)
                        .ToList().Count > 0)
                    .ToList();

                if (vacations != null)
                {
                    var staffs = vacations.Select(s => s.Staff).Distinct();

                    if (staffs != null)
                    {
                        foreach (var obj in staffs)
                        {
                            if (string.IsNullOrWhiteSpace(xpCollection))
                            {
                                GetGroupCriteriaStaff(xpObject, obj, groupCiretiaStaff);
                            }
                            else
                            {
                                GetGroupCriteriaStaffContains(xpObject, xpCollection, obj, groupCiretiaStaff);
                            }
                        }

                    }
                }
            }
        }

        private static void GetGroupCriteriaStaff(GroupOperator groupCriteria, Staff staff, string xpObject)
        {
            var criteria = new BinaryOperator($"{xpObject}.{nameof(XPObject.Oid)}", staff.Oid);
            groupCriteria.Operands.Add(criteria);
        }

        private static void GetGroupCriteriaStaffContains(GroupOperator groupCriteria, Staff staff, string xpObject, string xpCollection)
        {
            var criteria = new ContainsOperator(xpCollection, new BinaryOperator($"{xpObject}.{nameof(XPObject.Oid)}", staff.Oid));
            groupCriteria.Operands.Add(criteria);
        }

        private static void GetGroupCriteriaStaff(string xpObject, Staff staff, GroupOperator groupCiretiaStaff)
        {
            var crieriaAccountantResponsible = new BinaryOperator($"{xpObject}{nameof(Customer.AccountantResponsible)}.{nameof(XPObject.Oid)}", staff.Oid);
            groupCiretiaStaff.Operands.Add(crieriaAccountantResponsible);

            var crieriaBankResponsible = new BinaryOperator($"{xpObject}{nameof(Customer.BankResponsible)}.{nameof(XPObject.Oid)}", staff.Oid);
            groupCiretiaStaff.Operands.Add(crieriaBankResponsible);

            var crieriaPrimaryResponsible = new BinaryOperator($"{xpObject}{nameof(Customer.PrimaryResponsible)}.{nameof(XPObject.Oid)}", staff.Oid);
            groupCiretiaStaff.Operands.Add(crieriaPrimaryResponsible);

            var crieriaSalaryResponsible = new BinaryOperator($"{xpObject}{nameof(Customer.SalaryResponsible)}.{nameof(XPObject.Oid)}", staff.Oid);
            groupCiretiaStaff.Operands.Add(crieriaSalaryResponsible);
        }

        private static void GetGroupCriteriaStaffContains(string xpObject, string xpCollection, Staff staff, GroupOperator groupCiretiaStaff)
        {
            var criteriaNullCustomer = new ContainsOperator(xpCollection, new NullOperator(nameof(Customer)));
            groupCiretiaStaff.Operands.Add(criteriaNullCustomer);

            var crieriaAccountantResponsible = new ContainsOperator(xpCollection, new BinaryOperator($"{xpObject}{nameof(Customer.AccountantResponsible)}.{nameof(XPObject.Oid)}", staff.Oid));
            groupCiretiaStaff.Operands.Add(crieriaAccountantResponsible);

            var crieriaBankResponsible = new ContainsOperator(xpCollection, new BinaryOperator($"{xpObject}{nameof(Customer.BankResponsible)}.{nameof(XPObject.Oid)}", staff.Oid));
            groupCiretiaStaff.Operands.Add(crieriaBankResponsible);

            var crieriaPrimaryResponsible = new ContainsOperator(xpCollection, new BinaryOperator($"{xpObject}{nameof(Customer.PrimaryResponsible)}.{nameof(XPObject.Oid)}", staff.Oid));
            groupCiretiaStaff.Operands.Add(crieriaPrimaryResponsible);

            var crieriaSalaryResponsible = new ContainsOperator(xpCollection, new BinaryOperator($"{xpObject}{nameof(Customer.SalaryResponsible)}.{nameof(XPObject.Oid)}", staff.Oid));
            groupCiretiaStaff.Operands.Add(crieriaSalaryResponsible);
        }
    }
}

