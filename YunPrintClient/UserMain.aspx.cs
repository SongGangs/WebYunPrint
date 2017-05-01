using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace YunPrintClient
{
    public partial class UserMain : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt == null)
                    return;
                this.UserName.Text = dt.Rows[0]["UserName"].ToString();
                this.userName1.Text = dt.Rows[0]["UserName"].ToString();
                this.College.Text = dt.Rows[0]["College"].ToString();
                this.Major.Text = dt.Rows[0]["Major"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Sex"].ToString()))
                {
                    if (dt.Rows[0]["Sex"].ToString().Equals("0"))
                        this.SexTxt.Text = "男";
                    else
                        this.SexTxt.Text = "女";
                }
                else
                {
                    this.SexTxt.Text = null;
                }
            }
            else
            {
                return;
            }
            this.BWPrint.Text = GetMoney(1).Remove(4);
            this.ColorPrint.Text = GetMoney(2).Remove(4);
            this.BusiAdress.Text = GetAdressTelNumBer(2).Rows[0]["AddressName"].ToString();
            this.BusiTelNumber.Text = GetAdressTelNumBer(2).Rows[0]["PhoneNumber"].ToString();
        }

        private DataTable GetAdressTelNumBer(int k)
        {
            string sql = "select AddressName,PhoneNumber from UserInfo a join Address b on a.UserRecID=b.UserRecID where UserType=" + k;
            DataSet st = DataBase.RunDataSet(sql);
            DataTable s = st.Tables[0];
            return s;
        }

        private string GetMoney(int k)
        {
            string sql = "select SinglePagePrice from PrintType where PrintTypeID=" + k;
            DataSet st = DataBase.RunDataSet(sql);
            DataTable s = st.Tables[0];
            string sa = s.Rows[0]["SinglePagePrice"].ToString();
            return sa;
        }

        protected void UserCenter_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCenter.aspx");
        }

        protected void Esc_Button_Click(object sender, EventArgs e)
        {

        }

        protected void Unnamed6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }
    }
}