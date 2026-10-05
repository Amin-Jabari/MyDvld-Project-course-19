using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
   public class clsLicense
    {
        enum enMode { AddNew=1,Update=2}
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        enMode Mode= enMode.AddNew;
       
            public int LicenseID { get; set; }
            public int ApplicationID { get; set; }
            public int DriverID { get; set; }
            public int LicenseClass { get; set; }

            public DateTime IssueDate { get; set; }
            public DateTime ExpirationDate { get; set; }

            public string Notes { get; set; }

            public float PaidFees { get; set; }

            public bool IsActive { get; set; }

            public enIssueReason IssueReason { get; set; }

            public int CreatedByUserID { get; set; }

        public clsLicenseClass LiceseClassInfo;
        public clsDriver DriverInfo;
        public clsApplication ApplicationInfo;

        public string IssueReasonName{ get { return this.GetIssueReasonText(this.IssueReason); } }

        public clsLicense()

        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.LiceseClassInfo = clsLicenseClass.Find(LicenseClass);
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.ApplicationInfo = clsApplication.Find(ApplicationID);

            Mode = enMode.Update;
        }
        private bool _AddNewLicense()
        {
          

            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);


            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
           

            return clsLicenseData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }
        public  string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public static int GetActiveLicenseIDByPersonIDAndClassID(int  PersonID, int ClassID){

        return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, ClassID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public clsLicense RenewLicense(string Note,int CurrentUserID){

          
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
             Application.CreatedByUserID = CurrentUserID;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
          

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LiceseClassInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Note;
            NewLicense.PaidFees = this.LiceseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CurrentUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

         
            DeactivateCurrentLicense();

            return NewLicense;


        }

       void DeactivateCurrentLicense(){

            clsLicenseData.DeactivateLicense(this.LicenseID);


       }



    }
}
