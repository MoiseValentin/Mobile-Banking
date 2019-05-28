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
    public class Account
    {
        public string iban { get; set; }
        public string currency { get; set; }
        public float balance { get; set; }
        public string GenerateIban()
        {
            string iban = "RO49AAAA1B3100759384";

            Random rnd = new Random();
            int randomIban = rnd.Next(1000, 9999);
            iban = iban + randomIban.ToString();
            List<SqlParameter> sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("Iban", iban)
                };
            DataTable ibanResults = DatabaseConnection.ExecSp("ValidateIban", sqlParameters);
            if (ibanResults.Rows.Count == 0)
            {
                return iban;
            }
            else
            {
                return GenerateIban();
            }
        }
        

    }
}