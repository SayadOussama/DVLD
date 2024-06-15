using DVLDProject_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode _Mode = enMode.AddNew;
        public int UserID { set; get; }
        public string UserName { set; get; }
        public int PersonID { set; get; }
        public string PassWord { set; get; }
        public bool IsActive { set; get; }
        public clsUsers()
        {
            this.UserID = -1;
            this.UserName = "";
            this.PersonID = -1;
            this.PassWord = "";
            this.IsActive = false;
            _Mode = enMode.AddNew;
        }
        private clsUsers(int UserID, int PersonID, string userName, string passWord, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = userName;
            this.PassWord = passWord;
            this.IsActive = isActive;
            _Mode = enMode.UpdateNew;
        }
        public static clsUsers FindUserByID(int UserID)
        {
            // PersonID = -1;
            int PersonID = -1;
            bool IsActive  =false;
            string PassWord = "", UserName = "";

            if (clsDataAccessUsers.GetUserByUserID(UserID, ref PersonID, ref UserName,
           ref PassWord, ref IsActive))



                return new clsUsers(UserID, PersonID, UserName, PassWord, IsActive);
            else
                return null;

        }
        public static clsUsers FindUserByUserName(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            string PassWord = ""; //, UserName = "";

            if (clsDataAccessUsers.GetUserByUserName(ref UserID, ref PersonID,  UserName,
           ref PassWord, ref IsActive))



                return new clsUsers(UserID, PersonID, UserName, PassWord, IsActive);
            else
                return null;

        }

        private bool _AddNewUser()
        {
            
            this.UserID = clsDataAccessUsers.AddNewUser(this.PersonID, this.UserName, this.PassWord, this.IsActive);

            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {

            return clsDataAccessUsers.UpdateUser(this.UserID, this.PersonID, this.UserName, this.PassWord, this.IsActive);


        }
        public static string FindUserName(string userName)
        {
            return clsDataAccessUsers.GetUserName(userName);
        }
        public static string FindPassWord(string passWord)
        {
            return clsDataAccessUsers.GetPassWord(passWord);
        }
        public static bool IsActiveUser(string userName)
        {
            return clsDataAccessUsers.UserIsActive(userName);
        }
        public static DataTable GetAllUsers()
        {
            return clsDataAccessUsers.GetAllUsers();

        }
        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:

                    return _UpdateUser();


            }
            return false;
        }

        public static bool UserExistByPersonID(int PersonID)
        {
            return clsDataAccessUsers.IsUserExistByPersonID(PersonID);
        }
        public static bool DeleteUser(int UserID)
        {
            return clsDataAccessUsers.DeleteUser(UserID);
        }
    }
}
