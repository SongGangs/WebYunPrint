using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnUserInfo
    {
        private long UserRecID;
        private string UserName;
        private string Password;
        private string Email;
        private string PhoneNumber;
        private string Address;
        private int UserType;  //1 ：客户 2： 商家 
        private DateTime RegisterTime;
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

        public string uerName
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
        public string password
        {
            get
            {
                return Password;
            }
            set
            {
                Password = value;
            }
        }
        public string email
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }
        public string phoneNumber
        {
            get
            {
                return PhoneNumber;
            }
            set
            {
                PhoneNumber = value;
            }
        }
        public DateTime registerTime
        {
            get
            {
                return RegisterTime;
            }
            set
            {
                RegisterTime = value;
            }
        }

        public EnUserInfo(DataRow user)
        {
            this.UserRecID = (user["UserRecID"] is DBNull) ? 0 :Convert.ToInt64(user["UserRecID"]);
            this.UserName = (user["UserName"] is DBNull) ? "" : user["UserName"].ToString();
            //应该继续加
        }

        public EnUserInfo()
        {
            
        }
    }
}
