using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileBanking
{
    class User
    {
        private String SUCCESS="SUCCESS";
        private String FAILURE="FAILURE";
        private String nameUser;
        private String cnpUser;
        private String ibanUser; 
        private String email;
        private String password;
        private Account[] accounts;
        public User()
        {
        }
        public User ( String nameUser, String email, String cnpUser, String ibanUser)
        {///////creare User
            this.nameUser = nameUser;
            this.email = email;
            this.cnpUser = cnpUser;
            this.ibanUser = ibanUser;
        }
        public void loginUser(String email, String password)
        {
            ///verifica daca exista userul in baza de date 
        }
        public void setNameUser(String nameUser)
        {
            this.nameUser = nameUser;
        }
        public void setAccounts(Account[] accounts)
        {
            Account[] auxiliarAccounts = new Account[accounts.Length];
            for(int i=0; i<accounts.Length; i++)
            {
                auxiliarAccounts[i] = accounts[i];
            }
            this.accounts = auxiliarAccounts;
        }
        public String getNameUser()
        {
            return this.nameUser;
        }
        public String getCnpUser()
        {
            return this.cnpUser;
        }
        public String getIbanUser()
        {
            return this.ibanUser;
        }
        public void setEmail(String email)
        {
            this.email = email;
        }
       // public Double get()
        //{
           // return this.maintenanceFee;
        //}
        public void addAccount(String iban, String moneyCurency)
        {
            Account newAccount = new Account(iban, moneyCurency);
            Account[] auxiliarAccounts = new Account[accounts.Length + 1];
            for (int i = 0; i < accounts.Length; i++)
            {
                auxiliarAccounts[i] = accounts[i];
            }
            auxiliarAccounts[accounts.Length] = newAccount;
            this.accounts = auxiliarAccounts;
        }
        public String addMoneyMyAccount(String moneyCurrency, Double moneyAmount)
        {
            int i=0; //iterator
            while(i<this.accounts.Length)
            {
                if (this.accounts[i].getMoneyCurrency().Equals(moneyCurrency))
                {
                    this.accounts[i].addMoneyAmount(moneyAmount);
                    return SUCCESS;//: THE AMMOUNT WAS ADDED TO YOUR ACCOUNT
                }
                else {
                    i++;
                }
            }
            return FAILURE;//: YOU DO NOT OWN AN ACCOUNT IN THAT CURRNCY
        }
       
        public void sendMoney(String ibanReciever, String moneyAmount, String moneyCurrency)
        {
          
         //   User reciver=
        }
        
    }
}