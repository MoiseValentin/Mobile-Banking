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

    class Transaction
    {
        private String ibanUserEmitter;
        private String ibanUserReciever;
        private String descriptonTransaction;
        private String nameUserEmitter;
        private String nameUserReciever;
        private String cnpUserEmitter;
        private String cnpUserReciever;
        private Double moneyAmount;
        private String moneyCurrency; // moneda (dolar, euro, etc)
        private DateTime dateTransaction;
        private Boolean successfulTransaction;
        public Transaction()
        {
            this.ibanUserEmitter = "00";
            this.ibanUserReciever = "00";

        }
        public Transaction( String ibanUserEmitter, String ibanUserReciever)
        {
            this.ibanUserEmitter = ibanUserEmitter;
            this.ibanUserReciever = ibanUserReciever;
        }
        public void setMoney( String moneyCurrency, Double moneyAmount)
        {
            this.moneyCurrency = moneyCurrency;
            this.moneyAmount = moneyAmount;
        }
        public void setDescriptionTransaction(String descriptionTransaction)
        {
            this.descriptonTransaction = descriptonTransaction;
        }
        public void setUserEmitter(String nameUserEmitter, String cnpUserEmitter)
        {
            this.nameUserEmitter = nameUserEmitter;
            this.cnpUserEmitter = cnpUserEmitter;
        }
        public void setUserReciever(String nameUserReciever, String cnpUserReciever)
        {
            this.nameUserReciever = nameUserReciever;
            this.cnpUserReciever = cnpUserReciever;
        }
        public void setDateTransaction( DateTime dateTransaction)
        {
            this.dateTransaction = dateTransaction;
        }
        public String getIbanUserEmitter()
        {
            return this.ibanUserEmitter;
        }
        public String getIbanUserReciever()
        {
            return this.ibanUserReciever;
        }
        public String getNameUserEmitter()
        {
            return this.nameUserEmitter;
        }
        public String getNameUserReciever()
        {
            return this.nameUserReciever;
        }
        public Double getMoneyAmount()
        {
            return this.moneyAmount;
        }
        public String getMoneyCurrency()
        {
            return this.moneyCurrency;
        }
        public DateTime getDateTransaction()
        {
            return this.dateTransaction;
        }
        public void setSuccessfulTransaction(Boolean value)
        {
            this.successfulTransaction = value;
        }



    }

}