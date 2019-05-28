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
    public class NewAccountPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.new_account_page);
            Android.Widget.Button buttonNewAccount = FindViewById<Android.Widget.Button>(Resource.Id.buttonNewAccount);
            User currentUser = User.Instance;
            buttonNewAccount.Click += (e, o) =>
            {
                if (currentUser.numberOfAccounts < 3)
                {
                    var balance = FindViewById<TextView>(Resource.Id.textInsertBalance);
                    var radioButtonRON = FindViewById<RadioButton>(Resource.Id.radioRON);
                    var radioButtonEUR = FindViewById<RadioButton>(Resource.Id.radioEUR);
                    int ok = 0;
                    Account newAccount = currentUser.accountList[currentUser.numberOfAccounts];
                    if (radioButtonRON.Checked == true)
                    {
                        newAccount.currency = "RON";
                        ok = 1;
                    }
                    else if (radioButtonEUR.Checked == true)
                    {
                        newAccount.currency = "EUR";
                        ok = 1;
                    }
                    else Toast.MakeText(ApplicationContext, "You must select the currency type", ToastLength.Long).Show();
                    if (ok == 1)
                    {
                        if (balance.Text.ToString() == "") newAccount.balance = 0;
                        else newAccount.balance = (float)Convert.ToDouble(balance.Text);
                        newAccount.iban = newAccount.GenerateIban();
                        currentUser.accountList[currentUser.numberOfAccounts] = newAccount;
                        currentUser.numberOfAccounts++;
                        List<SqlParameter> sqlParameters = new List<SqlParameter>
                        {
                        new SqlParameter("Id", currentUser.id),
                        new SqlParameter("Iban", newAccount.iban),
                        new SqlParameter("Currency", newAccount.currency),
                        new SqlParameter("Balance",newAccount.balance),
                        new SqlParameter("NumberOfAccounts",currentUser.numberOfAccounts)
                        };

                        DatabaseConnection.ExecSp("CreateNewAccount" + currentUser.numberOfAccounts.ToString(), sqlParameters);
                    }
                }
                else Toast.MakeText(ApplicationContext, "Maximum number of accounts reached", ToastLength.Long).Show();
                Finish();
            };

        }
    }
}