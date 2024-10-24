using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using RMS.Core.Controllers.Letters;
using RMS.Core.Model.Mail;
using RMS.UI.xUI.PostOffice.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.UI.xUI.PostOffice.Forms
{
    public partial class MailClientForm : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();
        private Letter currentLetter;
        
        private LetterControl _letterControl;
        private LetterCatalogControl _letterCatalogControl;

        public MailClientForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;
        }
        
        private async void Form_Load(object sender, EventArgs e)
        {
            await CreateLetterCatalogControl();
            CreateLetterControl();
            
            //_letterControl.UpdateData(await LettersController.GetLettersAsync(_uof));
        }

        private void CreateLetterControl()
        {
            _letterControl = default;
            var baseLayoutItem = layoutControlGroupLetter.Items.FirstOrDefault(f => f.Text.Equals(nameof(_letterControl)));
            if (baseLayoutItem is null)
            {
                _letterControl = new LetterControl();
                //letterControl.SetIsOperatingContract();
                //letterControl.FocusedRowChangedEvent += ContractControl_FocusedRowChangedEvent;
                var item = layoutControlGroupLetter.AddItem(nameof(_letterControl));
                item.Control = _letterControl;
            }
            else
            {
                _letterControl = (LetterControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private async Task CreateLetterCatalogControl()
        {
            _letterCatalogControl = default;
            var baseLayoutItem = layoutControlGroupLetterCatalog.Items.FirstOrDefault(f => f.Text.Equals(nameof(_letterCatalogControl)));
            if (baseLayoutItem is null)
            {
                _letterCatalogControl = new LetterCatalogControl();
                _letterCatalogControl.FocusedNodeChanged += _letterCatalogControl_FocusedNodeChanged;
                var item = layoutControlGroupLetterCatalog.AddItem(nameof(_letterCatalogControl));
                item.Control = _letterCatalogControl;
                
                item.TextVisible = false;
            }
            else
            {
                _letterCatalogControl = (LetterCatalogControl)((LayoutControlItem)baseLayoutItem).Control;
            }

            _letterCatalogControl.UpdateData(await LetterCatalogsController.GetLetterCatalogsAsync(_uof));
        }

        private async void _letterCatalogControl_FocusedNodeChanged(int letterCatalogOid)
        {
            _letterControl?.UpdateData(await LettersController.GetLettersByCatalogAsync(_uof, letterCatalogOid));
        }

        /// <summary>
        /// Обновление информации на вкладках.
        /// </summary>
        /// <param name="obj">Текущий договор.</param>
        /// <param name="pageName">Наименование открываемой страницы.</param>
        private void UpdateTabbedControl(Letter obj, string pageName)
        {
            if (obj != null)
            {
                switch (pageName)
                {
                    case "layoutControlGroupRenter":
                        UpdateRenterGrid(obj);
                        break;
                }
            }
        }
        
        private void tabbedControlGroupRenterInfo_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            UpdateTabbedControl(currentLetter, e.Page.Name);
        }

        private void ContractControl_FocusedRowChangedEvent(Letter obj, int focusedRowHandle)
        {
            currentLetter = obj;
            UpdateTabbedControl(obj, tabbedControlGroupRenterInfo.SelectedTabPage.Name);
        }
        
        /// <summary>
        /// Обновление формы лицевого счета.
        /// </summary>
        /// <param name="counterparty"></param>
        private void UpdateRenterGrid(Letter contract)
        {
            //contract.Renters?.Reload();
            //var control = layoutControlGroupRenter.AssignObject<LetterControl>();
            //control.SetContrac(contract);
            //control.UpdateData(contract.Renters?.ToList());
        }        
    }
}