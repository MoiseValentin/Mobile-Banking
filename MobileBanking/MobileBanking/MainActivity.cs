using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms;
using Android.Content;

namespace MobileBanking
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Android.Widget.Button buttonLogIn = FindViewById<Android.Widget.Button>(Resource.Id.buttonLogInMain);
            buttonLogIn.Click += (e, o) =>
            {
                Intent nextActivity = new Intent(this, typeof(LoginPage));
                StartActivity(nextActivity);
            };
            Android.Widget.Button buttonSignUp = FindViewById<Android.Widget.Button>(Resource.Id.buttonSignUpMain);
            buttonSignUp.Click += (e, o) =>
            {
                Intent nextActivity = new Intent(this, typeof(RegisterPage));
                StartActivity(nextActivity);
            };

        }
    }
}