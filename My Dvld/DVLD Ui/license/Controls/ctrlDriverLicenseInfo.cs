using DVLD.Classes;
using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.license.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        clsLicense _License;
        int _LicenseID;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public clsLicense LicenceInfo{ get{ return _License; } }
        public int LicenseID{ get{  return _LicenseID; }  }
        void LoadPersonImage(){

            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {

                if (File.Exists(ImagePath))
                pbPersonImage.ImageLocation = ImagePath;
                
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public  void LoadInfo(int LicenseID){


            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
          /*need to change*/  lblIsDetained.Text = "No";MessageBox.Show("need to change detaind");
            lblClass.Text = _License.LiceseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonName;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            LoadPersonImage();

        }
    }
}
