using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DVLDProject_DataAccessLayer;
using System.Net.Sockets;

namespace DVLDProject_BusinessLayer
{
    public class ClsPeopel
    {
       public enum enMode { AddNew= 0 , UpdateNew=1}
       public enMode _Mode = enMode.AddNew;

        public int PersonID {  get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }


        //Empty Contructior 
        //To Upload Data From User In Presentation Layer 
     public  ClsPeopel()
        { 
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            _Mode = enMode.AddNew;
        
        }
      
        //private Contructor To Doing Update 

        private ClsPeopel(int PersonID , string NationalNo , string FirstName , string SecondName , string ThirdName , 
         string LastName , DateTime DateOfBirth , short Gender , string Address , string Phone ,string Email,int NationalityCountryID ,string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            _Mode = enMode.UpdateNew;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = clsDataAccessPeople.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,this.NationalityCountryID,this.ImagePath);

            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsDataAccessPeople.UpdatePerson(this.PersonID,this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            
        }
        public static ClsPeopel FindID(int PersonID)
        {
            // PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;  
            string NationalNo= "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonID(PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else return null;
        }
        public static ClsPeopel FindByNationalNo(string NationalNo)
        {
            int PersonID = -1;
           int NationalityCountryID = -1;
            short Gender = 0;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByNationalNo(ref PersonID,  NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))



                return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
              LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null; 
        }

        public static ClsPeopel FindByFirstName(string FirstName)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByFirstName(ref PersonID, ref NationalNo,  FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            return
                null;
        }
        public static ClsPeopel FindBySecondName(string SecondName)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", FirstName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonBySecondName(ref PersonID, ref NationalNo, ref FirstName,
            SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            return 
                
                null;
        }
        public static ClsPeopel FindByThirdName(string ThirdName)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", FirstName = "", SecondName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByThirdName(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else 
                return null;
        }
        public static ClsPeopel FindByLastName(string LastName)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", FirstName = "", SecondName = "",ThirdName="", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByLastName(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else 
                return null;
        }
        public static ClsPeopel FindByCountryName(string CountryName)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "",LastName= "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByCountryName(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath,CountryName)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

            else 
                return null;
        }

        public static ClsPeopel FindByGendor(short Gender)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByGendor(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth,  Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else 
                return null;
        }
        public static ClsPeopel FindByPhone(string Phone)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0; 
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByPhone(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address,  Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

            else
                return null;
        }


        public static ClsPeopel FindByEmail(string Email)
        {
            int PersonID = -1;
            int NationalityCountryID = -1;
            short Gender = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "",Phone ="", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsDataAccessPeople.GetPersonByEmail(ref PersonID, ref NationalNo, ref FirstName,
           ref SecondName, ref ThirdName, ref LastName,
           ref DateOfBirth, ref Gender, ref Address,ref Phone, Email, ref NationalityCountryID, ref ImagePath)) 



            return new ClsPeopel(PersonID, NationalNo, FirstName, SecondName, ThirdName,
          LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
           else 
                return null;
        }





        public static DataTable GetAllContacts()
        {
            return clsDataAccessPeople.GetAllPeople();

        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdatePerson();

            }




            return false;
        }

        public static bool isNationalNoExsit(string NationalNo)
        {
            return clsDataAccessPeople.IsNationalNoExist(NationalNo);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsDataAccessPeople.DeletePerson(PersonID);
        }
    }
}
