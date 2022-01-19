using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace Tip_Myself
{
    [Activity(Label = "@string/app_name")]
    public class MainActivity : AppCompatActivity
    {
        Button btnLogin, register;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            register = FindViewById<Button>(Resource.Id.register);

            btnLogin.Click += delegate
            {
                StartActivity(new Intent(Application.Context, typeof(Login)));
            };

            register.Click += delegate
            {
                StartActivity(new Intent(Application.Context, typeof(Registration)));
            };

        }
    }
}