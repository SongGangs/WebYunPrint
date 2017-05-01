using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnAddress
    {
        private long AddressRecID;
        private long UserRecID;
        private string AddressName;
        private int IsUsuallyAddress;
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
        public long userRecID
        {
            get
            {
                return UserRecID;
            }
            set
            {
                UserRecID = value;
            }
        }
        public string addressName
        {
            get
            {
                return AddressName;
            }
            set
            {
                AddressName = value;
            }
        }
        public int isUsuallyAddress
        {
            get
            {
                return IsUsuallyAddress;
            }
            set
            {
                IsUsuallyAddress = value;
            }
        }


    }
}