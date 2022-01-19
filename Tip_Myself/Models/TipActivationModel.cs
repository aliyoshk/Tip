using System;
namespace Tip_Myself.Models
{
    public class ToggleTip
    {
        public ToggleTip()
        {

        }
        public bool tipStatus { get; set; }

    }


    [Serializable]
    public class ToggleTipResponseModel
    {
        public ToggleTipResponseModel()
        {
        }

        public bool tipStatus { get; set; }

    }






    public class TipActivationModel
    {
        public TipActivationModel()
        {
        }

        public bool tipStatus { get; set; }
        public string tipPercent { get; set; }
    }

    [Serializable]
    public class TipActivationResponseModel
    {
        public TipActivationResponseModel()
        {
        }

        public string acctNumber { get; set; }
        public decimal walletBalance { get; set; }
        public bool tipStatus { get; set; }
        public string tipPercent { get; set; }

    }


 

}
