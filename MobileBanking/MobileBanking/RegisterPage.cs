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
    public class RegisterPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.register_page);

            Android.Widget.Button buttonSignUp = FindViewById<Android.Widget.Button>(Resource.Id.buttonSignUp);
            var emailInput = FindViewById<TextView>(Resource.Id.textInsertEmail);
            var passwordInput = FindViewById<TextView>(Resource.Id.textInsertPassword);
            buttonSignUp.Click += (e, o) =>
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("Username", emailInput.Text));
                sqlParameters.Add(new SqlParameter("Password", passwordInput.Text));

                DataTable loginResults = DatabaseConnection.ExecSp("CreateUser", sqlParameters);

                Toast.MakeText(ApplicationContext, "Account created!", ToastLength.Long).Show();
            };
            

        }
    }
}