using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnOrder
    {
        private string OrderNumber;
        private long UserRecID;
        private long OrderStatusID;
        private long AddressRecID;
        private long OrderTypeID;
        private DateTime PlaceOrderTime;
        private string Coment;
        private decimal  ToalPrice;
        //private long PrintTypeID;
        //private int TotalPages;
        private List<EnDoc> Docs; 
        public string orderNumber
        {
            get
            {
                return OrderNumber;
            }
            set
            {
                OrderNumber = value;
            }
        }
      public long userRecID
        {
            get
            {
                return UserRecID;
            }
            set
            {
               UserRecID= value;
            }
        }
  //public long printTypeID
  //      {
  //          get
  //          {
  //              return PrintTypeID;
  //          }
  //          set
  //          {
  //              PrintTypeID = value;
  //          }
  //      }
    
  public long orderStatusID
        {
            get
            {
                return OrderStatusID;
            }
            set
            {
                OrderStatusID= value;
            }
        }
  public long addressRecID
        {
            get
            {
                return AddressRecID;
            }
            set
            {
                AddressRecID = value;
            }
        }
  public long orderTypeID
        {
            get
            {
                return OrderTypeID;
            }
            set
            {
                OrderTypeID = value;
            }
        }
  public DateTime placeOrderTime
        {
            get
            {
                return PlaceOrderTime;
            }
            set
            {
                PlaceOrderTime = value;
            }
        }
  //public int totalPages
  //      {
  //          get
  //          {
  //              return TotalPages;
  //          }
  //          set
  //          {
  //              TotalPages = value;
  //          }
  //      }
  public string coment
        {
            get
            {
                return Coment;
            }
            set
            {
                Coment = value;
            }
        }
  public decimal toalPrice
        {
            get
            {
                return ToalPrice;
            }
            set
            {
                ToalPrice = value;
            }
        }

        public List<EnDoc> docs
        {
            get { return Docs; }
            set { Docs = value; }
        }
  
    }

}