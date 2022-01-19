using System;
namespace Tip_Myself.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
        }

        
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string transactionPin { get; set; }
        public string password { get; set; }
 }

    [Serializable]
    public class RegisterResponseModel
    {
        public RegisterResponseModel()
        {
        }

        public string acctNumber { get; set; }
        public string acctName { get; set; }
        public string email { get; set; }
        public decimal acctBalance { get; set; }
    }
}
