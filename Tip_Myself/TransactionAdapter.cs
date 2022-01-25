using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Tip_Myself
{
    class TransactionAdapterHolder : RecyclerView.ViewHolder
    {
        public TextView refs { get; set; }
        public TextView date { get; set; }
        public TextView amount { get; set; }
        public TextView source { get; set; }
        public TextView destination { get; set; }
        public TransactionAdapterHolder(View itemView) : base(itemView)
        {
            refs = itemView.FindViewById<TextView>(Resource.Id.refs);
            date = itemView.FindViewById<TextView>(Resource.Id.date);
            amount = itemView.FindViewById<TextView>(Resource.Id.amount);
            source = itemView.FindViewById<TextView>(Resource.Id.source);
            destination = itemView.FindViewById<TextView>(Resource.Id.destination);
        }
    }

    class TransactionAdapter : RecyclerView.Adapter
    {
        List<TransactionList> lists;

        public TransactionAdapter(List<TransactionList> list)
        {
            this.lists = list;
          
        }

        //public override int ItemCount => lists == null ? 0 : lists.Count;
        public override int ItemCount => lists.Count;


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TransactionAdapterHolder transactionAdapter = holder as TransactionAdapterHolder;
            transactionAdapter.refs.Text = "Reference ID :"+ lists[position].RefId;
            transactionAdapter.date.Text = "Date :" + lists[position].Date;
            transactionAdapter.amount.Text = "Amount : ₦" + lists[position].Amount.ToString();
            transactionAdapter.source.Text = "Source Account :" + lists[position].Source;
            transactionAdapter.destination.Text = "Beneficiary :" + lists[position].Desination;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.transactionAdapter, parent, false);
            return new TransactionAdapterHolder(view);
        }

        internal void swapData(List<TransactionList> lists)
        {
            this.lists = lists;
            NotifyDataSetChanged();
        }
    }


}
