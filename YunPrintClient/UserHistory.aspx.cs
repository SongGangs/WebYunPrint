using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YunPrintClient
{
    public partial class UserHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt == null)
                    return;
                string SqlOrder = "select OrderNumber,UserName,PlaceOrderTime,PhoneNumber,AddressName,Coment,ToalPrice from Orders a join UserInfo b on a.UserRecID=b.UserRecID join Address c on a.AddressRecID=c.AddressRecID where a.UserRecID=" + dt.Rows[0]["UserRecID"].ToString();
                DataSet st = DataBase.RunDataSet(SqlOrder);
                DataTable s = st.Tables[0];
                DataList1.DataSource = s;
                DataList1.DataBind();
                this.UserName.Text = dt.Rows[0]["UserName"].ToString();
                this.userName1.Text = this.UserName.Text;
                this.College.Text = dt.Rows[0]["College"].ToString();
                this.Major.Text = dt.Rows[0]["Major"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Sex"].ToString()) && dt.Rows[0]["Sex"].ToString().Equals("0"))
                    this.SexTxt.Text = "男";
                else
                {
                    this.SexTxt.Text = "女";
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lab = e.Item.FindControl("OrderNumber") as Label;
                string mySql = "select DocName,PrintName from Orders a join Doc b on a.OrderNumber=b.OrderNumber join PrintType c on b.PrintTypeID=c.PrintTypeID where a.OrderNumber='" + lab.Text + "'";
                DataSet ST = DataBase.RunDataSet(mySql);
                DataList DataList2 = e.Item.FindControl("DataList2") as DataList;
                DataList2.DataSource = ST.Tables[0].DefaultView;
                DataList2.DataBind();
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }
    }
}