using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tip_Myself.Models;
using Tip_Myself.Utils;
using Xamarin.Essentials;

namespace Tip_Myself
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {

        ImageView back;
        Button btnLogin;
        EditText et_email, et_password;
        private ProgressDialog progress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            progress = new ProgressDialog(this);
            // Create your application here
            back = FindViewById<ImageView>(Resource.Id.back);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            et_email = FindViewById<EditText>(Resource.Id.et_email);
            et_password = FindViewById<EditText>(Resource.Id.et_password);

            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");
            progress.SetTitle("Loading.");

            btnLogin.Click += delegate
            {
                string em = et_email.Text.ToString();
                string ps = et_password.Text.ToString();


                if (em == string.Empty)
                {
                    Toast.MakeText(this, "Enter your Email", ToastLength.Long).Show();
                }
                else if (ps == string.Empty)
                {
                    Toast.MakeText(this, "Enter your Password", ToastLength.Long).Show();
                }
                else
                {
                    validateLogin(em, ps);

                } 
                
            };

            
            back.Click += delegate
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            };
        }

        private async void validateLogin(string email, string password)
        {
                //LoginModel loginModel = new LoginModel() { email = "peace@gmail.com", password = "peace@gmail.com" };
                LoginModel loginModel = new LoginModel() { email = email, password = password};
              progress.Show();
                string log = JsonConvert.SerializeObject(loginModel);
                var mResult = await NetworkUtil.PostGeneralAsyc("Users/login", log);
          

                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<LoginResponseModel>(mResult);
                    if (mm != null)
                    {
                     MemoryManager.Instance(this).setUserAccount("LoginResponseModel", mm);
                        string lo1g = JsonConvert.SerializeObject(mm);
                        
                        Intent intent = new Intent(this, typeof(Dashboard));

                        intent.PutExtra("LoginResponseModelK", lo1g);
                        StartActivity(intent);
                         Finish();
                    }
                }

                else
                {
                    Toast.MakeText(this, "Oops... Check You Connection an try Again", ToastLength.Short).Show();
                }
                progress.Dismiss();
        }
      
    }
}