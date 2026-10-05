using DVLD.Classes;
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

namespace DVLD.Test.Test_Type
{
    public partial class frmEditTestTypes : Form
    {
        clsTestType _TestType;
      

        public frmEditTestTypes(clsTestType testType)
        {
            InitializeComponent();

            _TestType = testType;
        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            if (_TestType != null)
            {
                lblTestTypeID.Text = _TestType.ID.ToString();
                txtTitle.Text = _TestType.Title.ToString();
                txtFees.Text = _TestType.Fees.ToString();
                txtDescription.Text = _TestType.Description.ToString();
            }
            else

            {
                MessageBox.Show("Could not find Test Type with id = " + _TestType.ID.ToString(),  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

         if(!this.ValidateChildren()){
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = txtTitle.Text.Trim();
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.UpdateTestType())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
            ;
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
            ;
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            }
            ;


            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            ;
        }
    }
}
