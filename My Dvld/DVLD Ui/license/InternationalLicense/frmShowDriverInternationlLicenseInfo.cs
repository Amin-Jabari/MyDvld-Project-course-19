using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.license.InternationalLicense
{
    public partial class frmShowDriverInternationlLicenseInfo : Form
    {
        int _InternationlLicenseID;
        public frmShowDriverInternationlLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationlLicenseID=InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDriverInternationlLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationlLicenseID);
        }
    }
}
