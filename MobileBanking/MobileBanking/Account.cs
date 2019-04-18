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
    class Account
    {
        private String iban;
        private String moneyCurrency;
        private Double moneyAmount;
        private Double maintenanceFee; //comision de intretinere
        //private Transaction[] transactions;
        /// <summary>
        /// /\Userul ar trebui ca, dupa ce alege contul, daca are suficienti bani in cont sa poata sa trimita "un mesaj" bancii cu suma, descrierea si iban-ul contului in care 
        /// vrea sa transfere banii
        /// /\Banca, daca tranzactia este posibila, sa o efectueze si sa trimita mesaj userului
        ///          altfel, sa puna suma de bani in contul userului si sa-i trimita mesaj 
        /// /\ fiecare tranzactie efectuata sau neefrctuata, cu mesajul corespunzator, sa fie stocata in vectorul transactions din contul corespunzator
        /// </summary>

        private DateTime dateOpeningAccont;
        ///pentu fiecare cont, banca trebuie sa imi ia comision incepand cu data deschiderii 
        /// SI comisionul trebuie sa fie in functie de moneda in care este deschis contul
        public Account()
        {
            this.iban = "no iban set";
            this.moneyAmount = 0.0;
            this.moneyCurrency = "none";
        }
        public Account(String iban, String moneyCurency)
        {
            this.iban = iban;
            this.moneyAmount = 0.0;
            this.moneyCurrency = moneyCurency;
        }
        public void setMaintenaceFee() /////ar trebui sa afie setat de banca si sa nu poata fi modificat de User+
                                       //////pentru fiecare cont ar trebui sa avem alt comision????
        {
            this.maintenanceFee = maintenanceFee;
        }
        public void setIban(String iban)
        {
            this.iban = iban;
        }
        public void setMoneyAccount(String moneyCurrency)
        {
            this.moneyCurrency = moneyCurrency;
        }
        public void setMoneyAmount(Double moneyAmount)
        {
            this.moneyAmount = moneyAmount;
        }
        public String getIban()
        {
            return this.iban;
        }
        public String getMoneyCurrency()
        {
            return this.moneyCurrency;
        }
        public Double getMoneyAmount()
        {
            return this.moneyAmount;
        }
        public void addMoneyAmount(Double moneyAmount)
        {
            this.moneyAmount += moneyAmount;
        }
        public String extractMoney(Double extractMoneyAmount)
        {
            if (this.moneyAmount < extractMoneyAmount) return "FAILURE";
            else
            {
                this.moneyAmount -= extractMoneyAmount;
                return "SUCCESS";
            }
        }
        public void newInternBankTransaction (String ibanUserReciever, String moneyCurrency, Double moneyAmount)
        {
            if (this.extractMoney(moneyAmount).Equals("SUCCESS")) //daca are suficienti bani in cont 
            {
                Bank bankInstance = Bank.getBank();
                Transaction transaction = new Transaction(this.getIban(), ibanUserReciever);
                transaction.setMoney(moneyCurrency, moneyAmount);
                bankInstance.addInternTransaction(transaction);             
            }
        }
        public void newExternBankTransaction(String externBank, String ibanUserReciever, String moneyCurrency, Double moneyAmount)
        {
            /////////adaugam comision pentru banca?????
            if (this.extractMoney(moneyAmount).Equals("SUCCESS")) 
            {
                Bank bankInstance = Bank.getBank();
                Transaction transaction = new Transaction(this.getIban(), ibanUserReciever);
                transaction.setMoney(moneyCurrency, moneyAmount);
                bankInstance.addExternTransaction(externBank, transaction);
            }
        }
        
    }
}