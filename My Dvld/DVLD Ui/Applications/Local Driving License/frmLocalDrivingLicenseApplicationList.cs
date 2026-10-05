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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationList : Form
    {
        public frmLocalDrivingLicenseApplicationList()
        {
            InitializeComponent();
        }
        clsTestType.enTestType _TestType;
        void LoadApplicationList(){

            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();


           

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;

        }
        

        private void frmLocalDrivingLicenseApplicationList_Load(object sender, EventArgs e)
        {
        LoadApplicationList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.Text) {

                case "None":
                txtFilterValue.Visible = false;
                    break;

            
                default: 
                txtFilterValue.Visible = true;
                break;
            
            }
        }

        void ScheduleTest(){
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(ID,_TestType );
            frm.ShowDialog();
           
        frmLocalDrivingLicenseApplicationList_Load(null,null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ScheduleTest();
        }

      
        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

       

            cmsApplications.Enabled = true;

            foreach (ToolStripItem item in cmsApplications.Items)
            {
                item.Enabled = true;
            }

       

            int PassedTest = Convert.ToInt32(
                dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value);

            // Reset scheduling menu
            ScheduleTestsMenue.Enabled = true;

            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;


            switch (PassedTest)
            {
                case 0:

                    _TestType = clsTestType.enTestType.VisionTest;

                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;

                    break;


                case 1:

                    _TestType = clsTestType.enTestType.WrittenTest;

                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;

                    break;


                case 2:

                    _TestType = clsTestType.enTestType.StreetTest;

                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;

                    break;


                default:

                    ScheduleTestsMenue.Enabled = false;

                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

                    break;
            }


         

            string status = Convert.ToString(
                dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value);


            switch (status)
            {
                case "New":

                    cmsApplications.Enabled = true;

                    showLicenseToolStripMenuItem.Enabled = false;

                    break;


                case "Cancelled":

                    cmsApplications.Enabled = true;

                    foreach (ToolStripItem item in cmsApplications.Items)
                    {
                        item.Enabled =
                            item == showDetailsToolStripMenuItem ||
                            item == showPersonLicenseHistoryToolStripMenuItem;
                    }

                    break;


                case "Completed":

                    cmsApplications.Enabled = true;

                    foreach (ToolStripItem item in cmsApplications.Items)
                    {
                        item.Enabled =
                            item == showDetailsToolStripMenuItem ||
                            item == showPersonLicenseHistoryToolStripMenuItem ||
                            item == showLicenseToolStripMenuItem;
                    }

                    break;


                default:

                    cmsApplications.Enabled = false;

                    break;
            }
        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTest();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTest();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmShowApplicationInfo frm= new frmShowApplicationInfo(ID);
            frm.ShowDialog();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);
          if(  MessageBox.Show("Are You sure You Want to Delete This Application", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.Cancel){
                return;
            }
            if (localDrivingLicenseApplication != null)
            {
                if (localDrivingLicenseApplication.Delete())
                {

                    MessageBox.Show("The Delete Done Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Couldn't Delete This Application there is Data Conect to It", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);
           
          if(  localDrivingLicenseApplication.SetCancelled()){

                MessageBox.Show("The Update Done Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
         frmAddUpdateLocalDrivingLicenseApplication frm=   new frmAddUpdateLocalDrivingLicenseApplication(ID);
         frm.ShowDialog();
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmLicenseIssueFirstTime frm = new frmLicenseIssueFirstTime(ID);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID).GetLicense();

            frmShowLicenseDriverInfo frm = new frmShowLicenseDriverInfo(LicenseID);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID).ApplicantPersonID;
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
