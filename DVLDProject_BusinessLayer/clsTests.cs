using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsTests
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int TestID { set; get; }
        //int TestID, ref int TestAppointmentID, ref byte TestResult, ref string  Notes,ref int CreatedByUserID
        public int  TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }
        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID =-1;
            _Mode = enMode.AddNew;
        }
        private clsTests(int TestID,  int TestAppointmentID, bool TestResult,  string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enMode.UpdateNew;
        }
        private bool _AddNewTestResult()
        {

            this.TestID = clsDataAccessTests.AddNewUser(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestResult())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case enMode.UpdateNew:

                //    return _UpdateUser();


            }
            return false;
        }
        public static int HowManyTestPersonPass(int LocalDrivingApplicationID)
        {
            return clsDataAccessTests.HowManyTestPeronPassedTest(LocalDrivingApplicationID);
        }
        //public static bool IsPassedTestOrNot(int LocalDrivingApplicationID,)
        //{
        //    return (clsDataAccessTests.HowManyTestPeronPassedTest(LocalDrivingApplicationID) > 0);
        //}
        public static bool IsPassedToGetAnotherAppointment(int LocalDrivingApplicationID,int TestTypeID)
        {
            return (clsDataAccessTests.IsPassedATestToGetAnotherTestTest(LocalDrivingApplicationID, TestTypeID) > 0);
        }
    }
}
