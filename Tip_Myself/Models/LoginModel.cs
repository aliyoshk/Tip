using System;
using System.Runtime.Serialization;

namespace Tip_Myself.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
        }

        public string email { get; set; }
        public string password { get; set; }
    }

    [Serializable]
    public class LoginResponseModel 
    {
        public LoginResponseModel()
        {
        }

        public string acctNumber { get; set; }
        public string acctName { get; set; }
        public string email { get; set; }
        public decimal acctBalance { get; set; }
        //


    }

   
}
