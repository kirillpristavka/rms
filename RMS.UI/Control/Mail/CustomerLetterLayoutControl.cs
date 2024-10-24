using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Mail
{
    public partial class CustomerLetterLayoutControl : XtraUserControl
    {
        public delegate void FocusedNodeChangedEventHandler(GroupOperator groupOperator, TreeListNode treeListNode);
        public event FocusedNodeChangedEventHandler FocusedNodeChanged;

        public TreeList TreeList { get; }

        public CustomerLetterLayoutControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            
            TreeList = treeListCustomerLetter;

            CreateColumns();
            CreateNodes();
        }

        private bool IsFirstCreateNode;
        public void NullFocusedNode()
        {
            IsFirstCreateNode = true;

            Invoke((Action)delegate
            {
                TreeList.FocusedNode = null;
            });

            IsFirstCreateNode = false;
        }

        private void CreateColumns()
        {
            TreeList.BeginUpdate();

            TreeList.StateImageList = imageCollectionCustomerLetter;

            var columnName = TreeList.Columns.Add();
            columnName.Name = nameof(columnName);
            columnName.Caption = "Организация";
            columnName.VisibleIndex = 0;

            var columnDontRead = TreeList.Columns.Add();
            columnName.Name = nameof(columnDontRead);
            columnDontRead.Caption = "Не прочитано";
            columnDontRead.VisibleIndex = 1;
            columnDontRead.Width = 80;
            columnDontRead.OptionsColumn.FixedWidth = true; 
            columnDontRead.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnTotal = TreeList.Columns.Add();
            columnName.Name = nameof(columnTotal);
            columnTotal.Caption = "Всего";
            columnTotal.VisibleIndex = 2;
            columnTotal.Width = 80;
            columnTotal.OptionsColumn.FixedWidth = true;
            columnTotal.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            TreeList.EndUpdate();
        }
        
        public void CreateNodes()
        {
            IsFirstCreateNode = true;

            TreeList.BeginUnboundLoad();
            TreeList.ClearNodes();

            using (var uof = new UnitOfWork())
            {
                var criteriaCustomer = new NotOperator(new NullOperator(nameof(Customer.Email)));                
                var xpCollectionCustomer = new XPCollection<Customer>(uof, criteriaCustomer);

                foreach (var customer in xpCollectionCustomer.OrderBy(o => o.Name))
                {
                    var groupOperatorLetterSenderAddress = new GroupOperator(GroupOperatorType.Or);
                    foreach (var contact in customer.CustomerEmails)
                    {
                        if (!string.IsNullOrWhiteSpace(contact.Email))
                        {
                            var criteria = new BinaryOperator(nameof(Letter.LetterSenderAddress), contact.Email);
                            groupOperatorLetterSenderAddress.Operands.Add(criteria);
                        }
                    }
                    
                    var xpCollectionLetterCustomer = new XPCollection<Letter>(uof, groupOperatorLetterSenderAddress);

                    if (xpCollectionLetterCustomer != null && xpCollectionLetterCustomer.Count > 0)
                    {
                        TreeListNode parentForRootNodes = null;

                        var countDontReadParent = xpCollectionLetterCustomer.Count(c => !c.IsRead);
                        var countTotalParent = xpCollectionLetterCustomer.Count;

                        TreeListNode rootNode = TreeList.AppendNode(new object[] { customer, countDontReadParent, countTotalParent }, parentForRootNodes);
                        rootNode.StateImageIndex = 0;

                        foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
                        {
                            if (typeLetter == TypeLetter.InBox || typeLetter == TypeLetter.Outgoing)
                            {
                                var countDontRead = xpCollectionLetterCustomer.Where(w => w.TypeLetter == typeLetter).Count(c => !c.IsRead);
                                var countTotal = xpCollectionLetterCustomer.Where(w => w.TypeLetter == typeLetter).Count();
                                var imageIndex = -1;

                                switch (typeLetter)
                                {
                                    case TypeLetter.InBox:
                                        imageIndex = 1;
                                        break;

                                    case TypeLetter.Outgoing:
                                        imageIndex = 2;
                                        break;

                                    case TypeLetter.Basket:
                                        imageIndex = 3;
                                        break;

                                    case TypeLetter.Spam:
                                        imageIndex = 4;
                                        break;

                                    case TypeLetter.Draft:
                                        imageIndex = 5;
                                        break;
                                }

                                TreeList.AppendNode(new object[] { typeLetter.GetEnumDescription(),
                                            countDontRead,
                                            countTotal }, rootNode).StateImageIndex = imageIndex;
                            }                            
                        }
                    }

                    xpCollectionLetterCustomer?.Dispose();
                }                
            }
            
            TreeList.EndUnboundLoad();

            TreeList.FocusedNode = null;
            IsFirstCreateNode = false;
        }
        
        private void treeListCustomerLetter_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (IsFirstCreateNode)
            {
                return;
            }
            
            try
            {
                var groupOperator = new GroupOperator(GroupOperatorType.And);
                var groupOperatorCustomer = new GroupOperator(GroupOperatorType.Or);

                if (e.Node?.GetValue(0) is Customer)
                {
                    var customer = (Customer)e.Node.GetValue(0);
                    groupOperator.Operands.Add(GetCriteriaCustomer(customer));
                }
                else
                {
                    foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
                    {
                        if (e.Node != null && e.Node.GetValue(0) is string && e.Node.GetValue(0).Equals(typeLetter.GetEnumDescription()))
                        {
                            var groupOperatorTypeLetterAnd = new GroupOperator(GroupOperatorType.And);
                            var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                            groupOperatorTypeLetterAnd.Operands.Add(criteriaTypeLetter);
                            groupOperator.Operands.Add(groupOperatorTypeLetterAnd);

                            var customer = (Customer)e.Node.ParentNode.GetValue(0);
                            groupOperator.Operands.Add(GetCriteriaCustomer(customer));
                            break;
                        }
                    }
                }

                FocusedNodeChanged?.Invoke(groupOperator, e.Node);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private GroupOperator GetCriteriaCustomer(Customer customer)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.Or);

            var critreiaCustomer = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Letter.Customer.Oid)}", customer.Oid);
            groupOperator.Operands.Add(critreiaCustomer);

            foreach (var contact in customer.CustomerEmails)
            {
                if (!string.IsNullOrWhiteSpace(contact.Email))
                {
                    var criteria = new BinaryOperator(nameof(Letter.LetterSenderAddress), contact.Email);
                    groupOperator.Operands.Add(criteria);
                }
            }

            return groupOperator;
        }

        private void RefresTreeListNodeFocused(TreeListNode treeListNod, Customer customer, XPCollection<Letter> letters, TypeLetter? typeLetter)
        {
            if (treeListNod.GetValue(0) is string name && typeLetter != null)
            {
                var type = GetTypeLetter(name);

                if (type == typeLetter)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);

                    var criteriaCustomer = GetCriteriaCustomer(customer);
                    groupOperator.Operands.Add(criteriaCustomer);

                    var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                    groupOperator.Operands.Add(criteriaTypeLetter);

                    letters.Filter = groupOperator;
                    SetValue(treeListNod, letters);
                }
            }
        }

        /// <summary>
        /// Полная перерисовка объекта.
        /// </summary>
        public void RefresTreeListNodeFocused(TreeListNode treeListNodeFocused, TypeLetter? letterType = null)
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var xpcollectionLetter = new XPCollection<Letter>(uof);
                    
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    var groupOperatorCustomer = new GroupOperator(GroupOperatorType.Or);

                    if (treeListNodeFocused.GetValue(0) is Customer)
                    {
                        var customer = (Customer)treeListNodeFocused.GetValue(0);
                        groupOperator.Operands.Add(GetCriteriaCustomer(customer));

                        xpcollectionLetter.Filter = groupOperator;
                        SetValue(treeListNodeFocused, xpcollectionLetter);

                        if (treeListNodeFocused.Nodes != null && treeListNodeFocused.Nodes.Count > 0)
                        {
                            foreach (TreeListNode treeListNode in treeListNodeFocused.Nodes)
                            {
                                RefresTreeListNodeFocused(treeListNode, customer, xpcollectionLetter, letterType);
                            }
                        }
                    }
                    else
                    {
                        foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
                        {
                            if (treeListNodeFocused != null && treeListNodeFocused.GetValue(0) is string && treeListNodeFocused.GetValue(0).Equals(typeLetter.GetEnumDescription()))
                            {
                                var customer = (Customer)treeListNodeFocused.ParentNode.GetValue(0);
                                groupOperator.Operands.Add(GetCriteriaCustomer(customer));

                                xpcollectionLetter.Filter = groupOperator;
                                SetValue(treeListNodeFocused.ParentNode, xpcollectionLetter);

                                var groupOperatorTypeLetterAnd = new GroupOperator(GroupOperatorType.And);
                                var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                                groupOperatorTypeLetterAnd.Operands.Add(criteriaTypeLetter);
                                groupOperator.Operands.Add(groupOperatorTypeLetterAnd);

                                xpcollectionLetter.Filter = groupOperator;
                                SetValue(treeListNodeFocused, xpcollectionLetter);

                                break;
                            }
                        }
                    }

                    xpcollectionLetter.Dispose();

                    Invoke((Action)delegate
                    {
                        Refresh();
                    });
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void SetValue(TreeListNode treeListNode, XPCollection<Letter> xpCollection)
        {
            var countDontRead = xpCollection.Where(w => !w.IsRead).Count();
            var countTotal = xpCollection.Count();

            Invoke((Action)delegate
            {
                treeListNode.SetValue("columnDontRead", countDontRead);
                treeListNode.SetValue(1, countDontRead);
                treeListNode.SetValue("columnTotal", countTotal);
                treeListNode.SetValue(2, countTotal);
            });
        }

        private TypeLetter GetTypeLetter(string name)
        {
            foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
            {
                if (name.Equals(typeLetter.GetEnumDescription()))
                {
                    return typeLetter;
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// Полная перерисовка объекта.
        /// </summary>
        public void RefresControl()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var xpcollectionLetter = new XPCollection<Letter>(uof);

                    foreach (TreeListNode treeListNode in TreeList.Nodes)
                    {
                        var groupOperator = new GroupOperator(GroupOperatorType.And);

                        if (treeListNode.GetValue(0) is Customer)
                        {
                            var customer = (Customer)treeListNode.GetValue(0);
                            groupOperator.Operands.Add(GetCriteriaCustomer(customer));

                            xpcollectionLetter.Filter = groupOperator;
                            SetValue(treeListNode, xpcollectionLetter);
                        }

                        RefresChildrenTreeListNodeControl(treeListNode.Nodes, xpcollectionLetter);
                    }

                    Invoke((Action)delegate
                    {
                        Refresh();
                    });
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void RefresChildrenTreeListNodeControl(TreeListNodes treeListNodes, XPCollection<Letter> xpCollection)
        {
            try
            {
                foreach (TreeListNode node in treeListNodes)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    if (node.GetValue(0) is string name)
                    {
                        var customer = (Customer)node.ParentNode.GetValue(0);
                        groupOperator.Operands.Add(GetCriteriaCustomer(customer));

                        var typeLetter = GetTypeLetter(name);
                        var groupOperatorTypeLetterAnd = new GroupOperator(GroupOperatorType.And);
                        var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                        groupOperatorTypeLetterAnd.Operands.Add(criteriaTypeLetter);
                        groupOperator.Operands.Add(groupOperatorTypeLetterAnd);
                    }
                    xpCollection.Filter = groupOperator;
                    SetValue(node, xpCollection);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }        
    }
}
