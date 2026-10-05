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
    public partial class frmShowApplicationInfo : Form
    {

    int _LocalDrivingApplicationID=-1;
        public frmShowApplicationInfo(int LocalDrivingApplicationID)
        {
            InitializeComponent();
            _LocalDrivingApplicationID = LocalDrivingApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingApplicationID);
        }
    }
}
