using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnPrintType
    {
        private long PrintTypeID;
        private string PrintName;
        private decimal SinglePagePrice;
        public long printTypeID
        {
            get
            {
                return PrintTypeID;
            }
            set
            {
                PrintTypeID = value;
            }
        }
        public string printName
        {
            get
            {
                return PrintName;
            }
            set
            {
                PrintName = value;
            }
        }
        public decimal singlePagePrice
        {
            get
            {
                return SinglePagePrice;
            }
            set
            {
                SinglePagePrice = value;
            }
        }
    }
}