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
    public partial class frmReleaseDetainLicense : Form
    {
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }
        int _SelectedLicenseID;
        clsLicense _License;
        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID, DVLD_Buisness.clsLicense License)
        {

            if (LicenseID == -1) return;
            if (License == null) return;
            _License = License;
            llShowLicenseHistory.Enabled = true;
            if (!License.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(LicenseID);
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            lblDetainID.Text = detainedLicense.DetainID.ToString();
            lblLicenseID.Text = LicenseID.ToString();

            lblCreatedByUser.Text = detainedLicense.CreatedByUserInfo.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(detainedLicense.DetainDate);
            lblFineFees.Text = detainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;


            bool IsReleased = _License.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID); ;

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseDriverInfo frm =
               new frmShowLicenseDriverInfo(_License.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseHistory frm =
               new frmShowDriverLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
