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
}
