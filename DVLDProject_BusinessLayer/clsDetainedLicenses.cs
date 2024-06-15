using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsDetainedLicenses
    {
        public int DetainID {  get; set; }
        public int LicenseID { get;set; }
        public DateTime DetainDate { get; set; }    
        public decimal FineFees { get; set; }   
        public int CreatedByUserID { get; set; }        

        public bool IsReleased { get; set; }    

        public DateTime ReleaseDate { get; set; }

        public int ReleasedByUserID { get; set; }   
        public int ReleaseApplicationID { get; set; }

        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;

        public enum _enIssueReason { FirstTime = 1, Renew = 2, ReplaceForDamage = 3, ReplaceForLost = 4 }

        //Using Compsitions
        //-------------------------------
        public clsLicenses _License;
        public clsDrivers _Drivers;
        public clsLicenseClasses _LicenseClasses;
        public ClsPeopel _Person;
        //-------------------------------
        public clsDetainedLicenses()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
           
            _Mode = enMode.AddNew;
        }
        private clsDetainedLicenses(int DetainID,int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool  IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int  ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
          
            //Load Camposition here 
            this._License = clsLicenses.FindLicenseByID(LicenseID);
            this._Drivers = clsDrivers.FindDrivierByID(this._License.DriverID);
            this._LicenseClasses = clsLicenseClasses.GetLicenseClassByID(this._License.LicenseClass);
            this._Person = ClsPeopel.FindID(this._Drivers.PersonID);
            _Mode = enMode.UpdateNew;
        }
        public static clsDetainedLicenses FindDetainedLicenseByDetainID(int DetainID)
        {

            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;    
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
           

            if (clsDataAccessDetainedLicenses.GetDetaindedLicenseByDetainID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))




                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }
        private bool _AddNewDetainLicense()
        {

            this.DetainID = clsDataAccessDetainedLicenses.AddNewDetainedLicenses(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1);
        }
        private bool _UpdateDetainedLicense()
        {

            return clsDataAccessDetainedLicenses.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);


        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainLicense())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateDetainedLicense();


            }
            return false;
        }

        public static DataTable GetAllDetainedLicensesHistoryList()
        {
            return clsDataAccessDetainedLicenses.GetDetaindedLicenseHistoryList();

        }
        public static int GetDetainIDByLicnseID(int LicnseID)
        {
          return   clsDataAccessDetainedLicenses.GetDetainIdByLicenseID(LicnseID);
        }
    }
}
