using DVLD.Classes;
using DVLD.license;
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

namespace DVLD.Applications.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        clsLicense _LicenseInfo;

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID, DVLD_Buisness.clsLicense LicenseInfo)
        {
            if (LicenseID == -1) return;
            if(LicenseInfo == null) return;
            _LicenseInfo = LicenseInfo;
            lblLicenseID.Text = LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            if(LicenseInfo.IsDetained){
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = true;
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);




        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
            


            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
            
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
         frmShowLicenseDriverInfo frm = new frmShowLicenseDriverInfo(_LicenseInfo.LicenseID);
         frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(_LicenseInfo.DriverInfo.PersonID);
          frm.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if(!this.ValidateChildren()||string.IsNullOrEmpty(txtFineFees.Text)){
            txtFineFees.Focus();    
            return;
            }

            int  _DetainID = _LicenseInfo.Detain(txtFineFees.Text, clsGlobal.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}
