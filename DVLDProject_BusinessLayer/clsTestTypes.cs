using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsTestTypes
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public decimal TestTypeFees { set; get; }

        public clsTestTypes()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = -1;

            _Mode = enMode.AddNew;
        }
        private clsTestTypes(int TestTypeID, string TestTypeTitle,string  TestTypeDescription ,decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            _Mode = enMode.UpdateNew;
        }
        public static clsTestTypes FindTestTypeByID(int TestTypeID)
        {
            // PersonID = -1;

            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFees = -1;

            if (clsDataAccessTestTypes.GetTestTypesByID(TestTypeID, ref TestTypeTitle,ref TestTypeDescription, ref TestTypeFees))




                return new clsTestTypes( TestTypeID,  TestTypeTitle,   TestTypeDescription,  TestTypeFees);
            else
                return null;

        }

        private bool _UpdateTestType()
        {

            return clsDataAccessTestTypes.UpdateUpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);


        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                //if (_AddNewUser())
                //{

                //    _Mode = enMode.UpdateNew;
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

                case enMode.UpdateNew:

                    return _UpdateTestType();


            }
            return false;
        }
        public static DataTable GetAllTestTypes()
        {
            return clsDataAccessTestTypes.GetAllTestTypes();

        }

        public static decimal GetTestTypeFees(int TestTypeID)
        {
            return clsDataAccessTestTypes.GetTestTypeFees(TestTypeID);
        }


    }
}
