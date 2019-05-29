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
            var textNoAccounts = FindViewById<TextView>(Resource.Id.textNoAccounts);
            var textAccount1 = FindViewById<TextView>(Resource.Id.textAccount1);
            var textAccount1Currency = FindViewById<TextView>(Resource.Id.textAccount1Currency);
            var textAccount1Balance = FindViewById<TextView>(Resource.Id.textAccount1Balance);
            var textAccount1Iban = FindViewById<TextView>(Resource.Id.textAccount1Iban);
            var textAccount2 = FindViewById<TextView>(Resource.Id.textAccount2);
            var textAccount2Currency = FindViewById<TextView>(Resource.Id.textAccount2Currency);
            var textAccount2Balance = FindViewById<TextView>(Resource.Id.textAccount2Balance);
            var textAccount2Iban = FindViewById<TextView>(Resource.Id.textAccount2Iban);
            var textAccount3 = FindViewById<TextView>(Resource.Id.textAccount3);
            var textAccount3Currency = FindViewById<TextView>(Resource.Id.textAccount3Currency);
            var textAccount3Balance = FindViewById<TextView>(Resource.Id.textAccount3Balance);
            var textAccount3Iban = FindViewById<TextView>(Resource.Id.textAccount3Iban);
            if (currentUser.numberOfAccounts > 0)
            {
                textAccount1Currency.Text = currentUser.accountList[0].currency + ": ";
                textAccount1Balance.Text = currentUser.accountList[0].balance.ToString();
                textAccount1Iban.Text = "IBAN:  " + currentUser.accountList[0].iban;
                textAccount2Currency.Text = currentUser.accountList[1].currency + ": ";
                textAccount2Balance.Text = currentUser.accountList[1].balance.ToString();
                textAccount2Iban.Text = "IBAN:  " + currentUser.accountList[1].iban;
                textAccount3Currency.Text = currentUser.accountList[2].currency + ": ";
                textAccount3Balance.Text = currentUser.accountList[2].balance.ToString();
                textAccount3Iban.Text = "IBAN:  " + currentUser.accountList[2].iban;

                MakeAccountsInvisible();
                textNoAccounts.Visibility = ViewStates.Invisible;
                textAccount1.Visibility = ViewStates.Visible;
                textAccount1Currency.Visibility = ViewStates.Visible;
                textAccount1Balance.Visibility = ViewStates.Visible;
                textAccount1Iban.Visibility = ViewStates.Visible;
                if (currentUser.numberOfAccounts > 1)
                {
                    textAccount2.Visibility = ViewStates.Visible;
                    textAccount2Currency.Visibility = ViewStates.Visible;
                    textAccount2Balance.Visibility = ViewStates.Visible;
                    textAccount2Iban.Visibility = ViewStates.Visible;
                }
                if(currentUser.numberOfAccounts == 3)
                {
                    textAccount3.Visibility = ViewStates.Visible;
                    textAccount3Currency.Visibility = ViewStates.Visible;
                    textAccount3Balance.Visibility = ViewStates.Visible;
                    textAccount3Iban.Visibility = ViewStates.Visible;
                }

            }
            else
            {
                MakeAccountsInvisible();
            }
            void MakeAccountsInvisible()
            {
                textNoAccounts.Visibility = ViewStates.Visible;
                textAccount1.Visibility = ViewStates.Invisible;
                textAccount1Currency.Visibility = ViewStates.Invisible;
                textAccount1Balance.Visibility = ViewStates.Invisible;
                textAccount1Iban.Visibility = ViewStates.Invisible;
                textAccount2.Visibility = ViewStates.Invisible;
                textAccount2Currency.Visibility = ViewStates.Invisible;
                textAccount2Balance.Visibility = ViewStates.Invisible;
                textAccount2Iban.Visibility = ViewStates.Invisible;
                textAccount3.Visibility = ViewStates.Invisible;
                textAccount3Currency.Visibility = ViewStates.Invisible;
                textAccount3Balance.Visibility = ViewStates.Invisible;
                textAccount3Iban.Visibility = ViewStates.Invisible;
            }
            buttonNewAccount.Click += (e, o) =>
            {
                    Intent nextActivity = new Intent(this, typeof(NewAccountPage));
                    StartActivity(nextActivity);
                    Finish();
            };


        }
    }
}