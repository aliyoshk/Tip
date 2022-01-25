
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "TransactionHistory")]
    public class TransactionHistory : Activity
    {

        RecyclerView lv;
        TransactionAdapter adapter;
        RecyclerView.LayoutManager layoutManger;

        List<TransactionList> lists = new List<TransactionList>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.transaction_history);

            //TransactionList lists = new TransactionList();
            
            lv = FindViewById<RecyclerView>(Resource.Id.lv);
            lv.HasFixedSize = true;
            layoutManger = new LinearLayoutManager(this);
            lv.SetLayoutManager(layoutManger);
            adapter = new TransactionAdapter(lists);
            lv.SetAdapter(adapter);

            TransactionList list = new TransactionList();
            //list.Amount = am;
            //lists.Add(list);

            _ = InitData();
        }

        private async Task InitData()
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                HistoryResponseModel historyResponse = new HistoryResponseModel() { };
                string send = JsonConvert.SerializeObject(historyResponse);
                var mResult = await NetworkUtil.GetGeneralAsycTip("Transactions/TransactionHistory", "AcctNumber", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<List<HistoryResponseModel>>(mResult);

                  foreach (var item in mm)
                    {
                        lists.Add(new TransactionList()
                        {
                            RefId = item.transactionUniqueReference,
                            Amount = item.transactionAmount,
                            Desination = item.transactionDestinationAccount,
                            Source = item.transactionSourceAccount,
                            Date = DateTime.Parse(item.transactionDate).ToString("MM/dd/yyyy hh:mm tt")
                        });
                    }
                    adapter.swapData(lists);
                    adapter.NotifyDataSetChanged();
                        /*if (mm != null)
                        {
                            /*TransactionList l = new TransactionList();
                            l.Amount = mm.transactionAmount;
                            l.Desination = mm.transactionDestinationAccount;
                            l.Source = mm.transactionSourceAccount;
                            l.RefId = mm.transactionUniqueReference;*/

                      

                        //lists.Add(l); 

                    }
                }
            }
        }

    }
