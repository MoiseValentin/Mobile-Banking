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
    public class MainPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_page);
            var userName = FindViewById<TextView>(Resource.Id.textUserName);
            User currentUser = User.Instance;
            userName.Text = currentUser.firstName +" "+ currentUser.lastName;
            Android.Widget.Button buttonSeeAccounts = FindViewById<Android.Widget.Button>(Resource.Id.buttonAccounts);
            Android.Widget.Button buttonTransfer = FindViewById<Android.Widget.Button>(Resource.Id.buttonTransfer);
            buttonSeeAccounts.Click += (e, o) =>
            {
                Intent nextActivity = new Intent(this, typeof(AccountsPage));
                StartActivity(nextActivity);
            };
            buttonTransfer.Click += (e, o) =>
            {
                if(currentUser.numberOfAccounts == 0)
                {
                    Toast.MakeText(ApplicationContext, "You must have at least one account", ToastLength.Long).Show();
                }
                else
                {
                    Intent nextActivity = new Intent(this, typeof(TransferPage));
                    StartActivity(nextActivity);
                }
            };
        }
    }
}