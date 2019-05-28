using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileBanking
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AccountsPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.accounts_page);
            Android.Widget.Button buttonNewAccount = FindViewById<Android.Widget.Button>(Resource.Id.buttonNewAccount);
            var userName = FindViewById<TextView>(Resource.Id.textUserName);
            User currentUser = User.Instance;
            userName.Text = currentUser.firstName + " " + currentUser.lastName;

            buttonNewAccount.Click += (e, o) =>
            {
                    Intent nextActivity = new Intent(this, typeof(NewAccountPage));
                    StartActivity(nextActivity);
            };


        }
    }
}