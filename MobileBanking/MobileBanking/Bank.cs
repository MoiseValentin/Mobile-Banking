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
    class Bank
    {
        private static List <Transaction> internTransaction;     //intre utilizatorii aceleiasi banci
        private static List <Tuple <String, Transaction> > externTransaction;     //intre un utilizator al bancii si un cont al altei banci
        private const int NR_TRANSACTIONS=100;
        private static List<User> users;
        /// instantierea unui singur obiect => folosim getBank
        private static Bank bank; //o singura instanta a bancii
        private static readonly object variableLock = new object();
        private Bank()
        {
            //constructor privat pentru singleton
        }
        public static Bank getBank() //pentru a instantia un (singur) obiect din clasa banca 
        {
            if( bank == null)
            {
                lock (variableLock)
                {
                    if(bank== null)
                    {
                        bank = new Bank();

                    }
                }

            }
            return bank;
        }

        //metode
        /*
        public void addInternTransaction(Transaction newTransaction)
        {
            internTransaction.Add(newTransaction);
        }
        */
        public User findUser(String ibanUser)
        {
            if (bank == null)
            {
                getBank();///daca nu exista obiectul deja, il instantiez 
            }
            lock (variableLock)
            {
                int i = 0;
                Boolean find = false;
                while (i < users.Count && find == false)
                {
                    if (users[i].getIbanUser().Equals(ibanUser))
                    {
                        find = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            } 
                if (find == true) return users[i];
                else return null;
     
        }
        public void addExternTransaction(String externBank, Transaction newTransaction)
        {
            externTransaction.Add(Tuple.Create(externBank, newTransaction));
        }
      
    }
}