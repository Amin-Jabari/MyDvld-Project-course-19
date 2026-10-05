using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DVLD_DataAccess;
namespace DVLD_Buisness
{
    public class clsTestType
    {

       
        
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public enTestType ID{  get; set; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }
        public clsTestType()

        {
            this.ID = enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
   

        }

        public clsTestType( enTestType ID,string Title,string Description,float Fees)
        {
            this.ID=ID;
            this.Title=Title;
            this.Description=Description;
            this.Fees=Fees;
          
            
        }

        public static DataTable GetTestTypesTable() {

            return clsTestTypesData.GetAllTestTypes();

        }
        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = ""; float Fees = 0;

            if (clsTestTypesData.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description, ref Fees))

                return new clsTestType(TestTypeID, Title, Description, Fees);
            else
                return null;

        }
        public bool UpdateTestType(){
         return   clsTestTypesData.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

       
    }
}
