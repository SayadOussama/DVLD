using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsLocalDrivingLicenseApplications_View
    {

      
        public static DataTable GetAllLDApplication()
        {
            return clsDataAccessLocalDrivingLicenseApplications_View.GetAllListRecord();

        }


        
    }
}
