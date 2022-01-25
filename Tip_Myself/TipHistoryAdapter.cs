using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Tip_Myself
{
    class TipAdapterHolder : RecyclerView.ViewHolder
    {
        public TextView date { get; set; }
        public TextView amount { get; set; }
        public TextView tipAmount { get; set; }
        public TextView tipPercent { get; set; }
        public TipAdapterHolder(View itemView) : base(itemView)
        {
            tipAmount = itemView.FindViewById<TextView>(Resource.Id.tipAmount);
            date = itemView.FindViewById<TextView>(Resource.Id.date);
            amount = itemView.FindViewById<TextView>(Resource.Id.amount);
            tipPercent = itemView.FindViewById<TextView>(Resource.Id.tipPercent);
        }
    }


    public class TipHistoryAdapter : RecyclerView.Adapter
    {
        List<TipHistoryList> lists;
        public TipHistoryAdapter(List<TipHistoryList> list)
        {
            this.lists = list;
        }

        public override int ItemCount => lists.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TipAdapterHolder tipAdapter = holder as TipAdapterHolder;
            //tipAdapter.amount.Text = "Amount : ₦" + lists[position].Amount.ToString();
            tipAdapter.tipAmount.Text = "Tip amount : ₦" + lists[position].TipAmount.ToString();
            tipAdapter.tipPercent.Text = "Tip percent : " + lists[position].TipPercent;
            tipAdapter.date.Text = "Date : " + lists[position].Date;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.tip_history_adapter, parent, false);
            return new TipAdapterHolder(view);
        }

        internal void swapData(List<TipHistoryList> lists)
        {
            this.lists = lists;
            NotifyDataSetChanged();
        }
    }



    
}
