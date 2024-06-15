using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public class clsLocalDrivingLicenseApplications : clsApplications
    {

        static public clsApplications _Application;
        static public clsLocalDrivingLicenseApplications _LDLA;
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode2 = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = -1;

            this.LicenseClassID = -1;

            _Mode2 = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypesID, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID, int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypesID = ApplicationTypesID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreateByUserID = CreatedByUserID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            _Mode2 = enMode.UpdateNew;
        }

        private bool _AddNewLDLA()
        {
            LocalDrivingLicenseApplicationID = clsAccessDataLocalDrivingLicenseApplications.AddNewLDLA(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);









        }
        private bool _UpdateLDLA()
        {
            return clsAccessDataLocalDrivingLicenseApplications.UpdateLDLA(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        public static clsLocalDrivingLicenseApplications FindLocalDrivingLicenseApplicationsByLDLAID(int LocalDrivingLicenseApplicationID)
        {


            int ApplicationID = -1, ApplicantPersonID = -1, ApplicationTypesID = -1, CreateByUserID = -1, LicenseClassID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            decimal PaidFees = 0;


            if (clsAccessDataLocalDrivingLicenseApplications.FindLDLAByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                //you Must Find Also clsApplications To Able To Reload All Variable SubClass And Super Class 
                clsDataAccesssApplications.FindApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypesID,
          ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
          ref CreateByUserID);



                return new clsLocalDrivingLicenseApplications(ApplicationID, ApplicantPersonID, ApplicationDate,
            ApplicationTypesID, ApplicationStatus, LastStatusDate,
            PaidFees, CreateByUserID, LocalDrivingLicenseApplicationID, LicenseClassID);

            }
            return null;


        }


        public static int GetLDLAIDByApplicationID(int ApplicationID)
        {
            return clsAccessDataLocalDrivingLicenseApplications.GetLDLAIDByApplicationaID(ApplicationID);   
        }



      















        public bool Save()
        {
            //To Doing Save To LDLA you Must Doing Save to Super Class First (clsApplications) 
            base._Mode = (clsApplications.enMode)_Mode2;
            if (!base.Save())
                return false;


            switch (_Mode2)
            {
                case enMode.AddNew:
                    if (_AddNewLDLA())
                    {

                        _Mode2 = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateLDLA();

            }




            return false;
        }

        //new  when you have two same Function in the super class and sub class
        public new bool Delete()
        { 
            if(clsTests.HowManyTestPersonPass(this.LocalDrivingLicenseApplicationID) == 0) { 
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            IsLocalDrivingApplicationDeleted =
            clsAccessDataLocalDrivingLicenseApplications.DeleteLDLAS(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingApplicationDeleted)
                return false;
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;
            }
            else
                return false;
        }

        public static int PassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsAccessDataLocalDrivingLicenseApplications.GetTotalPassedTest(LocalDrivingLicenseApplicationID);
        }
        public bool DoesAttendTest(int TestTypeID)
        {
            return clsAccessDataLocalDrivingLicenseApplications.IsDoesAttendTest(this.LocalDrivingLicenseApplicationID, TestTypeID); ;
        }
        public int GetLicenseIDByPersonIDAndByLicenseID()
        {
            return clsAccessDataLocalDrivingLicenseApplications.GetLicenseIDByPersonIDAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID);
        }
        public void ChangeStatusToComplited()
        {
            if (clsTests.HowManyTestPersonPass(this.LocalDrivingLicenseApplicationID) == 3)
            {
                this.ApplicationStatus = 3;
                Save();
            }

        }
        public static bool IsHaveAllReadyLocalDriverLicenseAppointment(int PersonID, int LicenseClassID)
        {
            return clsAccessDataLocalDrivingLicenseApplications.IsHaveLocalDriverLicenseAppointment(PersonID, LicenseClassID);
        }
    }
}
