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
        _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;



            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            dgvDrivers.Columns[0].HeaderText = "Driver ID";
            dgvDrivers.Columns[0].Width = 100;

            dgvDrivers.Columns[1].HeaderText = "Person ID";
            dgvDrivers.Columns[1].Width = 100;

            dgvDrivers.Columns[2].HeaderText = "National No";
            dgvDrivers.Columns[2].Width = 110;

            dgvDrivers.Columns[3].HeaderText = "Full Name";
            dgvDrivers.Columns[3].Width = 300;

            dgvDrivers.Columns[4].HeaderText = "Date";
            dgvDrivers.Columns[4].Width = 140;

            dgvDrivers.Columns[5].HeaderText = "Active License";
            dgvDrivers.Columns[5].Width = 100;
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

         
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            
            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            } 
    }
    
}
