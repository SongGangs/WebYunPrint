using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnOrderType
    {
        //1，上门自提
        //2，送货上门
        //3，加急送
        private long OrderTypeID;
        private string OrderTypeName;
      public long orderTypeID
        {
            get
            {
                return OrderTypeID;
            }
            set
            {
               OrderTypeID= value;
            }
        }
      public string orderTypeName
        {
            get
            {
                return OrderTypeName;
            }
            set
            {
                OrderTypeName = value;
            }
        }
    }
}