using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.license.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        public event Action<int,clsLicense> OnLicenseSelected;


        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        int _LicenseID;
        private bool _FilterEnabled = true;
        public clsLicense SelectLicenseInfo{ get{ return ctrlDriverLicenseInfo1.LicenceInfo; } }
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public int LicenseID{ get { return _LicenseID; } }
        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
               
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
              
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
        public void LoadLicenseInfo(int LicenseID)
        {



            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if(OnLicenseSelected != null){
                OnLicenseSelected(_LicenseID,SelectLicenseInfo);
            }

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()||string .IsNullOrEmpty(txtLicenseID.Text))
            {
            
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }
        }
    }
}
