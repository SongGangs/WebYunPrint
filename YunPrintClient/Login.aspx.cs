using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace YunPrintClient
{
    public partial class Login : System.Web.UI.Page
    {
        private string url = "http://gw.api.taobao.com/router/rest?"; //阿里服务网址
        private string AppKey = "23281692";
        private string Security = "e767dfa2ce0a46ba554268dc720c7be6";
        private static int SecurityCode; //验证码
        private static bool IsHaveSend = false; //是否已发送验证码 不设置为static的话会
        private string phoneNumber;
        private CnlUserInfo user = new CnlUserInfo();
        private EnUserInfo EnUser = new EnUserInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
            HttpCookie cookies = Request.Cookies["User"];
            try
            {
                ///
                //summer
                //这里固定商家ID为1
                string sql = "select PhoneNumber,AddressName from UserInfo a join Address b on a.UserRecID=b.UserRecID where a.UserRecID=1";
                DataSet st = DataBase.RunDataSet(sql);
                DataTable s = st.Tables[0];
                this.RelPnone.Text = s.Rows[0]["PhoneNumber"].ToString();
                this.RelAdress.Text = s.Rows[0]["AddressName"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            if (!IsPostBack)
            {
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
                if (cookies != null )
                {
                    //如果Cookie不为空，则将Cookie里面的用户名和密码读取出来赋值给前台的文本框。
                    this.UserTextBox.Text = HttpUtility.UrlDecode(cookies["UserName"].Trim(), enc);
                    this.PasswarTextBox.Attributes.Add("value",
                        HttpUtility.UrlDecode(cookies["UserPassword"].ToString(), enc));
                    //这里依然把记住密码的选项给选中。
                    this.AutoLogin.Checked = true;
                }
            }
        }

        private CnlUserInfo users = new CnlUserInfo();
        //发送验证码
        private string SendSecurityCode(string SecurityCode)
        {
            phoneNumber = this.user_PhoneNumber.Value.ToString().Trim();
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

        //注册时确定按钮
        protected void OK_Btu_Click(object sender, EventArgs e)
        {
            phoneNumber = this.user_PhoneNumber.Value.ToString().Trim();
            if (String.IsNullOrEmpty(phoneNumber))
            {
                MessageTxt.Value = "请输入电话号码！";
                return;
            }
            if (String.IsNullOrEmpty(this.password.Value.ToString().Trim()))
            {
                MessageTxt.Value = "请输入密码！";
                return;
            }
            else
            {
                if (this.password.Value.ToString().Trim() != this.sure_password.Value.ToString().Trim())
                {
                    MessageTxt.Value = "两次输入密码不一致！";
                    return;
                }
            }
            if (MetarnetRegex.IsMobilePhone(phoneNumber))
            {
                if (IsHaveSend && (IdentifyCode.Value.ToString().Trim() == SecurityCode.ToString())) //判断验证码是否正确
                {
                    EnUserInfo Euser = new EnUserInfo();
                    Euser.uerName = this.UserNameText.Value.ToString().Trim();
                    Euser.phoneNumber = phoneNumber;
                    Euser.registerTime = DateTime.Now;
                    Euser.password = this.password.Value.ToString().Trim();

                    if (users.Register(Euser))
                    {
                        MessageTxt.Value = "注册成功！";
                        //还原
                        IsHaveSend = false;
                        SecurityCode = 0;
                        return;
                    }
                }
                else
                {
                    MessageTxt.Value = "验证码不正确！";
                    return;
                }
            }
            else
            {
                MessageTxt.Value = "手机号格式不正确！";
                return;
            }
        }

        //注册时发送验证码
        protected void Get_IdentifyCode_Click(object sender, EventArgs e)
        {
            phoneNumber = this.user_PhoneNumber.Value.ToString().Trim();
            DataSet ds = DataBase.RunDataSet("select * from UserInfo where PhoneNumber='" + phoneNumber + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('此手机号已注册！')", true);
                this.user_PhoneNumber.Focus();
            }
            else
            {
                Random rd = new Random();
                SecurityCode = rd.Next(1234, 9879); //生成验证码              
                SendSecurityCode(SecurityCode.ToString()); //发送验证码，后面还要根据返回的值判断是否发送成功
                IsHaveSend = true;
            }
        }

        //登录
        protected void LoginClick(object sender, EventArgs e)
        {
            DataTable UserTable = new DataTable();
            string NameOrNumber = UserTextBox.Text.Trim();
            string Password = PasswarTextBox.Text.Trim();
            if (String.IsNullOrEmpty(NameOrNumber))
            {
                MessageTxt.Value = "请输入用户名！";
                return;
            }
            try //电话号码登录
            {
                DataSet ds = DataBase.RunDataSet("select * from UserInfo where PhoneNumber='" + NameOrNumber + "'");
                Convert.ToInt64(NameOrNumber);
                if (NameOrNumber.Length == 11)
                {
                    UserTable = user.getUserByPhoneNumber(NameOrNumber);

                    if (UserTable.Rows.Count == 0)
                    {
                        MessageTxt.Value = "用户名不正确！";
                        return;
                    }

                    if (UserTable.Rows[0]["Password"].ToString() == Password) //Common.StringToMD5(Password)
                    {
                        int s = Convert.ToInt32(ds.Tables[0].Rows[0]["UserType"].ToString());
                        if (s == 1)
                        {
                            Session["UserTable"] = new DataTable();
                            Session["UserTable"] = UserTable;
                            HttpCookie cookie = new HttpCookie("User");
                            if (this.AutoLogin.Checked)
                            {
                                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
                                string LoginName = HttpUtility.UrlEncode(this.UserTextBox.Text.Trim(), enc);
                                string LoginPass = HttpUtility.UrlEncode(this.PasswarTextBox.Text.Trim(), enc);

                                ////所有的验证信息检测之后，如果用户选择的记住密码，则将用户名和密码写入Cookie里面保存起来。
                                cookie.Values.Add("UserName", LoginName);
                                cookie.Values.Add("UserPassword", LoginPass);
                                ////这里是设置Cookie的过期时间，这里设置一个星期的时间，过了一个星期之后状态保持自动清空。
                                cookie.Expires = System.DateTime.Now.AddDays(7.0);
                                HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                if (cookie != null)
                                {
                                    //如果用户没有选择记住密码，那么立即将Cookie里面的信息情况，并且设置状态保持立即过期。
                                    cookie.Values.Clear();
                                    cookie.Values.Remove("UserName");
                                    TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                                    cookie.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                                    Response.AppendCookie(cookie);
                                }
                            }
                            Response.Redirect("UserMain.aspx");
                        }
                        else
                        {
                            Session["UserTable"] = new DataTable();
                            Session["UserTable"] = UserTable;
                            HttpCookie cookie = new HttpCookie("User");
                            if (this.AutoLogin.Checked)
                            {
                                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
                                string LoginName = HttpUtility.UrlEncode(this.UserTextBox.Text.Trim(), enc);
                                string LoginPass = HttpUtility.UrlEncode(this.PasswarTextBox.Text.Trim(), enc);

                                ////所有的验证信息检测之后，如果用户选择的记住密码，则将用户名和密码写入Cookie里面保存起来。
                                cookie.Values.Add("UserName", LoginName);
                                cookie.Values.Add("UserPassword", LoginPass);
                                ////这里是设置Cookie的过期时间，这里设置一个星期的时间，过了一个星期之后状态保持自动清空。
                                cookie.Expires = System.DateTime.Now.AddDays(7.0);
                                HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                if (cookie != null)
                                {
                                    //如果用户没有选择记住密码，那么立即将Cookie里面的信息情况，并且设置状态保持立即过期。
                                    cookie.Values.Clear();
                                    cookie.Values.Remove("UserName");
                                    TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                                    cookie.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                                    Response.AppendCookie(cookie);
                                }
                            }
                            Response.Redirect("BossWeb.aspx");
                        }
                    }
                    else
                        MessageTxt.Value = "密码不正确，请重新输入！";
                }
            }
            catch (Exception ex) //用户名登陆
            {
                DataSet ds = DataBase.RunDataSet("select * from UserInfo where UserName='" + NameOrNumber + "'");
                UserTable = user.getUserByUserName(NameOrNumber);
                if (UserTable.Rows.Count == 0)
                {
                    MessageTxt.Value = "用户名不正确！";
                    this.UserTextBox.Text = "";
                    return;
                }
                else
                  {
                    if (UserTable.Rows[0]["Password"].ToString() == Password) //Common.StringToMD5(Password)
                    {
                        int s = Convert.ToInt32(ds.Tables[0].Rows[0]["UserType"].ToString());
                        if (s == 1)
                        {
                            Session["UserTable"] = new DataTable();
                            Session["UserTable"] = UserTable;
                            HttpCookie cookie = new HttpCookie("User");
                            if (this.AutoLogin.Checked)
                            {
                                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
                                string LoginName = HttpUtility.UrlEncode(this.UserTextBox.Text.Trim(), enc);
                                string LoginPass = HttpUtility.UrlEncode(this.PasswarTextBox.Text.Trim(), enc);

                                ////所有的验证信息检测之后，如果用户选择的记住密码，则将用户名和密码写入Cookie里面保存起来。
                                cookie.Values.Add("UserName", LoginName);
                                cookie.Values.Add("UserPassword", LoginPass);
                                ////这里是设置Cookie的过期时间，这里设置一个星期的时间，过了一个星期之后状态保持自动清空。
                                cookie.Expires = System.DateTime.Now.AddDays(7.0);
                                HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                if (cookie != null)
                                {
                                    //如果用户没有选择记住密码，那么立即将Cookie里面的信息情况，并且设置状态保持立即过期。
                                    cookie.Values.Clear();
                                    cookie.Values.Remove("UserName");
                                    TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                                    cookie.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                                    Response.AppendCookie(cookie);
                                }
                            }
                            Response.Redirect("UserMain.aspx");
                        }
                        else
                        {
                            Session["UserTable"] = new DataTable();
                            Session["UserTable"] = UserTable;
                            HttpCookie cookie = new HttpCookie("User");
                            if (this.AutoLogin.Checked)
                            {
                                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
                                string LoginName = HttpUtility.UrlEncode(this.UserTextBox.Text.Trim(), enc);
                                string LoginPass = HttpUtility.UrlEncode(this.PasswarTextBox.Text.Trim(), enc);

                                ////所有的验证信息检测之后，如果用户选择的记住密码，则将用户名和密码写入Cookie里面保存起来。
                                cookie.Values.Add("UserName", LoginName);
                                cookie.Values.Add("UserPassword", LoginPass);
                                ////这里是设置Cookie的过期时间，这里设置一个星期的时间，过了一个星期之后状态保持自动清空。
                                cookie.Expires = System.DateTime.Now.AddDays(7.0);
                                HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                if (cookie != null)
                                {
                                    //如果用户没有选择记住密码，那么立即将Cookie里面的信息情况，并且设置状态保持立即过期。
                                    cookie.Values.Clear();
                                    cookie.Values.Remove("UserName");
                                    TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                                    cookie.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                                    Response.AppendCookie(cookie);
                                }
                            }
                            Response.Redirect("BossWeb.aspx");
                        }
                    }
                    else
                        MessageTxt.Value = "密码不正确，请重新输入！";
                }
            }
        }

        //忘记密码时确定按钮
        protected void OK_Click(object sender, EventArgs e)
        {
            phoneNumber = this.UserTel.Value.ToString().Trim();
            if (String.IsNullOrEmpty(phoneNumber))
            {
                MessageTxt.Value = "请输入电话号码！";
                return;
            }
            if (String.IsNullOrEmpty(this.UserNewpassword.Value.ToString().Trim()))
            {
                MessageTxt.Value = "请输入密码！";
                return;
            }
            else
            {
                if (this.Sure_Userpassword.Value.ToString().Trim() != this.UserNewpassword.Value.ToString().Trim())
                {
                    MessageTxt.Value = "两次输入密码不一致！";
                    return;
                }
            }
            if (MetarnetRegex.IsMobilePhone(phoneNumber))
            {
                if (IsHaveSend && (IdentCode.Value.ToString().Trim() == SecurityCode.ToString())) //判断验证码是否正确
                {
                    string pass = this.UserNewpassword.Value.ToString().Trim();
                    DataSet ds =
                        DataBase.RunDataSet("select * from UserInfo where  PhoneNumber='" + phoneNumber + "'");
                    DataTable dt = ds.Tables[0];
                    dt.Rows[0]["Password"] = pass;
                    int k = DataBase.update("UserInfo", "UserRecID", dt);
                    if (k > 0)
                    {
                        MessageTxt.Value = "密码修改成功！";
                        //还原
                        IsHaveSend = false;
                        SecurityCode = 0;
                        return;
                    }
                }
                else
                {
                    MessageTxt.Value = "验证码不正确！";
                    return;
                }
            }
            else
            {
                MessageTxt.Value = "手机号格式不正确！";
                return;
            }
        }

        //忘记密码时发送验证码
        protected void GetIdentCode_Click(object sender, EventArgs e)
        {
            phoneNumber = this.UserTel.Value.ToString().Trim();
            DataSet ds = DataBase.RunDataSet("select * from UserInfo where PhoneNumber='" + phoneNumber + "'");
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Random rd = new Random();
                    SecurityCode = rd.Next(1234, 9879); //生成验证码              
                    SendSecurityCode(SecurityCode.ToString()); //发送验证码，后面还要根据返回的值判断是否发送成功
                    IsHaveSend = true;
                }
            }
            catch (Exception)
            {
                MessageTxt.Value = "此手机号未注册，请先注册！";
            }
        }

        protected void UserTextBox_TextChanged(object sender, EventArgs e)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
            this.PasswarTextBox.Attributes.Add("value",
                        HttpUtility.UrlDecode("", enc));
            AutoLogin.Checked = false;
            this.PasswarTextBox.Focus();
        }

    }
}