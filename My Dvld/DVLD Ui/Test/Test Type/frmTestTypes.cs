using DVLD.Test.Test_Type;
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

namespace DVLD.Applications.Test_Type
{
    public partial class frmTestTypes : Form
    {
        public frmTestTypes()
        {
            InitializeComponent();
        }
        private void _FormatTestTypesGrid()
        {
            dgvTestTypes.DataSource = clsTestType.GetTestTypesTable();
            if (dgvTestTypes.Rows.Count > 0)
            {
               
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[3].HeaderText = "Fees";

               
                dgvTestTypes.Columns[0].Width = 80;  
                dgvTestTypes.Columns[1].Width = 160; 
                dgvTestTypes.Columns[2].Width = 350; 
                dgvTestTypes.Columns[3].Width = 100; 

   
                dgvTestTypes.Columns[3].DefaultCellStyle.Format = "N2"; 

                
                dgvTestTypes.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void frmTestTypecs_Load(object sender, EventArgs e)
        {

           _FormatTestTypesGrid();
            lblRecordsCount.Text = dgvTestTypes.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        clsTestType  FillObjectFromRow(DataGridViewRow row){

        clsTestType Testtype = new clsTestType();
        Testtype.ID =(clsTestType.enTestType)Convert.ToUInt32(row.Cells["TestTypeID"].Value);
        Testtype.Title = (string)row.Cells["TestTypeTitle"].Value;
        Testtype.Description = (string)row.Cells["TestTypeDescription"].Value;
        Testtype.Fees = Convert.ToSingle(row.Cells["TestTypeFees"].Value);

        return Testtype;  
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTestType testType = new clsTestType();
           testType= FillObjectFromRow(dgvTestTypes.CurrentRow);
           frmEditTestTypes frm=new frmEditTestTypes(testType);
           frm.ShowDialog();
        }
    }
}
