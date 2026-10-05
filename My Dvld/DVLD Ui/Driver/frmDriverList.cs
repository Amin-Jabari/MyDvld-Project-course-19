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

namespace DVLD.Driver
{
    public partial class frmDriverList : Form
    {
        public frmDriverList()
        {
            InitializeComponent();
        }
        private DataTable _dtAllDrivers;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDriverList_Load(object sender, EventArgs e)
        {
            dgvDrivers.DataSource = clsDriver.GetAllDrivers();
        }

        private void cbFilterBy_TextChanged(object sender, EventArgs e)
        {


            txtFilterValue.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();


        }
            private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // Map Selected Filter to real Column name
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            // Checking if the column is numeric or text string
            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {
                // In this case we deal with numbers not string
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                // Search using LIKE with '%' on both sides to match text anywhere in the cell
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }

            // Update the record count after applying the filter
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            } 
    }
    
}
