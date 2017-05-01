using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace YunPrintClient
{
    public partial class UserCenter : System.Web.UI.Page
    {
        string url = "http://gw.api.taobao.com/router/rest?";//阿里服务网址
        string AppKey = "23281692";
        string Security = "e767dfa2ce0a46ba554268dc720c7be6";
        static int SecurityCode;//验证码
        static bool IsHaveSend = false;//是否已发送验证码 不设置为static的话会
        string phoneNumber;
        private EnUserInfo User = new EnUserInfo();
        private CnlUserInfo Us = new CnlUserInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt == null)
                    return;
                this.UserName.Text = dt.Rows[0]["UserName"].ToString();
                this.userName1.Text = this.UserName.Text;
                this.UserTelNumber.Text = dt.Rows[0]["PhoneNumber"].ToString();
                this.College.Text = dt.Rows[0]["College"].ToString();
                this.Major.Text = dt.Rows[0]["Major"].ToString();
                this.UserCollge.Text = dt.Rows[0]["College"].ToString();
                this.UserMajor.Text = dt.Rows[0]["Major"].ToString();
                this.UserQQMail.Text = dt.Rows[0]["Email"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Sex"].ToString()))
                {
                    this.Sex.SelectedIndex = Convert.ToInt16(dt.Rows[0]["Sex"].ToString());
                    if (dt.Rows[0]["Sex"].ToString().Equals("0"))
                        this.SexTxt.Text = "男";
                    else
                        this.SexTxt.Text = "女";
                }
                else
                {
                    this.SexTxt.Text = null;
                }

                string sql = "select AddressName from Address where UserRecID='" + dt.Rows[0]["UserRecID"].ToString() + "'";
                DataSet st = DataBase.RunDataSet(sql);
                DataTable s = st.Tables[0];
                if (s.Rows.Count > 0)
                {
                    Repeater1.DataSource = s;
                    Repeater1.DataBind();
                }

            }
            else
            {
                return;
            }

        }

        protected void Save_Button_Click(object sender, EventArgs e)
        {
            phoneNumber = this.UserTelNumber.Text.Trim();
            DataTable dt = Session["UserTable"] as DataTable;
            dt.Rows[0]["College"] = this.UserCollge.Text.ToString();
            dt.Rows[0]["Major"] = this.UserMajor.Text.ToString();
            dt.Rows[0]["Email"] = this.UserQQMail.Text.ToString();
            try
            {
                dt.Rows[0]["Sex"] = this.Sex.SelectedItem.Value;
            }
            catch (Exception)
            {
            }


            try
            {
                int k = DataBase.update("UserInfo", "UserRecID", dt);
            }
            catch (Exception)
            {
            }

            if (IsHaveSend)
            {
                if (IdentifyCode.Text.Trim() == SecurityCode.ToString()) //判断验证码是否正确
                {
                    dt.Rows[0]["PhoneNumber"] = this.UserTelNumber.Text.Trim();
                    try
                    {
                        int k = DataBase.update("UserInfo", "UserRecID", dt);
                        //还原
                        IsHaveSend = false;
                        SecurityCode = 0;
                        return;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    MessageTxt.Value = "验证码不正确，手机号未修改成功！";//这里manuInput.Text就是要弹出的信息
                    this.IdentifyCode.Text = "";
                    return;
                }
            }
            MessageTxt.Value = "修改成功！";
            this.IdentifyCode.Text = "";
        }
        //增加、删除地址
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataTable dt = Session["UserTable"] as DataTable;
            int i = Convert.ToInt32(dt.Rows[0]["UserRecID"]);
            string AdressNum = "select AddressName from Address where UserRecID=" + i;
            DataSet AdressN = DataBase.RunDataSet(AdressNum);
            if (e.CommandName.Equals("del"))
            {
                if (AdressN.Tables[0].Rows.Count > 1)
                {
                    string mycon = "delete from Address where AddressName='" + e.CommandArgument + "'";
                    int k = DataBase.ExecuteNonQuery(mycon);
                    if (k > 0)
                    {
                        MessageTxt.Value = "删除成功！";
                    }
                    Response.Redirect("UserCenter.aspx");
                }
                else
                {
                    MessageTxt.Value = "必须要有一个地址！删除失败！";
                }
            }
            if (e.CommandName == "insert")
            {
                TextBox tbx = (TextBox)e.Item.FindControl("UserAdressAdd");
                if (tbx.Text != "")
                {
                    if (AdressN.Tables[0].Rows.Count < 3)
                    {
                        for (int k = 0; k < AdressN.Tables[0].Rows.Count; k++)
                        {
                            if (tbx.Text != AdressN.Tables[0].Rows[k]["AddressName"].ToString())
                            {
                                string sql = "select * from Address where 0=1";
                                DataSet AdressForm = DataBase.RunDataSet(sql);
                                DataTable AdresTable = new DataTable();
                                AdresTable = AdressForm.Tables[0];
                                DataRow dr = AdresTable.NewRow();
                                dr["AddressRecID"] = 0;
                                dr["UserRecID"] = i;
                                dr["AddressName"] = tbx.Text;
                                dr["IsUsuallyAddress"] = 1;
                                AdresTable.Rows.Add(dr);
                                int p = DataBase.update("Address", "AddressRecID", AdresTable);
                                if (p > 0)
                                {
                                    MessageTxt.Value = "增添成功！";
                                    tbx.Text = "";
                                }
                                Response.Redirect("UserCenter.aspx");
                            }
                            else
                            {
                                MessageTxt.Value = "此地址已存在！请重新输入！";
                                tbx.Text = "";
                            }
                        }
                    }
                    else
                    {
                        MessageTxt.Value = "地址不能超过三个！添加失败！";
                        tbx.Text = "";
                    }
                }
                else
                {
                    MessageTxt.Value = "不能增添地址，请重新输入地址！";
                }
            }
        }


        //发送验证码
        public string SendSecurityCode(string SecurityCode)
        {
            phoneNumber = this.UserTelNumber.Text.Trim();
            string result = "";
            if (MetarnetRegex.IsMobilePhone(phoneNumber))
            {
                ITopClient client = new DefaultTopClient(url, AppKey, Security);

                AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();

                req.Extend = "123456";

                req.SmsType = "normal";

                req.SmsFreeSignName = "注册验证";
                //    req.SmsParam = String.Format("{\"code\":\"1234\",\"product\":\"打堆云打印\"}", SecurityCode);// "{\"code\":\"1234\",\"product\":\"打堆云打印\"}";
                req.SmsParam = "{\"code\":\"" + SecurityCode + "\",\"product\":\"打堆云打印\"}";
                req.RecNum = phoneNumber;
                req.SmsTemplateCode = "SMS_3125049";
                AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
                result = rsp.Body;
            }
            return result;

        }
        protected void Get_IdentifyCode_Click(object sender, EventArgs e)
        {
            phoneNumber = this.UserTelNumber.Text.Trim();
            DataSet ds = DataBase.RunDataSet("select * from UserInfo where PhoneNumber='" + phoneNumber + "' and UserName='" + UserName.Text.Trim() + "'");
            if (ds.Tables[0].Rows.Count > 0 && MetarnetRegex.IsMobilePhone(phoneNumber))
            {
                MessageTxt.Value = "请检查你的手机号是否已修改！";
            }
            else
            {
                Random rd = new Random();
                SecurityCode = rd.Next(1234, 9879);//生成验证码              
                SendSecurityCode(SecurityCode.ToString());//发送验证码，后面还要根据返回的值判断是否发送成功
                IsHaveSend = true;
            }

        }

        protected void Cancel_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMain.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }
    }
}

