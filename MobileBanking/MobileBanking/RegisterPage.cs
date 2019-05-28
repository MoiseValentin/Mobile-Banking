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
            SetContentView(Resource.Layout.register_page);
            Android.Widget.Button buttonSignUp = FindViewById<Android.Widget.Button>(Resource.Id.buttonSignUp);
            var emailInput = FindViewById<TextView>(Resource.Id.textInsertEmail);
            var passwordInput = FindViewById<TextView>(Resource.Id.textInsertPassword);
            var firstName = FindViewById<TextView>(Resource.Id.textInsertFirstName);
            var lastName = FindViewById<TextView>(Resource.Id.textInsertLastName);
            var phoneNumber = FindViewById<TextView>(Resource.Id.textInsertPhoneNumber);
            var birthDate = FindViewById<TextView>(Resource.Id.textInsertBirthDate);
            int ok;
            string password="";
            buttonSignUp.Click += (e, o) =>
            {
                ok = 1;
                if(passwordInput.Text.Length <= 3)
                {
                    Toast.MakeText(ApplicationContext, "Password must be at least 4 characters long", ToastLength.Long).Show();
                    ok = 0;
                }
                else
                {
                    password = PCLCrypto.GenerateHash(passwordInput.Text);
                }
                if(emailInput.Text.Length <= 6 || !emailInput.Text.Contains('@'))
                {
                    Toast.MakeText(ApplicationContext, "Invalid email", ToastLength.Long).Show();
                    ok = 0;
                }
                if(firstName.Text.Length <= 2 || lastName.Text.Length <=2)
                {
                    Toast.MakeText(ApplicationContext, "Invalid name", ToastLength.Long).Show();
                    ok = 0;
                }
                if(phoneNumber.Text.Length <= 7)
                {
                    Toast.MakeText(ApplicationContext, "Invalid phone number", ToastLength.Long).Show();
                    ok = 0;
                }
                if (ok == 1)
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>
                    {
                    new SqlParameter("Email", emailInput.Text)
                    };
                    DataTable validateEmailResults = DatabaseConnection.ExecSp("ValidateEmail", sqlParameters);
                    if (validateEmailResults.Rows.Count > 0)
                    {
                        Toast.MakeText(ApplicationContext, "Email already used", ToastLength.Long).Show();
                    }
                    else
                    {
                        List<SqlParameter> sqlParameters2 = new List<SqlParameter>
                        {
                        new SqlParameter("Email", emailInput.Text),
                        new SqlParameter("Password", password),
                        new SqlParameter("FirstName", firstName.Text),
                        new SqlParameter("LastName", lastName.Text),
                        new SqlParameter("PhoneNumber", phoneNumber.Text),
                        new SqlParameter("BirthDate", birthDate.Text)
                        };

                        DatabaseConnection.ExecSp("CreateUser", sqlParameters2);

                        Toast.MakeText(ApplicationContext, "Account created!", ToastLength.Long).Show();
                        Finish();
                    }
                }
            };
        }
    }
}