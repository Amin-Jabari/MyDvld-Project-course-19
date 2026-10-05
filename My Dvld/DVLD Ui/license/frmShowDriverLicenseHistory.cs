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
    public partial class frmShowDriverLicenseHistory : Form
    {

        int _PersonID = -1;

        public frmShowDriverLicenseHistory(int PerosnID)
        {
            InitializeComponent();
            _PersonID = PerosnID;
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1) { 
            
             ctrlPersonCard1.LoadPersonInfo(_PersonID);
             ctrlDriverLicense1.LoadInfoByPersonID(_PersonID);
            
            }


        }
    }
}
