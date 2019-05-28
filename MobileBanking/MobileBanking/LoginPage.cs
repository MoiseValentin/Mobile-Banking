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
    public class LoginPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_page);
            Android.Widget.Button buttonLogIn = FindViewById<Android.Widget.Button>(Resource.Id.buttonLogIn);
            var emailInput = FindViewById<TextView>(Resource.Id.textInsertEmail);
            var passwordInput = FindViewById<TextView>(Resource.Id.textInsertPassword);
            buttonLogIn.Click += (e, o) =>
            {
                string password = PCLCrypto.GenerateHash(passwordInput.Text);
                List<SqlParameter> sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("Email", emailInput.Text),
                    new SqlParameter("Password", password)
                };

                DataTable loginResults = DatabaseConnection.ExecSp("ValidateLogin", sqlParameters);
                DataRow storedResults = loginResults.Rows[0];
                  if(loginResults.Rows.Count == 1)
                  {
                    User currentUser = User.Instance;                    
                    currentUser.password = passwordInput.Text;
                    currentUser.email = storedResults["Email"].ToString();
                    currentUser.firstName = storedResults["FirstName"].ToString();
                    currentUser.lastName = storedResults["LastName"].ToString();
                    currentUser.phoneNumber = storedResults["PhoneNumber"].ToString();
                    currentUser.birthDate = Convert.ToDateTime(storedResults["BirthDate"].ToString());
                    currentUser.numberOfAccounts = Convert.ToInt32(storedResults["NumberOfAccounts"]);
                    currentUser.id = Convert.ToInt32(storedResults["Id"]);
                    for (int i = 0; i < currentUser.numberOfAccounts; i++)
                    {
                        int j = i + 1;
                        currentUser.accountList[i].iban = storedResults["Account" + j.ToString() + "_Iban"].ToString();
                        currentUser.accountList[i].balance = (float)Convert.ToDouble(storedResults["Account" + j.ToString() + "_Balance"]);
                        currentUser.accountList[i].currency = storedResults["Account" + j.ToString() + "_Currency"].ToString();
                    }
                    Toast.MakeText(ApplicationContext, "Welcome!", ToastLength.Long).Show();
                    Intent nextActivity = new Intent(this, typeof(MainPage));
                    StartActivity(nextActivity);
                }
                  else
                {
                    Toast.MakeText(ApplicationContext, "Wrong email/password", ToastLength.Long).Show();
                }
            };

        }
    }
}