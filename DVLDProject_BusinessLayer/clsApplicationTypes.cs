using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int ApplicationTypeID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public decimal ApplicationFees { set; get; }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = -1;
           
            _Mode = enMode.AddNew;
        }
        private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
          
            _Mode = enMode.UpdateNew;
        }
        public static clsApplicationTypes FindApplicationByID(int ApplicationTypeID)
        {
            // PersonID = -1;
          
            string ApplicationTypeTitle = "";
            decimal ApplicationFees = -1; 

            if (clsDataAccessApplicationTypes.GetApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))




                return new clsApplicationTypes(ApplicationTypeID,  ApplicationTypeTitle,  ApplicationFees);
            else
                return null;

        }

        private bool _UpdateApplicationID()
        {

            return clsDataAccessApplicationTypes.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);


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

                    return _UpdateApplicationID();


            }
            return false;
        }
        public static DataTable GetAllApplitionTypes()
        {
            return clsDataAccessApplicationTypes.GetAllApplicationTypes();

        }
        public static decimal GetApplicationFees(int ApplicationTypeID) {
          return clsDataAccessApplicationTypes.GetPaidFeesByAppTypeID(ApplicationTypeID);   
        }
    }
}
