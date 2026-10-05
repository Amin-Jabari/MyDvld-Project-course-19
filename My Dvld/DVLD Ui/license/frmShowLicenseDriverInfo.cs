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
    public partial class frmShowLicenseDriverInfo : Form
    {

        int _LicenseID=-1;
        public frmShowLicenseDriverInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseDriverInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
        }
    }
}
