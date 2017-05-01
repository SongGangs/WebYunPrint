using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YunPrintClient
{
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt==null)
                {
                    return;
                }
                UserName.Text = dt.Rows[0]["UserName"].ToString();
                userName1.Text = UserName.Text;
                College.Text = dt.Rows[0]["College"].ToString();
                Major.Text = dt.Rows[0]["Major"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Sex"].ToString()))
                {
                    if (dt.Rows[0]["Sex"].ToString().Equals("0"))
                        this.SexTxt.Text = "男";
                    else
                        this.SexTxt.Text = "女";
                }
                else
                {
                    this.SexTxt.Text = "";
                }
            }
        }

        protected void Save_Button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NewPass.Text))
            {
                if (NewPass.Text.Trim() == SurePass.Text.Trim())
                {
                    DataTable dt=Session["UserTable"] as DataTable;
                    dt.Rows[0]["PassWord"] = NewPass.Text.Trim();
                    try
                    {
                        int k=DataBase.update("UserInfo", "UserRecID", dt);
                        MessageTxt.Value = "密码修改成功！";
                    }
                    catch (Exception)
                    {
                        MessageTxt.Value = "密码修改失败，请重试！";
                    }
                }
                else
                {
                    MessageTxt.Value = "新密码与确认密码不符合，请重新输入！";
                }
            }
            else
            {
                MessageTxt.Value = "新密码不能为空！";
            }
        }

        protected void Cancel_Button_Click(object sender, EventArgs e)
        {

        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }
    }
} 