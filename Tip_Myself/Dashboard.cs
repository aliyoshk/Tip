using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "Dashboard")]
    public class Dashboard : AppCompatActivity
    {
        ImageView img_tip, btnSend, transHistory, wallet,  card;
        TextView acctBalance, acctNumber, acctName;


        private LoginResponseModel mIntentData;


        protected override void OnResume()
        {
            base.OnResume();
            getUserDetails();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dashboard);
            acctBalance = FindViewById<TextView>(Resource.Id.textView4);
            acctNumber = FindViewById<TextView>(Resource.Id.acctNumber);
            acctName = FindViewById<TextView>(Resource.Id.acctName);
            transHistory = FindViewById<ImageView>(Resource.Id.imageView5);
            wallet = FindViewById<ImageView>(Resource.Id.imageView4);
            card = FindViewById<ImageView>(Resource.Id.imageView6);

            /*var mLoginResponse = Intent.GetStringExtra("LoginResponseModelK");
            if (!string.IsNullOrEmpty(mLoginResponse))
            {
                mIntentData = JsonConvert.DeserializeObject<LoginResponseModel>(mLoginResponse);

                decimal ab = mIntentData.acctBalance ;
                string an = mIntentData.acctNumber;
                string name = mIntentData.acctName;

                acctBalance.Text = "₦ "+ ab.ToString("N0");
                acctNumber.Text = an;
                acctName.Text = name;
       
            }*/
            getUserDetails();


            // Create your application here
            img_tip = FindViewById <ImageView>(Resource.Id.tip);
            btnSend = FindViewById<ImageView>(Resource.Id.btnSend);

            transHistory.Click += delegate
            {
                Intent intent = new Intent(this, typeof(TransactionHistory));
                StartActivity(intent);
            };

            img_tip.Click += delegate
            {

                StartActivity(new Intent(Application.Context, typeof(TipDashboardActivity)));
            };

            btnSend.Click += async delegate
            {
                //validateLogin();
                Intent intent = new Intent(this, typeof(SendMoneyNew));
                StartActivity(intent);
            };

            wallet.Click += delegate
            {
                Toast.MakeText(this, "E-Wallet Feature unavailable, check back later", ToastLength.Short).Show();
                //Intent intent = new Intent(this, typeof(Enable_Tip));
                //StartActivity(intent);
            };

            card.Click += delegate
            {
                Toast.MakeText(this, "Card Request Feature unavailable, check back later", ToastLength.Short).Show();
                //Intent i = new Intent(this, typeof(TipDashboard));
                //StartActivity(i);
            };

            BottomNavigationView bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            //RemoveShiftMode(bottomNavigation);

           

            /*bottomNavigation.NavigationItemSelected += (s, e) =>
            {
                switch (e.Item.ItemId)
                {
                    case Resource.Id.airtime:
                        Toast.MakeText(this, "Airtime Menu", ToastLength.Short).Show();
                        break;

                    case Resource.Id.payBills:
                        Toast.MakeText(this, "Pay Bills Menu", ToastLength.Short).Show();
                        break;

                    case Resource.Id.transfer:
                        Intent intent = new Intent(this, typeof(SendMoney));
                        StartActivity(intent);
                        break;

                    case Resource.Id.support:
                        Toast.MakeText(this, "Support Menu", ToastLength.Short).Show();
                        break;

                    case Resource.Id.settings:
                        Toast.MakeText(this, "Settings Menu", ToastLength.Short).Show();
                        Intent i = new Intent(this, typeof(TipDashboard));
                        StartActivity(i);
                        break;
                        break;
                }
            };*/
        }

     /*   private async void validateLogin()
        {

            LoginModel loginModel = new LoginModel() { email = "peace@gmail.com", password = "peace@gmail.com" };
            string log = JsonConvert.SerializeObject(loginModel);
            var mResult = await NetworkUtil.PostGeneralAsyc("Users/login", log);

            var mm = JsonConvert.DeserializeObject<LoginResponseModel>(mResult);
                    string lo1g = JsonConvert.SerializeObject(mm);
                    Intent intent = new Intent(this, typeof(SendMoney));

                    intent.PutExtra("DashboardDetails", lo1g);
                    StartActivity(intent);
        }*/


       
        private async void getUserDetails()
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                var mResult = await NetworkUtil.GetGeneralAsycTip("Users/UserDetails", "acctNum", user.acctNumber);
                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<LoginResponseModel>(mResult);
                    if (mm != null)
                    {
                        acctBalance.Text = "₦" +mm.acctBalance.ToString("N0");
                        acctName.Text = mm.acctName;
                        acctNumber.Text = mm.acctNumber;
                        MemoryManager.Instance(this).setUserAccount("LoginResponseModel",mm);
                    }
                }
            }
        }
    }
}