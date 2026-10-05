using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        enum enMode{ AddNew=1,Update=2}
        enMode _Mode=enMode.AddNew;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        int _LocalDrivingLicenseApplicationID;

     
        void fillCbFilter(){

            foreach (DataRow item in clsLicenseClass.GetAllLicenseClassTable().Rows)
            {

                cbLicenseClass.Items.Add(item["ClassName"]);
            }
        }
        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

        }

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClassTable();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillLicenseClassesInComoboBox();


            if (_Mode == enMode.AddNew)
            {

                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrlPersonCardWithFilter2.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;


            }

        }

        private void _LoadData()
        {

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter2.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

        }

     

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }

        }
   

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FillApplicationInfo(){

            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter2.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now.Date;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = clsLicenseClass.Find(cbLicenseClass.SelectedItem.ToString()).LicenseClassID;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle( lblFees.Text);
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            FillApplicationInfo();

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_LocalDrivingLicenseApplication.ApplicantPersonID, (int)clsApplication.enApplicationType.NewDrivingLicense, _LocalDrivingLicenseApplication.LicenseClassID);
            if(ActiveApplicationID>0){

                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;

            }

            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                btnSave.Enabled = false;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }


            //incase of add new mode.
            if (ctrlPersonCardWithFilter2.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                cbLicenseClass.SelectedIndex = 0;
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter2.FilterFocus();
            }
        }
    }
}
