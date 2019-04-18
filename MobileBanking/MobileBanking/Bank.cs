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
        public void addInternTransaction(Transaction newTransaction)
        {
            internTransaction.Add(newTransaction);
        }
        public void addExternTransaction(String externBank, Transaction newTransaction)
        {
            externTransaction.Add(Tuple.Create(externBank, newTransaction));
        }
        private void makeInternTransaction()
        {
            if(bank == null)
            {
                getBank();///daca nu exista obiectul deja, il instantiez 
            }
            if(internTransaction.Count == NR_TRANSACTIONS)
            {
                lock (variableLock)
                {
                    foreach(Transaction transaction in internTransaction)
                    {
                        int i = 0;
                        Boolean searchUser = false;
                        while ( i<users.Count && searchUser == false)
                        {
                            if (users[i].getIbanUser().Equals(transaction.getIbanUserReciever()))
                            {
                                searchUser = true;
                                if(users[i].recieveMoney(transaction).Equals("SUCCES"))
                                {
                                    // daca tranzactia a fost efectuata cu succes => anunt userul
                                    transaction.setSuccessfulTransaction(true);
                                }else{
                                    //in cazul in care tranzactia nu poate fi efectuata, banii sunt mutati in contul celui care trimite si tranzactia este trecuta in failedTransaction
                                    transaction.setSuccessfulTransaction(false);
                                    int j=0;
                                    Boolean findUser = false;
                                    while (j < users.Count() && findUser==false)
                                    {
                                        if (transaction.getIbanUserEmitter().Equals(users[j].getIbanUser()))
                                        {/////de modificat
                                            users[i].addMoneyMyAccount(transaction.getMoneyCurrency(), transaction.getMoneyAmount());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    internTransaction.Clear();
                }
            }
            
        }


    }
}