using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public  class clsDriver_View
    {
        public static DataTable GetDriver_ViewList()
        {
            return clsDataAccessDriver_View.GetAllDriver_ViewRecord();

        }
    }
}
