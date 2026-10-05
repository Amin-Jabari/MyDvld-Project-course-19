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

namespace DVLD.license
{
    public partial class frmLicenseIssueFirstTime : Form
    {

        int _LocalDrivingLicesneApplicationID=-1;

        public frmLicenseIssueFirstTime(int LocalDrivingLicenseApplcationID)
        {
            InitializeComponent();
            _LocalDrivingLicesneApplicationID = LocalDrivingLicenseApplcationID;
        }

        private void frmLicenseIssueFirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicesneApplicationID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicesneApplicationID);
            int LicenseID = localDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
