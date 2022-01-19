using System;
namespace Tip_Myself.Models
{
    public class SendMoneyModel
    {
        public SendMoneyModel()
        {
        }
        public string toAccount { get; set; }
        public decimal amount { get; set; }
        public string transactionPin { get; set; }
    }

    [Serializable]
    public class SendMoneyResponseModel
    {
        public SendMoneyResponseModel()
        {
        }

        public decimal transactionAmount { get; set; }
        public int tipPercent { get; set; }
        public bool tipAmount { get; set; }
        public string date { get; set; }

    }

    [Serializable]
    public class HistoryResponseModel
    {
        public HistoryResponseModel()
        {
        }

        public string transactionUniqueReference { get; set; }
        public decimal transactionAmount { get; set; }
        public string transactionSourceAccount { get; set; }
        public string transactionDestinationAccount { get; set; }
        public string transactionDate { get; set; }

    }
}
