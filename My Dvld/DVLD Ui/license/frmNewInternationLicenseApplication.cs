using DVLD.Classes;
using DVLD.license.InternationalLicense;
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

namespace DVLD.license
{
    public partial class frmNewInternationLicenseApplication : Form
    {
        public frmNewInternationLicenseApplication()
        {
            InitializeComponent();
        }
        int _LicenseID;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int _InternationalLicenseID;
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;


        }

        private void frmNewInternationLicenseApplication_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled=false;
            btnIssueLicense.Enabled = false;
        }


        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           frmShowDriverInternationlLicenseInfo frm = new frmShowDriverInternationlLicenseInfo(_InternationalLicenseID);
           frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        int PersonID=ctrlDriverLicenseInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID;
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void ctrlDriverLicenseInfoWithFilter2_OnLicenseSelected(int LicenseID, clsLicense LicenseInfo)
        {
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            if (LicenseID != -1)
            {

                llShowLicenseInfo.Enabled = false;
                llShowLicenseHistory.Enabled = false;

                return;
            }
            lblLocalLicenseID.Text = LicenseID.ToString();


            if (LicenseInfo.LiceseClassInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;
            _LicenseID = LicenseID;
        }
    }
}
