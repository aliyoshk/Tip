
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

namespace Tip_Myself
{
    [Activity(Label = "Registration")]
    public class Registration : Activity
    {
        EditText fname, sname, email, pin, password;
        Button create;
        RadioGroup radioGroup;
        TextView haveAccount;
        ImageView back;
        private ProgressDialog progress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_registration);
            fname = FindViewById<EditText>(Resource.Id.fname);
            sname = FindViewById<EditText>(Resource.Id.sname);
            email = FindViewById<EditText>(Resource.Id.email);
            pin = FindViewById<EditText>(Resource.Id.pin);
            password = FindViewById<EditText>(Resource.Id.password);
            create = FindViewById<Button>(Resource.Id.create);
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            haveAccount = FindViewById<TextView>(Resource.Id.haveAccount);
            back = FindViewById<ImageView>(Resource.Id.back);


            progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");
            progress.SetTitle("Loading.");


            back.Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);

	         };

            haveAccount.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Login));
                StartActivity(intent);
            };

            create.Click += delegate {

                Empty();
	        };
        }

        public bool Empty()
        {
            string fn = fname.Text.ToString();
            string sn = sname.Text.ToString();
            string em = email.Text.ToString();
            string pn = pin.Text.ToString();
            string ps = password.Text.ToString();
            int rd = radioGroup.CheckedRadioButtonId;

            if(fn == String.Empty)
            {
                fname.Error = "Enter your firstname";
                fname.RequestFocus();
                return true;
            }

            if (sn == String.Empty)
            {
                sname.Error = "Enter your surname";
                sname.RequestFocus();
                return true;
            }

            if (em == String.Empty)
            {
                email.Error = "Enter your email";
                email.RequestFocus();
                return true;
            }

            if (pn == String.Empty)
            {
                pin.Error = "Choose transaction Pin";
                pin.RequestFocus();
                return true;
            }

            if(pn.Trim().Length < 4 || pn.Trim().Length > 4)
            {
                pin.Error = "Pin Should be 4 digit";
                pin.RequestFocus();
                return true;
            }

            if (ps == String.Empty)
            {
                password.Error = "Choose a Password";
                password.RequestFocus();
                return true;
            }


            if (rd == -1)
            {
                Toast.MakeText(this, "Agree with policy", ToastLength.Short).Show();
                return true;
            }
            else
            {
                validateLogin(fn, sn, em, pn, ps);
                fn=""; sn = ""; em=""; pn=""; ps="";
            }

            return false;
        }


        private async void validateLogin(string firstName, string lastname, string email, string transactionPin, string password)
        {
            RegisterModel reg = new RegisterModel { firstName = firstName, lastname = lastname, email = email, transactionPin = transactionPin, password = password};
            progress.Show();
            string create = JsonConvert.SerializeObject(reg);
            var mResult = await NetworkUtil.PostGeneralAsyc("Users/CreateAccount", create);

            if (!string.IsNullOrEmpty(mResult))
            {
                var mm = JsonConvert.DeserializeObject<LoginResponseModel>(mResult);
                if (mm != null)
                {
                    string lo1g = JsonConvert.SerializeObject(mm);
                    Toast.MakeText(this, "Account Created Successful", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(Login));
                    StartActivity(intent);
                }
            }

            progress.Dismiss();
        }
    }
}
