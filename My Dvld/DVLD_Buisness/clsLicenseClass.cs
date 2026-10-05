using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
     public class clsLicenseClass
    {
       
            public int LicenseClassID { get; set; }
            public string ClassName { get; set; }
            public string ClassDescription { get; set; }
            public byte MinimumAllowedAge { get; set; }
            public byte DefaultValidityLength { get; set; }
            public float ClassFees { get; set; }

            public clsLicenseClass()
            {
                this.LicenseClassID = -1;
                this.ClassName = "";
                this.ClassDescription = "";
                this.MinimumAllowedAge = 0;
                this.DefaultValidityLength = 0;
                this.ClassFees = 0;
            }

            public clsLicenseClass(int licenseClassID, string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, float classFees)
            {
                this.LicenseClassID = licenseClassID;
                this.ClassName = className;
                this.ClassDescription = classDescription;
                this.MinimumAllowedAge = minimumAllowedAge;
                this.DefaultValidityLength = defaultValidityLength;
                this.ClassFees = classFees;
            
            }


        public static clsLicenseClass Find(int LicenseClassID) {

            string ClassName = ""; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; float ClassFees = 0;


          if(  clsLicenseClassData.GetLicenseClassByClassID(LicenseClassID,ref ClassName,ref ClassDescription,ref MinimumAllowedAge,ref DefaultValidityLength,ref ClassFees)){

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

          } else{ return null; }
        
        
        }
        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }
        public static DataTable GetAllLicenseClassTable() {

            return clsLicenseClassData.GetAllLicenseClassTable();
        
        }

     }
}
