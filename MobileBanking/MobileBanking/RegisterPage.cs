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
            buttonSignUp.Click += (e, o) =>
            {
                string password = PCLCrypto.GenerateHash(passwordInput.Text);
                List<SqlParameter> sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("Email", emailInput.Text),
                    new SqlParameter("Password", password),
                    new SqlParameter("FirstName", firstName.Text),
                    new SqlParameter("LastName", lastName.Text),
                    new SqlParameter("PhoneNumber", phoneNumber.Text),
                    new SqlParameter("BirthDate", birthDate.Text)
                };

                DataTable loginResults = DatabaseConnection.ExecSp("CreateUser", sqlParameters);

                Toast.MakeText(ApplicationContext, "Account created!", ToastLength.Long).Show();
            };
            

        }
    }
}