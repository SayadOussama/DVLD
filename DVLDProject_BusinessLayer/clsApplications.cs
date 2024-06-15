using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        //int ApplicationPersonID, ref DateTime ApplicationDate,
        //   ref int ApplicationTypesID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
        //   ref Decimal PaidFees, ref int CreatedByUserID
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypesID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreateByUserID { get; set; }


        //Empty Contructior 
        //To Upload Data From User In Presentation Layer 
        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypesID = -1;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.Now; 
            this.PaidFees = 0;
            this.CreateByUserID = -1;
    

            _Mode = enMode.AddNew;

        }
        public clsApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypesID, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypesID = ApplicationTypesID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate; 
            this.PaidFees = PaidFees;
            this.CreateByUserID = CreatedByUserID;


            _Mode = enMode.UpdateNew;




        }


        private bool _AddNewApplication()
        {
            

            this.ApplicationID = clsDataAccesssApplications.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypesID,
                this.ApplicationStatus, this.LastStatusDate,this.PaidFees,this.CreateByUserID);

            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            //call DataAccess Layer 

            return clsDataAccesssApplications.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypesID,
                this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreateByUserID);


        }
        public static clsApplications FindByApplicationID(int ApplicationID)
        {
           
            int ApplicantPersonID = -1, ApplicationTypesID = -1, CreateByUserID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
           
            decimal PaidFees = 0;
            

            if (clsDataAccesssApplications.FindApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypesID,
           ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
           ref CreateByUserID))



                return new clsApplications(ApplicationID, ApplicantPersonID,  ApplicationDate,  ApplicationTypesID,
            ApplicationStatus,  LastStatusDate,  PaidFees,
            CreateByUserID);
            else return null;
        }

        public static clsApplications FindByApplicationPersonID(int ApplicantPersonID)
        {

            int ApplicationID = -1, ApplicationTypesID = -1, CreateByUserID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;

            decimal PaidFees = 0;


            if (clsDataAccesssApplications.FindApplicationByPersonApplicationID(ref ApplicationID, ApplicantPersonID, ref ApplicationDate, ref ApplicationTypesID,
           ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
           ref CreateByUserID))



                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypesID,
            ApplicationStatus, LastStatusDate, PaidFees,
            CreateByUserID);
            else return null;
        }
        public static DataTable GetAllRecordFromApplicationsTable()
        {
            return clsDataAccesssApplications.GetAllApplicationRecords();

        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateApplication();

            }




            return false;
        }

        public static bool isApplicationExsist(int ApplicationID)
        {
            return clsDataAccesssApplications.IsApplicationIDExist(ApplicationID);
        }
       public   bool Delete()
        {
            return clsDataAccesssApplications.DeleteApplication(this.ApplicationID);
        }

    }
}
