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
            Android.Widget.Button connectButton = FindViewById<Android.Widget.Button>(Resource.Id.button1);
            var txt = FindViewById<TextView>(Resource.Id.textview);
            connectButton.Click += (e, o) =>
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("Username", "a"));
                sqlParameters.Add(new SqlParameter("Password", "abc"));

                DataTable loginResults = DatabaseConnection.ExecSp("ValidateLogin", sqlParameters);
                if(loginResults.Rows.Count == 1)
                {
                    string user = loginResults.Rows[0]["Username"].ToString();
                    txt.Text = "OMG MERGE";
                }
            };
        }
    }
}