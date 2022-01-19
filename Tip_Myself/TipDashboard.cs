
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "TipDashboard")]
    public class TipDashboard : Activity
    {
        TextView walBalance, status, percent;
        ImageView btnTip;
        private SendMoneyResponseModel mIntentData;

        protected override void OnResume()
        {
            base.OnResume();
            getTipDetails();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_tip_dashboard);

            walBalance = FindViewById<TextView>(Resource.Id.textView4);
            status = FindViewById<TextView>(Resource.Id.status);
            percent = FindViewById<TextView>(Resource.Id.textView5);
            btnTip = FindViewById<ImageView>(Resource.Id.btnTip);

            btnTip.Click += delegate {

                Intent intent = new Intent(this, typeof(Enable_Tip));
                StartActivity(intent);
	        };


            var user = MemoryManager.Instance(this).getTip("TipActivationResponseModel");
            if (user != null)
            {
                walBalance.Text = "₦ "+user.walletBalance.ToString();
                percent.Text = user.tipPercent + " %";
                status.Text =  user.tipStatus.ToString();

                if(user.tipStatus == true)
                {
                    
                    status.Text = "Active";
                }
                else
                {
                    status.Text = "Inactive";
                }
            }

            getTipDetails();

        }

            private async void getTipDetails()
            {
                var user = MemoryManager.Instance(this).getTip("TipActivationResponseModel");
                if (user != null)
                {
                    var mResult = await NetworkUtil.GetGeneralAsycTip("TipWallet/ActivateStatus", "acctNum", user.acctNumber);
                    if (!string.IsNullOrEmpty(mResult))
                    {
                        var mm = JsonConvert.DeserializeObject<TipActivationResponseModel>(mResult);
                        if (mm != null)
                        {
                        walBalance.Text = user.walletBalance.ToString();
                        status.Text = user.tipStatus.ToString();
                        percent.Text = user.tipPercent.ToString();
                        MemoryManager.Instance(this).setTipStatus("TipActivationResponseModel", mm);
                        }
                    }
                }
        }

    
    }
}
