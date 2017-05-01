using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace YunPrintClient
{
    public class CnlUserInfo
    {
        public DataTable getUserByUserName(string username)
        {
            string sql = "select * from UserInfo where UserName='" + username+"'";
            DataSet ds = DataBase.RunDataSet(sql);
            return ds.Tables[0];
        }

        public DataTable getUserByPhoneNumber(string PhoneNumber)
        {
            string sql = "select * from UserInfo where PhoneNumber=" + PhoneNumber;
            DataSet ds = DataBase.RunDataSet(sql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取表
        /// </summary>
        /// <returns></returns>
        public DataTable getDatable(EnUserInfo obj)
        {
            string sql = "select * from UserInfo where 0=1";
            DataSet ds = DataBase.RunDataSet(sql);
            DataTable user = ds.Tables[0];
            DataRow dr = user.NewRow();
            dr["UserRecID"] = 0;
            dr["UserName"] = obj.uerName;
            dr["Password"] = obj.password;
            dr["RegisterTime"] = obj.registerTime;
            dr["PhoneNumber"] = obj.phoneNumber;
            dr["UserType"] = 1;
            user.Rows.Add(dr);
            return user;          
        }
        
        /// <summary>
        /// 注册 1，成功；2，用户名已存在；3，电话号码已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool  Register(EnUserInfo obj)
        {
            DataTable user=getDatable(obj);

            int key;
            try 
            {
                key = DataBase.update("UserInfo", "UserRecID", user);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }       
        }

    }
}

