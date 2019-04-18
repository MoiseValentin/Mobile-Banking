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
        private String nameUser;
        private String cnpUser;
        private String ibanUser; 
        private String email;
        private Account[] accounts;
        private List<Transaction> failedTransaction;
        private List<Transaction> successfulTransaction;
        public User()
        {
            this.maintenanceFee = 0.0;
        }
        public User (String cnpUser, String ibanUser)
        {
            this.cnpUser = cnpUser;
            this.ibanUser = ibanUser;
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
        public Double get()
        {
            return this.maintenanceFee;
        }
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
                    return "SUCCESS";//: THE AMMOUNT WAS ADDED TO YOUR ACCOUNT
                }
                else {
                    i++;
                }
            }
            return "FAILURE";//: YOU DO NOT OWN AN ACCOUNT IN THAT CURRNCY
        }
        public String recieveMoney(Transaction transaction)
        {
            int i = 0;
            Boolean existingAccount = false;
            while (i<accounts.Length && existingAccount==false)
            {
                if (transaction.getMoneyCurrency().Equals(accounts[i].getMoneyCurrency()))
                {
                    existingAccount = true;
                    accounts[i].addMoneyAmount(transaction.getMoneyAmount());
                    /////vreau sa memorez tranzactiile de primire bani sau le scriu direct in baza de date si le iau de acolo prin Bank ???
                }
                else
                {
                    i++;
                }

            }
            if (i < accounts.Length)
            {
                return "SUCCES";
            }
            else
            {
                return "FAILURE";
            }
        }
        public void addFailedTransaction(Transaction transaction)
        {
            failedTransaction.Add(transaction);

        }
        public void addSuccessfulTransaction(Transaction transaction)
        {
            successfulTransaction.Add(transaction);
        }
    }
}