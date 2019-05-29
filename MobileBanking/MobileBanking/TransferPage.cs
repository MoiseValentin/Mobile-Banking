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
    public class TransferPage : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.transfer_page);
            var radioButtonAccount1 = FindViewById<RadioButton>(Resource.Id.radioAccount1);
            var radioButtonAccount2 = FindViewById<RadioButton>(Resource.Id.radioAccount2);
            var radioButtonAccount3 = FindViewById<RadioButton>(Resource.Id.radioAccount3);
            Android.Widget.Button buttonNewTransfer = FindViewById<Android.Widget.Button>(Resource.Id.buttonTransfer); 
            var ibanForTransfer = FindViewById<TextView>(Resource.Id.textInsertIbanTransfer);
            var balanceForTransfer = FindViewById<TextView>(Resource.Id.textInsertBalance);
            User currentUser = User.Instance;
            int index, ok;
            radioButtonAccount2.Visibility = ViewStates.Invisible;
            radioButtonAccount3.Visibility = ViewStates.Invisible;

            radioButtonAccount1.Text = "Account 1 (" + currentUser.accountList[0].balance.ToString() + "  " + currentUser.accountList[0].currency + ")";
              
            if (currentUser.numberOfAccounts > 1)
            {
                radioButtonAccount2.Text = "Account 2 (" + currentUser.accountList[1].balance.ToString() + "  " + currentUser.accountList[1].currency + ")";
                radioButtonAccount2.Visibility = ViewStates.Visible;
            }
            if (currentUser.numberOfAccounts == 3)
            {
                radioButtonAccount3.Text = "Account 3 (" + currentUser.accountList[2].balance.ToString() + "  " + currentUser.accountList[2].currency + ")";
                radioButtonAccount3.Visibility = ViewStates.Visible;
            }

            buttonNewTransfer.Click += (e, o) =>
            {
                ok = 1;
                if (radioButtonAccount1.Checked == true)
                {
                    index = 0;
                }
                else if (radioButtonAccount2.Checked == true)
                {
                    index = 1;
                }
                else
                {
                    index = 2;
                }
                if ((float)Convert.ToDouble(balanceForTransfer.Text) > currentUser.accountList[index].balance)
                {
                    Toast.MakeText(ApplicationContext, "You don't have enough money", ToastLength.Long).Show();
                    ok = 0;
                }
                List<SqlParameter> sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("Iban", ibanForTransfer.Text)
                };
                DataTable ibanResults = DatabaseConnection.ExecSp("ValidateIban", sqlParameters);
                if (ibanResults.Rows.Count != 1)
                {
                    Toast.MakeText(ApplicationContext, "Invalid IBAN", ToastLength.Long).Show();
                    ok = 0;
                }
                if(ok == 1)
                {
                    int index2 = 0;
                    currentUser.accountList[index].balance = currentUser.accountList[index].balance - (float)Convert.ToDouble(balanceForTransfer.Text);
                    List<SqlParameter> sqlParameters2 = new List<SqlParameter>
                    {
                    new SqlParameter("Balance", currentUser.accountList[index].balance),
                    new SqlParameter("Id", currentUser.id)
                    };
                    int indexSql = index + 1;
                    DatabaseConnection.ExecSp("UpdateBalance" + indexSql.ToString(), sqlParameters2);
                    DataRow storedResults = ibanResults.Rows[0];
                    for (int i = 1; i <= Convert.ToInt32(storedResults["NumberOfAccounts"]); i++)
                    {
                        if (ibanForTransfer.Text == storedResults["Account" + i.ToString() + "_Iban"].ToString()) index2 = i;
                    }
                    float newbalance;
                    if (currentUser.accountList[index].currency == "RON")
                    {
                        if(storedResults["Account" + index2.ToString() + "_Currency"].ToString() == "EUR")
                            newbalance = (float)Convert.ToDouble(storedResults["Account" + index2.ToString() + "_Balance"]) + ((float)Convert.ToDouble(balanceForTransfer.Text) / (float)4.76);
                        else
                            newbalance = (float)Convert.ToDouble(storedResults["Account" + index2.ToString() + "_Balance"]) + (float)Convert.ToDouble(balanceForTransfer.Text);
                    }
                    else
                    {
                        if (storedResults["Account" + index2.ToString() + "_Currency"].ToString() == "RON")
                            newbalance = (float)Convert.ToDouble(storedResults["Account" + index2.ToString() + "_Balance"]) + ((float)Convert.ToDouble(balanceForTransfer.Text) * (float)4.76);
                        else
                            newbalance = (float)Convert.ToDouble(storedResults["Account" + index2.ToString() + "_Balance"]) + (float)Convert.ToDouble(balanceForTransfer.Text);
                    }
                    
                    List<SqlParameter> sqlParameters3 = new List<SqlParameter>
                    {
                    new SqlParameter("Balance", newbalance),
                    new SqlParameter("Id", storedResults["Id"])
                    };
                    DatabaseConnection.ExecSp("UpdateBalance" + index2.ToString(), sqlParameters3);
                    Toast.MakeText(ApplicationContext, "Transfer successful", ToastLength.Long).Show();
                    Finish();
                }
            };
            
        }
    }
}