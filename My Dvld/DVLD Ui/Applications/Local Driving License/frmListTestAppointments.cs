using DVLD.Properties;
using DVLD.Test;
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
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;
        
        int _LocalDrinvingLicenseApplicationID= 0;
        
        clsTestType.enTestType _TestType;
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();
            
            _LocalDrinvingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }


        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSechduleTest_Load(object sender, EventArgs e)
        {
            
          
          _LoadTestTypeImageAndTitle();

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrinvingLicenseApplicationID);
            
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrinvingLicenseApplicationID, _TestType);

            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
          
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsTheLastLocked(_LocalDrinvingLicenseApplicationID, (int)_TestType))
            {

                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
           


            frmScheduleTest frm = new frmScheduleTest(_LocalDrinvingLicenseApplicationID, _TestType);
            frm.ShowDialog();
            frmSechduleTest_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;


            frmScheduleTest frm = new frmScheduleTest(_LocalDrinvingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmSechduleTest_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
          if(  frm.ShowDialog()==DialogResult.Yes){
          this.Close();
          }
            frmSechduleTest_Load(null, null);
        }
    }
    
}
