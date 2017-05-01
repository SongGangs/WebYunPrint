using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace YunPrintClient.AppCode
{
    public class CnOrders
    {
        public DataTable getOrderByNumber(string OrderNumber)
        {
            string sql = "select * from Orders where OrderNumber=" + OrderNumber;
            DataSet ds = DataBase.RunDataSet(sql);
            return ds.Tables[0];
        }

        public DataTable getOrderTabel(EnOrder obj)
        {
            DataTable dt = getOrderByNumber(obj.orderNumber);
            dt.Rows[0]["ToalPrice"] = obj.toalPrice;
            dt.Rows[0]["OrderStatusID"] = obj.orderStatusID;
            dt.Rows[0]["PlaceOrderTime"] = obj.placeOrderTime;
           // dt.Rows[0]["UserRecID"] = obj.userRecID;
            return dt;
        }
        public DataTable getDataTable(string Sql)
        {
            DataSet mySet = DataBase.RunDataSet(Sql);
            DataTable myTable = mySet.Tables[0];
            return myTable;
        }
    }
}