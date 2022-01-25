
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Newtonsoft.Json;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "TipHistory")]
    public class TipHistory : Activity
    {
        RecyclerView rec;
        TipHistoryAdapter adapter;
        RecyclerView.LayoutManager layoutManger;

        List<TipHistoryList> lists = new List<TipHistoryList>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tipHistory);
            // Create your application here
            rec = FindViewById<RecyclerView>(Resource.Id.rec);
            rec.HasFixedSize = true;
            layoutManger = new LinearLayoutManager(this);
            rec.SetLayoutManager(layoutManger);
            adapter = new TipHistoryAdapter(lists);
            rec.SetAdapter(adapter);

            _ = tipHistory();
        }

        private async Task tipHistory()
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                HistoryResponseModel historyResponse = new HistoryResponseModel() { };
                string send = JsonConvert.SerializeObject(historyResponse);
                var mResult = await NetworkUtil.GetGeneralAsycTip("Transactions/WalletHistory", "AcctNumber", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<List<TipHistoryList>>(mResult);

                    foreach (var item in mm)
                    {
                        lists.Add(new TipHistoryList()
                        {
                            //Amount = item.Amount,
                            TipAmount = item.TipAmount,
                            TipPercent = item.TipPercent,
                            Date = DateTime.Parse(item.Date).ToString("MM/dd/yyyy hh:mm tt")
                    });
                    }

                    adapter.swapData(lists);
                    adapter.NotifyDataSetChanged();
                
                }
            }
        }
    }
}
