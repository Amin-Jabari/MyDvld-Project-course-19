using DVLD.Classes;
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

namespace DVLD.license.RenewLicense
{
    public partial class frmRenewDriverLicense : Form
    {

        float _ApplicationFees;
        public frmRenewDriverLicense()
        {
            InitializeComponent();
        }
        clsLicense _Licnese;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewDriverLicense_Load(object sender, EventArgs e)
        {
            clsApplicationType applicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense);

           _ApplicationFees= applicationType.Fees;

            lblApplicationFees.Text =_ApplicationFees.ToString();

            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            txtNotes.Enabled = false;
            btnRenewLicense.Enabled = false;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID, clsLicense LicenseInfo)
        {

            if (LicenseID <= 0) { return; }
            if(LicenseInfo == null) { return; }
            _Licnese = LicenseInfo;
            lblOldLicenseID.Text = LicenseID.ToString();
            lblLicenseFees.Text = LicenseInfo.PaidFees.ToString();
            lblTotalFees.Text = (LicenseInfo.PaidFees + _ApplicationFees).ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(LicenseInfo.ExpirationDate);
            llShowLicenseHistory.Enabled = true;

            if (LicenseInfo.ExpirationDate>DateTime.Now){

                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(LicenseInfo.ExpirationDate)
               , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }


            if(!LicenseInfo.IsActive){

                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
            txtNotes.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(_Licnese.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseDriverInfo frm = new frmShowLicenseDriverInfo(_Licnese.LicenseID);
            frm.ShowDialog();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = _Licnese.RenewLicense(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);
            lblRenewedLicenseID.Text =NewLicense.LicenseID.ToString();
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            btnRenewLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            txtNotes.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;

        }
    }
}
