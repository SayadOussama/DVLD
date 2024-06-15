using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLDProject_BusinessLayer.clsLicenses;

namespace DVLDProject_BusinessLayer
{
    public  class clsInternationalLicenses
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;

        public enum _enIssueReason { FirstTime = 1, Renew = 2, ReplaceForDamage = 3, ReplaceForLost = 4 }

        //Using Compsitions
        //-------------------------------
        public clsDrivers _Drivers;
        public clsLicenses _License;
        public ClsPeopel _Person;
        //-------------------------------

        public int InternationalLicenseID { set; get; }

        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
     
        public bool IsActive { set; get; }
       
        public int CreatedByUserID { set; get; }
        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID =-1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID,int IssuedUsingLocalLicenseID , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            //Load Camposition here 
            this._Drivers = clsDrivers.FindDrivierByID(this.DriverID);
            this._License = clsLicenses.FindLicenseByID(this.IssuedUsingLocalLicenseID);
            this._Person = ClsPeopel.FindID(this._Drivers.PersonID);
            _Mode = enMode.UpdateNew;
        }
        public static clsInternationalLicenses FindLicenseByID(int InternationalLicenseID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1; 
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsDataAccessInternationalLicenses.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID,ref IssuedUsingLocalLicenseID,  ref IssueDate, ref ExpirationDate,  ref IsActive, ref CreatedByUserID))




                return new clsInternationalLicenses(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,  IsActive, CreatedByUserID);
            else
                return null;

        }

        private bool _AddNewInternationalLicense()
        {

            this.InternationalLicenseID = clsDataAccessInternationalLicenses.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive , this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {

            return clsDataAccessInternationalLicenses.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);


        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateInternationalLicense();


            }
            return false;
        }
        public static bool PersonHaveAllReadyInternationalLicense(int ApplicationID)
        {
            return clsDataAccessInternationalLicenses.PersonHaveAllReadyInternationalLicense(ApplicationID);
        }
        public static DataTable InternationalLicenseHistory(int PersonID)
        {
            return clsDataAccessInternationalLicenses.GetInternationalLicenseHistory(PersonID);

        }
     public static DataTable  GetInternationalLicensesList() {

            return clsDataAccessInternationalLicenses.GetAllInternationalLicenses();

        }
    }
    
}
