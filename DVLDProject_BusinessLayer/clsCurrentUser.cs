using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_BusinessLayer
{
     public static  class clsCurrentUser
    {

      public  static int _UserID { set; get; }
        public static clsUsers _User; 
      //  static int CurrentUserID(int UserID) { }
      static public clsUsers _Users {  set; get; }
    }
}
