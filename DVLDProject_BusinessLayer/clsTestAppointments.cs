using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsTestAppointments
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int TestApointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalDrivingLicenseApplication { set; get; }

        public DateTime AppointmentDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int ReTakeTestAppID = -1; 
        public clsTestAppointments()
        {
            this.TestApointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplication = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.ReTakeTestAppID = -1;
            _Mode = enMode.AddNew;
        }
        private clsTestAppointments(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int ReTakeTestAppID)
        {
            this.TestApointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplication = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.ReTakeTestAppID = ReTakeTestAppID; 
            _Mode = enMode.UpdateNew;
        }
        public static clsTestAppointments FindTestAppointment(int TestApointmentID)
        {
            



            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            int ReTakeTestAppID = -1;
            bool IsLocked = false;

            if (clsDataAccessTestAppointements.FindTestAppointmentByID(TestApointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate , ref PaidFees
                                               ,ref CreatedByUserID, ref IsLocked, ref ReTakeTestAppID))




                return new clsTestAppointments(TestApointmentID,  TestTypeID,  LocalDrivingLicenseApplicationID,  AppointmentDate,  PaidFees, CreatedByUserID, IsLocked, ReTakeTestAppID);
            else
                return null;

        }
        //GetTestApointmentByLDLAID
        public static clsTestAppointments FindTestAppointmentByLDLAID(int LocalDrivingLicenseApplicationID)
        {




            int TestTypeID = -1;
            int TestApointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int ReTakeTestAppID = -1;
            if (clsDataAccessTestAppointements.FindTestAppointmentByLocalDrivingLicenseApplication(ref TestApointmentID, ref TestTypeID,   LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees
                                               , ref CreatedByUserID, ref IsLocked, ref ReTakeTestAppID ))




                return new clsTestAppointments(TestApointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, ReTakeTestAppID);
            else
                return null;

        }

        private bool _AddNewTestAppointment()
        {

            this.TestApointmentID = clsDataAccessTestAppointements.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplication, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.ReTakeTestAppID);

            return (this.TestApointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {

            return clsDataAccessTestAppointements.UpdateTestAppointment(this.TestApointmentID, this.TestTypeID, this.LocalDrivingLicenseApplication, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.ReTakeTestAppID);


        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateTestAppointment();


            }
            return false;
        }

        public static DataTable GetTestApointmentListByLDLAID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsDataAccessTestAppointements.GetTestApointmentByLDLAID(LocalDrivingLicenseApplicationID, TestTypeID);

        }
        public static  bool IsHaveAllReadyAppointmentByLDLAID(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsDataAccessTestAppointements.IsISAllReadyHaveAppointmentSearchingByLDLAID(LocalDrivingLicenseApplicationID,TestTypeID);
        }
        public static bool IsISAllReadyHaveAppointmentAndFileByLDLAID(int LocalDrivingLicenseApplicationID)
        {
            return clsDataAccessTestAppointements.IsISAllReadyHaveAppointmentAndFile(LocalDrivingLicenseApplicationID);
        }
        public static int HowTrailsHave(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsDataAccessTestAppointements.HowManyTrail(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static bool HaveAppointmentAndLocked(int LocalDrivingLicenseApplicationID)
        {
            return clsDataAccessTestAppointements.IsHaveLockedAppointment(LocalDrivingLicenseApplicationID);
        }
    }
}
