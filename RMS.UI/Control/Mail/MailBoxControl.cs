using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI.Control.Mail
{
    public partial class MailBoxControl : XtraUserControl
    {
        private Session Session { get; }
        public TreeList TreeList { get; }

        public delegate void FocusedNodeChangedEventHandler(GroupOperator groupOperator, TreeListNode treeListNode);
        public event FocusedNodeChangedEventHandler FocusedNodeChanged;

        public MailBoxControl(Session session)
        {
            InitializeComponent();
            Session = session;

            Dock = DockStyle.Fill;
            TreeList = treeListMailbox;
            
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
                TreeList.FocusedColumn = null;
                TreeList.ClearFocusedColumn();
                TreeList.ClearSelection();
            });

            IsFirstCreateNode = false;
        }

        private void CreateColumns()
        {
            TreeList.BeginUpdate();

            TreeList.StateImageList = imageCollectionMailbox;

            var columnName = TreeList.Columns.Add();
            columnName.Name = nameof(columnName);
            columnName.Caption = "Наименование";
            columnName.VisibleIndex = 0;

            var columnDontRead = TreeList.Columns.Add();
            columnName.Name = nameof(columnDontRead);
            columnDontRead.Caption = "Не прочитано";
            columnDontRead.VisibleIndex = 1;
            columnDontRead.Width = 100;
            columnDontRead.OptionsColumn.FixedWidth = true;
            columnDontRead.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var columnTotal = TreeList.Columns.Add();
            columnName.Name = nameof(columnTotal);
            columnTotal.Caption = "Всего";
            columnTotal.VisibleIndex = 2;
            columnTotal.Width = 65;
            columnTotal.OptionsColumn.FixedWidth = true;
            columnTotal.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            TreeList.EndUpdate();
        }

        private async void CreateNodes()
        {
            IsFirstCreateNode = true;

            TreeList.BeginUnboundLoad();
            TreeList.ClearNodes();

            var rootNode = TreeList.AppendNode(new object[] { "Все отправленные", null, null }, null);
            rootNode.StateImageIndex = 2;

            rootNode = TreeList.AppendNode(new object[] { "Все черновики", null, null }, null);
            rootNode.StateImageIndex = 5;

            var xpcollectionMailbox = new XPCollection<Mailbox>(Session, new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.Working));

            foreach (var item in xpcollectionMailbox)
            {
                TreeListNode parentForRootNodes = null;

                //var countDontReadParent = item.GetLetter()?.Count(c => !c.IsRead && c.LetterCatalog is null) ?? null;
                //var countTotalParent = item.GetLetter()?.Count(c => c.LetterCatalog is null) ?? null;

                var countDontReadParent = await new XPQuery<Letter>(Session)
                    ?.CountAsync(w => 
                        w.Mailbox != null
                        && w.Mailbox.Oid == item.Oid
                        && w.IsRead == false 
                        && w.LetterCatalog == null);
                
                var countTotalParent = await new XPQuery<Letter>(Session)
                    ?.CountAsync(w =>
                        w.Mailbox != null
                        && w.Mailbox.Oid == item.Oid
                        && w.LetterCatalog == null);


                rootNode = TreeList.AppendNode(new object[] { item, countDontReadParent, countTotalParent }, parentForRootNodes);
                rootNode.StateImageIndex = 0;

                foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
                {
                    //var countDontRead = item.GetLetter()?.Where(w => w.TypeLetter == typeLetter && w.LetterCatalog is null)?.Count(c => !c.IsRead) ?? null;
                    //var countTotal = item.GetLetter()?.Where(w => w.TypeLetter == typeLetter && w.LetterCatalog is null)?.Count() ?? null;
                    
                    var countDontRead = await new XPQuery<Letter>(Session)
                        ?.CountAsync(w =>
                            w.Mailbox != null
                            && w.Mailbox.Oid == item.Oid
                            && w.TypeLetter == typeLetter
                            && w.IsRead == false
                            && w.LetterCatalog == null);
                    
                    var countTotal = await new XPQuery<Letter>(Session)
                        ?.CountAsync(w =>
                            w.Mailbox != null
                            && w.Mailbox.Oid == item.Oid
                            && w.TypeLetter == typeLetter
                            && w.LetterCatalog == null);


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

            xpcollectionMailbox.Dispose();

            TreeList.ExpandAll();
            TreeList.EndUnboundLoad();
            
            TreeList.FocusedNode = null;
            IsFirstCreateNode = false;
        }

        private void treeListMailbox_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (IsFirstCreateNode)
            {
                return;
            }
            
            try
            {
                var groupOperator = new GroupOperator(GroupOperatorType.And);

                if (e.Node?.GetValue(0) is Mailbox)
                {
                    var mailbox = (Mailbox)e.Node.GetValue(0);

                    var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                    groupOperator.Operands.Add(criteriaMailbox);

                    var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                    groupOperator.Operands.Add(criteriaLetterCatalog);
                }
                else
                {
                    foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
                    {
                        if (e.Node != null && e.Node.GetValue(0) is string && e.Node.GetValue(0).Equals(typeLetter.GetEnumDescription()))
                        {
                            var mailbox = (Mailbox)e.Node.ParentNode.GetValue(0);

                            var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                            groupOperator.Operands.Add(criteriaMailbox);

                            var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                            groupOperator.Operands.Add(criteriaLetterCatalog);

                            //if (typeLetter != TypeLetter.Spam)
                            //{
                            //    var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                            //    groupOperator.Operands.Add(criteriaLetterCatalog);
                            //}                            

                            //var criteriaCustomer = new NullOperator($"{nameof(Letter.Customer)}");
                            //groupOperator.Operands.Add(criteriaCustomer);

                            var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                            groupOperator.Operands.Add(criteriaTypeLetter);

                            break;
                        }
                    }
                }

                if (e.Node != null && e.Node.GetValue(0) is string && e.Node.GetValue(0).Equals("Все отправленные"))
                {
                    groupOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), TypeLetter.Outgoing);
                    groupOperator.Operands.Add(criteriaTypeLetter);
                }

                if (e.Node != null && e.Node.GetValue(0) is string && e.Node.GetValue(0).Equals("Все черновики"))
                {
                    groupOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), TypeLetter.Draft);
                    groupOperator.Operands.Add(criteriaTypeLetter);
                }

                FocusedNodeChanged?.Invoke(groupOperator, e.Node);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private TreeListNode FindNode(TreeListNodes treeListNodes, TypeLetter typeLetter)
        {
            var treeListNode = default(TreeListNode);
            
            foreach (TreeListNode node in treeListNodes)
            {
                if (node.GetValue(0) is Mailbox mailbox)
                {
                    treeListNode = FindNode(node.Nodes, typeLetter);
                }
                else if (node.GetValue(0) is string nodeName )
                {
                    if (nodeName.Equals(typeLetter.GetEnumDescription()))
                    {
                        treeListNode = node;
                        break;
                    }
                }
            }
            
            return treeListNode;
        }
        
        public async Task RefresControlAsync(TypeLetter typeLetter) 
        {
            try
            {
                var node = FindNode(treeListMailbox.Nodes, typeLetter);
                await RefresChildrenTreeListNodeControlAsync(node, typeLetter);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private async Task RefresChildrenTreeListNodeControlAsync(TreeListNode node, TypeLetter typeLetter)
        {
            try
            {
                if (node.ParentNode.GetValue(0) is Mailbox mailbox)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                    groupOperator.Operands.Add(criteriaMailbox);

                    var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                    groupOperator.Operands.Add(criteriaLetterCatalog);

                    var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                    groupOperator.Operands.Add(criteriaTypeLetter);
                    
                    using (var uof = new UnitOfWork())
                    {
                        using (var collection = new XPCollection<Letter>(uof, groupOperator))
                        {
                            collection.Load();
                            await SetValueAsync(node, collection).ConfigureAwait(false);
                        }                        
                    }
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }


        /// <summary>
        /// Полная перерисовка объекта.
        /// </summary>
        public async Task RefresControl()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    foreach (TreeListNode treeListNode in TreeList.Nodes)
                    {
                        if (treeListNode.GetValue(0) is Mailbox mailbox)
                        {
                            var countDontRead = await new XPQuery<Letter>(Session)
                                ?.CountAsync(w =>
                                    w.Mailbox != null
                                    && w.Mailbox.Oid == mailbox.Oid
                                    && w.IsRead == false
                                    && w.LetterCatalog == null);

                            var countTotal = await new XPQuery<Letter>(Session)
                                ?.CountAsync(w =>
                                    w.Mailbox != null
                                    && w.Mailbox.Oid == mailbox.Oid
                                    && w.LetterCatalog == null);

                            SetValue(treeListNode, countDontRead, countTotal);
                        }

                        await RefresChildrenTreeListNodeControl(treeListNode.Nodes, uof);
                    }

                    Invoke((Action)delegate
                    {
                        Refresh();
                    });                    
                }
                
                //using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                //{
                //    var xpcollectionLetter = new XPCollection<Letter>(session);

                //    foreach (TreeListNode treeListNode in TreeList.Nodes)
                //    {
                //        var groupOperator = new GroupOperator(GroupOperatorType.And);
                //        if (treeListNode.GetValue(0) is Mailbox mailbox)
                //        {
                //            var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                //            groupOperator.Operands.Add(criteriaMailbox);

                //            var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                //            groupOperator.Operands.Add(criteriaLetterCatalog);

                //            //var groupOpertarOr = new GroupOperator(GroupOperatorType.Or);                            
                //            //var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                //            //groupOpertarOr.Operands.Add(criteriaLetterCatalog);
                //            //var criteriaLetterType = new BinaryOperator($"{nameof(Letter.TypeLetter)}", TypeLetter.Spam);
                //            //groupOpertarOr.Operands.Add(criteriaLetterType);
                //            //groupOperator.Operands.Add(groupOpertarOr);

                //            xpcollectionLetter.Filter = groupOperator;
                //            SetValue(treeListNode, xpcollectionLetter);
                //        }

                //        RefresChildrenTreeListNodeControl(treeListNode.Nodes, xpcollectionLetter);
                //    }

                //    Invoke((Action)delegate
                //    {
                //        Refresh();
                //    });

                //    xpcollectionLetter.Dispose();
                //}
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void RefresTreeListNodeFocused(TreeListNode treeListNod, Mailbox mailbox, XPCollection<Letter> letters, TypeLetter? typeLetter)
        {
            if (treeListNod.GetValue(0) is string name && typeLetter != null)
            {
                var type = GetTypeLetter(name);

                if (type == typeLetter)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);

                    var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                    groupOperator.Operands.Add(criteriaMailbox);

                    var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                    groupOperator.Operands.Add(criteriaLetterCatalog);

                    var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                    groupOperator.Operands.Add(criteriaTypeLetter);

                    letters.Filter = groupOperator;
                    SetValue(treeListNod, letters);
                }                
            }
        }

        private async Task RefresTreeListNodeFocused(TreeListNode treeListNod, Mailbox mailbox, UnitOfWork uof, TypeLetter? typeLetter)
        {
            if (treeListNod.GetValue(0) is string name && typeLetter != null)
            {
                var type = GetTypeLetter(name);

                if (type == typeLetter)
                {
                    var countDontRead = await new XPQuery<Letter>(uof)
                       ?.CountAsync(c =>
                           c.Mailbox != null
                           && c.Mailbox.Oid == mailbox.Oid
                           && c.LetterCatalog == null
                           && c.IsRead == false);

                    var countTotal = await new XPQuery<Letter>(uof)
                        ?.CountAsync(c =>
                            c.Mailbox != null
                            && c.Mailbox.Oid == mailbox.Oid
                            && c.LetterCatalog == null);

                    SetValue(treeListNod, countDontRead, countTotal);
                }
            }
        }

        /// <summary>
        /// Перерисовка конкретного объекта.
        /// </summary>
        public async Task RefresTreeListNodeFocused(TreeListNode treeListNodeFocused, TypeLetter? typeLetter = null)
        {           
            try
            {
                using (var uof = new UnitOfWork())
                {
                    //var xpcollectionLetter = new XPCollection<Letter>(uof);
                    //var groupOperator = new GroupOperator(GroupOperatorType.And);

                    if (treeListNodeFocused.GetValue(0) is Mailbox mailbox)
                    {
                        //var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                        //groupOperator.Operands.Add(criteriaMailbox);

                        //var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                        //groupOperator.Operands.Add(criteriaLetterCatalog);

                        //xpcollectionLetter.Filter = groupOperator;
                        //SetValue(treeListNodeFocused, xpcollectionLetter);

                        var countDontRead = await new XPQuery<Letter>(uof)
                           ?.CountAsync(c =>
                               c.Mailbox != null
                               && c.Mailbox.Oid == mailbox.Oid
                               && c.LetterCatalog == null
                               && c.IsRead == false);

                        var countTotal = await new XPQuery<Letter>(uof)
                            ?.CountAsync(c =>
                                c.Mailbox != null
                                && c.Mailbox.Oid == mailbox.Oid
                                && c.LetterCatalog == null);

                        SetValue(treeListNodeFocused, countDontRead, countTotal);

                        if (treeListNodeFocused.Nodes != null && treeListNodeFocused.Nodes.Count > 0)
                        {
                            foreach (TreeListNode treeListNode in treeListNodeFocused.Nodes)
                            {
                                //RefresTreeListNodeFocused(treeListNode, mailbox, xpcollectionLetter, typeLetter);
                                await RefresTreeListNodeFocused(treeListNode, mailbox, uof, typeLetter);
                            }
                        }
                    }
                    else if (treeListNodeFocused.GetValue(0) is string name)
                    {
                        typeLetter = GetTypeLetter(name);

                        if (treeListNodeFocused.ParentNode != null && treeListNodeFocused.ParentNode.GetValue(0) is Mailbox parentMailbox)
                        {
                            mailbox = parentMailbox;

                            //var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                            //groupOperator.Operands.Add(criteriaMailbox);

                            //var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                            //groupOperator.Operands.Add(criteriaLetterCatalog);

                            //var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                            //groupOperator.Operands.Add(criteriaTypeLetter);

                            //xpcollectionLetter.Filter = groupOperator;
                            //SetValue(treeListNodeFocused, xpcollectionLetter);

                            var countDontRead = await new XPQuery<Letter>(uof)
                               ?.CountAsync(c =>
                                   c.Mailbox != null
                                   && c.Mailbox.Oid == mailbox.Oid
                                   && c.TypeLetter == typeLetter
                                   && c.LetterCatalog == null
                                   && c.IsRead == false);

                            var countTotal = await new XPQuery<Letter>(uof)
                                ?.CountAsync(c =>
                                    c.Mailbox != null
                                    && c.Mailbox.Oid == mailbox.Oid
                                    && c.TypeLetter == typeLetter
                                    && c.LetterCatalog == null);

                            SetValue(treeListNodeFocused, countDontRead, countTotal);

                            if (treeListNodeFocused.ParentNode != null)
                            {
                                await RefresTreeListNodeFocused(treeListNodeFocused.ParentNode);
                            }
                        }                       
                    }

                    //xpcollectionLetter.Dispose();
                    
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

        private async Task RefresChildrenTreeListNodeControl(TreeListNodes treeListNodes, UnitOfWork uof)
        {
            try
            {
                foreach (TreeListNode node in treeListNodes)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    if (node.GetValue(0) is string name)
                    {
                        var typeLetter = GetTypeLetter(name);
                        var mailbox = (Mailbox)node.ParentNode.GetValue(0);
                                                
                        var countDontRead = await new XPQuery<Letter>(uof)
                           ?.CountAsync(c =>
                               c.Mailbox != null
                               && c.Mailbox.Oid == mailbox.Oid
                               && c.TypeLetter == typeLetter
                               && c.LetterCatalog == null
                               && c.IsRead == false);

                        var countTotal = await new XPQuery<Letter>(uof)
                            ?.CountAsync(c =>
                                c.Mailbox != null
                                && c.Mailbox.Oid == mailbox.Oid
                                && c.TypeLetter == typeLetter
                                && c.LetterCatalog == null);

                        SetValue(node, countDontRead, countTotal);
                    }
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
                        var typeLetter = GetTypeLetter(name);
                        var mailbox = (Mailbox)node.ParentNode.GetValue(0);

                        var criteriaMailbox = new BinaryOperator($"{nameof(Letter.Mailbox)}.{nameof(Letter.Mailbox.Oid)}", mailbox.Oid);
                        groupOperator.Operands.Add(criteriaMailbox);

                        var criteriaLetterCatalog = new NullOperator($"{nameof(Letter.LetterCatalog)}");
                        groupOperator.Operands.Add(criteriaLetterCatalog);

                        var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), typeLetter);
                        groupOperator.Operands.Add(criteriaTypeLetter);
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

        private void SetValue(TreeListNode treeListNode, int countDontRead, int countTotal)
        {
            try
            {
                Invoke((Action)delegate
                {
                    treeListNode?.SetValue("columnDontRead", countDontRead);
                    treeListNode?.SetValue(1, countDontRead);
                    treeListNode?.SetValue("columnTotal", countTotal);
                    treeListNode?.SetValue(2, countTotal);
                });
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void SetValue(TreeListNode treeListNode, XPCollection<Letter> xpCollection)
        {
            try
            {
                var countDontRead = xpCollection.Where(w => !w.IsRead).Count();
                var countTotal = xpCollection.Count();

                Invoke((Action)delegate
                {
                    treeListNode?.SetValue("columnDontRead", countDontRead);
                    treeListNode?.SetValue(1, countDontRead);
                    treeListNode?.SetValue("columnTotal", countTotal);
                    treeListNode?.SetValue(2, countTotal);
                });
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async Task SetValueAsync(TreeListNode treeListNode, XPCollection<Letter> xpCollection)
        {
            await Task.Run(() =>
            {
                try
                {
                    var countDontRead = xpCollection.Where(w => !w.IsRead).Count();
                    var countTotal = xpCollection.Count();

                    Invoke((Action)delegate
                    {
                        treeListNode?.SetValue("columnDontRead", countDontRead);
                        treeListNode?.SetValue(1, countDontRead);
                        treeListNode?.SetValue("columnTotal", countTotal);
                        treeListNode?.SetValue(2, countTotal);
                    });
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }                
            });
        }

        private TypeLetter GetTypeLetter(string name)
        {
            if (name.Equals("Все черновики"))
            {
                name = TypeLetter.Draft.GetEnumDescription();
            }
            else if (name.Equals("Все отправленные"))
            {
                name = TypeLetter.Outgoing.GetEnumDescription();
            }
            
            foreach (TypeLetter typeLetter in Enum.GetValues(typeof(TypeLetter)))
            {
                if (name.Equals(typeLetter.GetEnumDescription()))
                {
                    return typeLetter;
                }
            }

            throw new Exception();
        }
    }
}
