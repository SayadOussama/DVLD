using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public class clsDrivers
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int DriverID { set; get; }
        //int TestID, ref int TestAppointmentID, ref byte TestResult, ref string  Notes,ref int CreatedByUserID
        public int PersonID { set; get; }
        public int CreatedByUersID { set; get; }
        public DateTime CreatedDate { set; get; }
     
        public clsDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUersID = -1;
            this.CreatedDate = DateTime.Now;
            
            _Mode = enMode.AddNew;
        }
        private clsDrivers(int DriverID, int PersonID, int CreatedByUersID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUersID = CreatedByUersID;
            this.CreatedDate = CreatedDate;
            _Mode = enMode.UpdateNew;
        }
        private bool _AddNewDriver()
        {

            this.DriverID = clsDataAccessDrivers.AddNewDriver(this.PersonID, this.CreatedByUersID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {

            return clsDataAccessDrivers.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUersID,this.CreatedDate);


        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    case enMode.UpdateNew:

                        return _UpdateDriver();


            }
            return false;
        }

        public static clsDrivers FindDrivierByID(int DriverID)
        {
            
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now; //, UserName = "";

            if (clsDataAccessDrivers.GetDriverByID(DriverID, ref PersonID,ref CreatedByUserID,
           ref CreatedDate))



                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static clsDrivers FindDrivierByPersonID(int PersonID)
        {
            int DriverID = -1;
            //int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now; //, UserName = "";

            if (clsDataAccessDrivers.GetDriverByPersonID(ref DriverID,  PersonID, ref CreatedByUserID,
           ref CreatedDate))



                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }
        public static bool IsDriverExsist(int PersonID)
        {
           return  clsDataAccessDrivers.IsDriverExistByPersonID(PersonID);
        }



    }
}
