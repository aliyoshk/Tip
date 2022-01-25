
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
using AndroidX.AppCompat.App;
using Java.Lang;
using Newtonsoft.Json;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "Enable_Tip")]
    public class Enable_Tip : AppCompatActivity
    {
        LinearLayout l1, l2;
        Spinner percent, tipChoice;
        private string tipPercent;
        Switch sw;
        private ProgressDialog progress;
        ImageView back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_enable_tip);
            progress = new ProgressDialog(this);
            l1 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            l2 = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
             sw = FindViewById<Switch>(Resource.Id.switch1);
            percent = FindViewById<Spinner>(Resource.Id.percent);
            tipChoice = FindViewById<Spinner>(Resource.Id.tipChoice);
            back = FindViewById<ImageView>(Resource.Id.imageView7);
            getWalletStatus();

            back.Click += delegate {

                Intent intent = new Intent(this, typeof(Dashboard));
                StartActivity(intent);
                	};


            percent.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(percent_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.tip_percent, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            percent.Adapter = adapter;

            tipChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(tipChoice_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(this, Resource.Array.tip_transaction, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            tipChoice.Adapter = adapter1;

            sw.CheckedChange += (s, b) =>
            {
                bool isChecked = b.IsChecked;
                

                if (isChecked)
                {
                    // The toggle is enabled
                    Toast.MakeText(this, "Tip Activated", ToastLength.Short).Show();
                    l1.Visibility = ViewStates.Visible;
                    l2.Visibility = ViewStates.Visible;
                    //validateActivation(true, 0.ToString());
                    ToActivateTipPercent(true, percent.ToString());
                }
                else
                {
                    if (l1.Visibility == ViewStates.Visible){

                        //validateActivation(false, 0.ToString());
                        ToActivateTipPercent(false, 0.ToString());
                    }
                    // The toggle is disabled
                    l1.Visibility = ViewStates.Invisible;
                    l2.Visibility = ViewStates.Invisible;
                }
            };



        }
        private void percent_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            tipPercent = spinner.GetItemAtPosition(e.Position).ToString();//tipPercent

            string toast = string.Format("", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void tipChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            if (l1.Visibility == ViewStates.Visible)
            {
                ToActivateTipPercent(true, tipPercent);
                //validateActivation(true, tipPercent);
            }
           
        }




        private async void validateActivation(bool tipStatus, string tipPercent)
        {
            var nu = tipPercent;
            var hh = nu.Replace("%", "");
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null) {
                TipActivationModel tipActivation = new TipActivationModel() { tipStatus = tipStatus, tipPercent = hh };
                string tog = JsonConvert.SerializeObject(tipActivation);
                var mResult = await NetworkUtil.PostGeneralAsycTip("TipWallet/ToggleTipMyself", tog, "acctNum",user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<TipActivationResponseModel>(mResult);
                    if (mm != null)
                    {
                        if (mm.tipStatus)
                        {
                            MemoryManager.Instance(this).setTipStatus("TipActivationResponseModel", mm);
                            Toast.MakeText(this, "Tip Activated Successfully", ToastLength.Long).Show();
                            
                        }
                    }
                }
            }
        }


        private async void ToActivateTipPercent(bool tipStatus, string tipPercent)
        {
            var nu = tipPercent;
            var hh = nu.Replace("%", "");
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                TipActivationModel tipActivation = new TipActivationModel() { tipStatus = tipStatus, tipPercent = hh };
                string tog = JsonConvert.SerializeObject(tipActivation);
                var mResult = await NetworkUtil.PostGeneralAsycTip("TipWallet/ActivateStatus", tog, "acctNum", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<TipActivationResponseModel>(mResult);
                    if (mm != null)
                    {
                        if (mm.tipStatus)
                        {
                            MemoryManager.Instance(this).setTipStatus("TipActivationResponseModel", mm);
                            Toast.MakeText(this, "Tip Successfully Activated", ToastLength.Long).Show();
                        }
                    }
                }
            }

        }



        //private method of your class
        private int getIndex(Spinner spinner, string myString)
        {
            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(myString))
                {
                    return i;
                }
            }

            return 0;
        }


        private async void getWalletStatus()
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                var mResult = await NetworkUtil.GetGeneralAsycTip("Users/WalletDetail", "acctNumber", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<TipActivationResponseModel>(mResult);
                    if (mm != null)
                    {
                        if (mm.tipStatus)
                        {
                            sw.Checked = true;
                            percent.SetSelection(getIndex(percent,mm.tipPercent+"%"));
                            Toast.MakeText(this, "Tip Updated", ToastLength.Long).Show();
                            //.................
                            MemoryManager.Instance(this).setTipStatus("TipActivationResponseModel", mm);
                        }

                    }
                }
            }

        }
    }
    
}
