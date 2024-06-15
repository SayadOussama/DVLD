using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DVLDProject_DataAccessLayer;

namespace DVLDProject_BusinessLayer
{
    public  class clsCountries
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountries()

        {
            this.ID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;

        }

        private clsCountries(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;
          

            Mode = enMode.Update;

        }
        public static DataTable GetAllCountries()
        {
            return clsDataAccessCountries.GetAllCountries();

        }
        public static string GetCountryName(int CountryID)
        {
           return  clsDataAccessCountries.GetCountryname(CountryID);
        }


    }
}
