using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnOrderStaus
    {
        private long OrderStatusID;
        private string OrderName;
        public long orderStatusID
        {
            get
            {
                return OrderStatusID;
            }
            set
            {
                OrderStatusID = value;
            }
        }
        public string orderName
        {
            get
            {
                return OrderName;
            }
            set
            {
                OrderName = value;
            }
        }
    }
}