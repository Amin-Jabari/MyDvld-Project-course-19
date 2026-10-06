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

namespace DVLD.Applications
{
    public partial class frmReplaceLostOrDamageLicenseApplication : Form
    {
        public frmReplaceLostOrDamageLicenseApplication()
        {
            InitializeComponent();
        }

        clsLicense _Licnese;
      
        int _NewLicenseID;

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacment for Damage License";
         lblApplicationFees.Text  = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            
            this.Text = lblTitle.Text;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacment for Lost License";
           lblApplicationFees.Text   = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
           
            this.Text = lblTitle.Text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID, DVLD_Buisness.clsLicense LicenseInfo)
        {
            if (LicenseID < 0) return;
            if(LicenseInfo == null) return;

            _Licnese = LicenseInfo;
            lblOldLicenseID.Text = LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            if(!LicenseInfo.IsActive){

                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;

            }
            btnIssueReplacement.Enabled=true;


        }

        private void frmReplaceLostOrDamageLicenseApplication_Load(object sender, EventArgs e)
        {
        

          lblApplicationDate.Text =clsFormat.DateToShort(DateTime.Now);
            btnIssueReplacement.Enabled = false;
            rbDamagedLicense.Checked = true;
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;


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

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense = _Licnese.ReplaceLicense(GetIssueReason(), clsGlobal.CurrentUser.UserID);
           

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        clsLicense.enIssueReason GetIssueReason(){

            if (rbDamagedLicense.Checked)
                return clsLicense.enIssueReason.DamagedReplacement;

            else
                return clsLicense.enIssueReason.LostReplacement;



        }
    }
}
