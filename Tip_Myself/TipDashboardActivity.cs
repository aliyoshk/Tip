
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
using AndroidX.SwipeRefreshLayout.Widget;
using Newtonsoft.Json;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "TipDashboardActivity")]
    public class TipDashboardActivity : Activity
    {
        SwipeRefreshLayout swipe;
        LinearLayout hide;
        TextView walletBalance, status, percentage;
        Switch switch1;
        Spinner percent, tipChoice;
        ImageView bck;
        private string tipPercent;
        TextView viewHistory;


        /*protected override void OnResume()
        {
            base.OnResume();
            getTipDetails();
        }*/

        /*public void Swipe_Refresh()
        {
            swipe.Refreshing = false;
            _ = getTipDetails();
        }*/

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tip_dashboard_activation);
            // Create your application here

            hide = FindViewById<LinearLayout>(Resource.Id.hide);
            walletBalance = FindViewById<TextView>(Resource.Id.walletBalance);
            status = FindViewById<TextView>(Resource.Id.status);
            percentage = FindViewById<TextView>(Resource.Id.textView5);
            switch1 = FindViewById<Switch>(Resource.Id.switch1);
            percent = FindViewById<Spinner>(Resource.Id.percent);
            tipChoice = FindViewById<Spinner>(Resource.Id.tipChoice);
            bck = FindViewById<ImageView>(Resource.Id.bck);
            viewHistory = FindViewById<TextView>(Resource.Id.viewHistory);

            _ = getTipDetails(); //this method set all the wallet details onto the dashboard

            //HoldActivationDetails(); //shows the data hold which the user sets switch and percent

            swipe = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe);
            swipe.SetColorSchemeResources(Resource.Color.abc_background_cache_hint_selector_material_dark, Resource.Color.abc_color_highlight_material,
                Resource.Color.abc_hint_foreground_material_dark, Resource.Color.abc_primary_text_material_dark);
            swipe.Refresh += Swipe_Refresh;




            bck.Click += delegate {
                Intent intent = new Intent(this, typeof(Dashboard));
                StartActivity(intent);
           	};

            viewHistory.Click += delegate
            {
                Intent intent = new Intent(this, typeof(TipHistory));
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

            switch1.CheckedChange += (s, b) =>
            {
                bool isChecked = b.IsChecked;


                if (isChecked)
                {
                    // The toggle is enabled
                    //Toast.MakeText(this, "Tip Activated", ToastLength.Short).Show();
                    hide.Visibility = ViewStates.Visible;
                    //ToActivateTipPercent(true, 0.ToString());
                }
                else
                {
                    /*if (hide.Visibility == ViewStates.Visible)
                    {
                        ToActivateTipPercent(false, 0.ToString());
                    }*/

                    // The toggle is disabled
                    hide.Visibility = ViewStates.Invisible;
                    ToActivateTipPercent(false, 0.ToString());
                }
            };

        }

        private void Swipe_Refresh(object sender, EventArgs e)
        {
            swipe.Refreshing = false;
            _ = getTipDetails();
        }

        private void percent_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            tipPercent = spinner.GetItemAtPosition(e.Position).ToString();//tipPercent
            //string toast = string.Format("Your Percent is ", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void tipChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Your Selection is on", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();

            if (hide.Visibility == ViewStates.Visible)
            {
                ToActivateTipPercent(true, tipPercent.ToString());
            }

        }


        /*private async void validateActivation(bool tipStatus, string tipPercent)
        {
            var nu = tipPercent;
            var hh = nu.Replace("%", "");
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                TipActivationModel tipActivation = new TipActivationModel() { tipStatus = tipStatus, tipPercent = hh };
                string tog = JsonConvert.SerializeObject(tipActivation);
                var mResult = await NetworkUtil.PostGeneralAsycTip("TipWallet/ToggleTipMyself", tog, "acctNum", user.acctNumber);
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
        }*/

        /*private async void validateActivation(bool tipStatus)
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                ToggleTip togg = new ToggleTip() { tipStatus = tipStatus};
                string tog = JsonConvert.SerializeObject(togg);

                var mResult = await NetworkUtil.PostGeneralAsycTip("TipWallet/ToggleTipMyself", tog, "acctNum", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<ToggleTipResponseModel>(mResult);
                    if (mm != null)
                    {
                        if (mm.tipStatus)
                        {
                            MemoryManager.Instance(this).setToggleStatus("ToggleTipResponseModel", mm);
                            Toast.MakeText(this, "Tip Switch on", ToastLength.Long).Show();

                        }
                    }
                }
            }
        }*/



        /*private async void ToActivateTipPercent(bool tipStatus, string tipPercent)
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


                        private async void HoldActivationDetails()
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
                                            switch1.Checked = true;
                                            percent.SetSelection(getIndex(percent, mm.tipPercent + "%"));
                                            Toast.MakeText(this, "Tip is Active", ToastLength.Long).Show();
                                        }

                                    }
                                }
                            }

                        }*/
        //..............Holding Spinner and Switch State....................

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
        //.......End of Spinner and Switch State Holder...............



        //..............Settings Dashboard details....................
        private async Task getTipDetails()
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                WalletDetails walletDetails = new WalletDetails() { };
                string details = JsonConvert.SerializeObject(walletDetails);
                var mResult = await NetworkUtil.GetGeneralAsycTip("Users/WalletDetail", "acctNumber", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<TipActivationResponseModel>(mResult);
                    if (mm != null)
                    {
                        bool st = false;
                        if (mm.tipStatus == st)
                        {
                            status.Text = "Inactive";
                            switch1.Checked = false;
                        }
                        else
                        {
                            status.Text = "Active";
                            switch1.Checked = true;
                            Toast.MakeText(this, "Tip is Active", ToastLength.Short).Show();
                        }

                        //status.Text = mm.tipStatus.ToString();
                        walletBalance.Text = "₦ " + mm.walletBalance.ToString("N0");
                        percentage.Text = mm.tipPercent + "%";
                        percent.SetSelection(getIndex(percent, mm.tipPercent + "%")); // holding the percent of the spinner
                    }
                }
            }
       
        }//.......End of Tip Dashboard Settings...............



        //..............Settings WalletToggleStatus details....................
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

        }//.......End of WalletToggleStatus  Settings...............

    }
}
