using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsLicenses
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;

        public enum _enIssueReason { FirstTime = 1, Renew = 2, ReplaceForDamage = 3, ReplaceForLost = 4 }

        //Using Compsitions
        //-------------------------------
        public  clsDrivers _Drivers;
        public clsLicenseClasses _LicenseClasses;
       public  ClsPeopel _Person;
        //-------------------------------

        public int LicenseID { set; get; }
       
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }

        public int LicenseClass { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public decimal PaidFees { set; get; }
        public bool IsActive { set; get; }
     //   public byte IssueReason { set; get; }
        public int CreatedByUserID { set; get; }
        public _enIssueReason IssueReason { set; get; }
        public clsLicenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = _enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsLicenses(int LicenseID,int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, _enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes=Notes;
            this.PaidFees = PaidFees;
            this.DriverID=DriverID;
            this.IsActive = IsActive; 
            this.IssueReason = IssueReason;
            this.CreatedByUserID=CreatedByUserID;
            //Load Camposition here 
            this._Drivers = clsDrivers.FindDrivierByID(this.DriverID);
            this._LicenseClasses = clsLicenseClasses.GetLicenseClassByID(this.LicenseClass);
            this._Person =ClsPeopel.FindID(this._Drivers.PersonID);
            _Mode = enMode.UpdateNew;
        }
        public static  string _ReturnIssueReasonEnumsToString(_enIssueReason IssueReason)
        {
            string  IssueReasonstring  = "";
            switch (IssueReason)
            {
                case _enIssueReason.FirstTime:
                    IssueReasonstring = "FirstTime";
                    break;
                case _enIssueReason.Renew:
                    IssueReasonstring = "Renew"; 
                    break;
                case _enIssueReason.ReplaceForDamage:
                    IssueReasonstring = "ReplaceForDamage";
                    break;
                case _enIssueReason.ReplaceForLost:
                    IssueReasonstring = "ReplaceForLost";
                    break;
                
            }
            return IssueReasonstring;
        }
        public static clsLicenses FindLicenseByID(int LicenseID)
        {
           
            int ApplicationID = -1;
            int DriverID = -1;
           int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
           int CreatedByUserID = -1;

            if (clsAccessDataLicenses.GetLicenseByID( LicenseID, ref  ApplicationID, ref  DriverID, ref  LicenseClass, ref  IssueDate, ref  ExpirationDate, ref  Notes, ref  PaidFees, ref  IsActive, ref  IssueReason, ref  CreatedByUserID))




                return new clsLicenses(LicenseID, ApplicationID,  DriverID,  LicenseClass,  IssueDate,  ExpirationDate,  Notes,  PaidFees,  IsActive,(_enIssueReason)  IssueReason,  CreatedByUserID);
            else
                return null;

        }
        private bool _AddNewLicense()
        {

            this.LicenseID = clsAccessDataLicenses.AddNewLicense( this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }
        private bool _UpdateLicense()
        {

            return clsAccessDataLicenses.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte) this.IssueReason, this.CreatedByUserID);


        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateLicense();


            }
            return false;
        }
        public static DataTable GetAllLicenses()
        {
            return clsAccessDataLicenses.GetAllLicenses();

        }
        public static DataTable GetLicenseHistory(int PersonID)
        {
            return clsAccessDataLicenses.GetLicenseHistory(PersonID);

        }
        public static bool PersonIsAllReadyHaveLicense(int LocalDriverLicenseApplicationID)
        {
            return clsAccessDataLicenses.PersonHaveAllReadyLicense(LocalDriverLicenseApplicationID);
        }
        public int  GetInternationalLicenseIDByLicenseID()
        {
            return clsAccessDataLicenses.GetInternationalLicenseIDByLicensesID(this.LicenseID);
        }

        public static bool IsLicenseIDExist(int LicenseID)
        {
            return clsAccessDataLicenses.IsLicenseIDExist(LicenseID);
        }
        public static bool LicenseIsDetained(int LicenseID)
        {
            return clsAccessDataLicenses.LicenseIsDetainedByLicensesID(LicenseID);
        }
    }
}
