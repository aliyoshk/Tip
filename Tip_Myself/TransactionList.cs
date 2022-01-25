using System;
namespace Tip_Myself
{
    public class TransactionList
    {
        public string RefId { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public string Desination { get; set; }
        public string Date { get; set; }

        public TransactionList(string RefId, decimal Amount, string Source, string Destination, string Date)
        {
            this.RefId = RefId;
            this.Amount = Amount;
            this.Source = Source;
            this.Desination = Desination;
            this.Date = Date;
        }

        public TransactionList()
        {
            
        }
    }

    public class TipHistoryList
    {
        public decimal Amount { get; set; }
        public decimal TipAmount { get; set; }
        public string TipPercent { get; set; }
        public string Date { get; set; }

        public TipHistoryList(decimal Amount, decimal TipAmount, string TipPercent, string Date)
        {
            this.Amount = Amount;
            this.TipAmount = TipAmount;
            this.TipPercent = TipPercent;
            this.Date = Date;
        }

        public TipHistoryList()
        {

        }

    }
}
