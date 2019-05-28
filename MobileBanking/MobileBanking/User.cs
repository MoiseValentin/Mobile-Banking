using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace MobileBanking
{
    public sealed class User
    {
        private static readonly User instance = new User();
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public int numberOfAccounts { get; set; }
        public int id { get; set; }
        public Account[] accountList = new Account[3];
        static User()
        {
        }
        private User()
        {
            for (int i = 0; i < 3; i++)
            {
                accountList[i] = new Account();
            }
        }
        public static User Instance
        {
            get
            {
                return instance;
            }
        }
    }
}