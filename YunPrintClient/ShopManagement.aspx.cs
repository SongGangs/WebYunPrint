using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YunPrintClient
{
    public partial class ShopManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if(dt==null)
                    return;
                BWPrice.Text =  GetPrintMoney("黑白打印").Remove(4);
                ColorPrice.Text = GetPrintMoney("彩色打印").Remove(4);
                SendModePrice0.Text = GetSendMoney("送货上门").Remove(4);
                SendModePrice1.Text = GetSendMoney("加急送（需配送费）").Remove(4);
                SendModePrice2.Text = GetSendMoney("上门自提").Remove(4);
                DataSet ADD = GetNumberOrAddress(dt.Rows[0]["UserName"].ToString());
                PhoneNumberTXT.Text = ADD.Tables[0].Rows[0]["PhoneNumber"].ToString();
                AddressTXT.Text = ADD.Tables[0].Rows[0]["AddressName"].ToString();
            }
        }

        //获取打印类型价格！
        private string GetPrintMoney(string PrintName)
        {
            string sql = "select SinglePagePrice from PrintType where PrintName='" + PrintName + "'";
            DataSet st = DataBase.RunDataSet(sql);
            DataTable s = st.Tables[0];
            string sa = s.Rows[0]["SinglePagePrice"].ToString();
            return sa;
        }

        //获取配送方式价格！
        private string GetSendMoney(string SendName)
        {
            string sql = "select SendPrice from OrderType where OrderTypeName='" + SendName + "'";
            DataSet st = DataBase.RunDataSet(sql);
            DataTable s = st.Tables[0];
            string sa = s.Rows[0]["SendPrice"].ToString();
            return sa;
        }

        //获取电话和地址！
        private DataSet GetNumberOrAddress(string username)
        {
            string sql = "select a.PhoneNumber,b.AddressName from UserInfo a join Address b on a.UserRecID=b.UserRecID where a.UserName='" +
                         username + "'";
            DataSet ds = DataBase.RunDataSet(sql);
            return ds;
        }

        protected void SaveBtu_Click(object sender, EventArgs e)
        {
            int count, k, j, m, n;
            //修改打印类型每页单价
            string Sql = "select * from PrintType ";
            DataSet ST = DataBase.RunDataSet(Sql);
            DataTable S = ST.Tables[0];
            S.Rows[0]["SinglePagePrice"] = this.BWPrice.Text.Trim();
            S.Rows[1]["SinglePagePrice"] = this.ColorPrice.Text.Trim();
            try
            {
                k = DataBase.update("PrintType", "PrintTypeID", S);
            }
            catch (Exception)
            {

                throw;
            }
            //修改配送方式价格
            string mySql = "select * from OrderType";
            DataSet mySet = DataBase.RunDataSet(mySql);
            DataTable myTable = mySet.Tables[0];
            myTable.Rows[0]["SendPrice"] = this.SendModePrice0.Text.Trim();
            myTable.Rows[1]["SendPrice"] = this.SendModePrice1.Text.Trim();
            myTable.Rows[2]["SendPrice"] = this.SendModePrice2.Text.Trim();
            try
            {
                j = DataBase.update("OrderType", "OrderTypeID", myTable);
            }
            catch (Exception)
            {

                throw;
            }
            //修改电话号码、地址
            DataTable dt = Session["UserTable"] as DataTable;
            dt.Rows[0]["PhoneNumber"] = this.PhoneNumberTXT.Text.Trim();
            try
            {
                m = DataBase.update("UserInfo", "UserRecID", dt);
            }
            catch (Exception)
            {
                throw;
            }
            string SqlConnection = "select * from Address where UserRecID=" +
                                   dt.Rows[0]["UserRecID"].ToString();
            DataSet myDataSet = DataBase.RunDataSet(SqlConnection);
            DataTable myDataTable = myDataSet.Tables[0];
            myDataTable.Rows[0]["AddressName"] = this.AddressTXT.Text.Trim();
            try
            {
                n = DataBase.update("Address", "AddressRecID", myDataTable);
            }
            catch (Exception)
            {
                throw;
            }
            count = k + j + m + n;
            if (count > 0)
            {
                this.Response.Write(" <script language=javascript>alert('修改成功！'); window.location.href= 'ShopManagement.aspx'</script> ");
            }
            else
            {
                MessageTxt.Value = "修改失败！";
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }

    }
}