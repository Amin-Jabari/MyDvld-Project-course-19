using DVLD.Applications;
using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Test_Type;
using DVLD.Classes;
using DVLD.license;
using DVLD.license.InternationalLicense;
using DVLD.license.RenewLicense;
using DVLD.Login;
using DVLD.People;
using DVLD.Test;
using DVLD.User;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace DVLD
{

    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain( frmLogin frm )
        {
            InitializeComponent();
            _frmLogin= frm;

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmAddUpdateLocalDrivingLicenseApplication());
        }
        private void ShowChildForm(Form childForm)
        {
      
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            childForm.MdiParent = this;
            childForm.Show();
        }
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmListPeople());
           
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmListUsers());
   
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm( new frmUserInfo(clsGlobal.CurrentUser.UserID));
           

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmChangePassword(clsGlobal.CurrentUser.UserID));
           

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ShowChildForm(new frmManageApplicationTypes());
           
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmTestTypes());
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
             ShowChildForm(new frmNewInternationLicenseApplication());
       

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmRenewDriverLicense());

        }

       

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
         
        }

      
        private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmLocalDrivingLicenseApplicationList());

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }

      

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowChildForm(new frmInternationalLicenseApplicationList());

        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
        

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
       

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }
    }
}
