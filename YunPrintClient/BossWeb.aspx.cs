using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace YunPrintClient
{
    public partial class BossWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeTxt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string AddTime = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            if (!IsPostBack)
            {
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt.Rows[0]["UserType"].ToString() == "3")
                {
                    this.shop.Visible = false;
                    this.num.Visible = false;
                }
                SelectInfo(TimeTxt.Text, AddTime);
                string Sql = "select UserName,Password,PhoneNumber from UserInfo where UserType=3";
                DataSet ST = DataBase.RunDataSet(Sql);
                DataTable S = ST.Tables[0];
                UserList.DataSource = S;
                UserList.DataBind();
                Timer Timer1 = new Timer();
                Timer1.Enabled = true; //启动计时器
            }
        }

        private void SelectInfo(string StartTime,string EndTime)
        {
            string SqlOrder = "select OrderNumber,OrderTypeID,UserName,PlaceOrderTime,PhoneNumber,AddressName,Coment,ToalPrice from Orders a join UserInfo b on a.UserRecID=b.UserRecID join Address c on a.AddressRecID=c.AddressRecID where PlaceOrderTime between'" + StartTime + "' and '" + EndTime + "' order by('OrderNumber') desc";
            DataSet st = DataBase.RunDataSet(SqlOrder);
            DataTable s = st.Tables[0];
            if (s.Rows.Count > 0)
            {
                DataList1.DataSource = s;
                DataList1.DataBind();
            }
            else
            {
                DataList1.DataBind();
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                Label lab = e.Item.FindControl("OrderNumber") as Label;
                //string mySql = "select DocName,PrintName from Orders a join PrintType b on a.PrintTypeID=b.PrintTypeID join Doc c on a.OrderNumber=c.OrderNumber where a.OrderNumber='" + lab.Text + "'";
                string mySql = "select DocName,PrintName from Orders a join Doc b on a.OrderNumber=b.OrderNumber join  PrintType c on b.PrintTypeID=c.PrintTypeID where a.OrderNumber='" + lab.Text + "'";
                
                DataSet ST = DataBase.RunDataSet(mySql);
                DataList DataList2 = e.Item.FindControl("DataList2") as DataList;
                DataList2.DataSource = ST.Tables[0].DefaultView;
                DataList2.DataBind();
            }
        }
        //定时执行
        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            TimeTxt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string AddTime = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            SelectInfo(TimeTxt.Text, AddTime);
        }
        //返回当天
        protected void Back_LinkBtn_Click(object sender, EventArgs e)
        {
            string StartTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            string AddTime = System.DateTime.Now.AddDays(1).ToString();
            SelectInfo(StartTime, AddTime);
            Timer1.Enabled = true;
        }

        protected void Select_ServerClick(object sender, EventArgs e)
        {
            this.Timer1.Enabled = false;
        }
        //搜索时间段
        protected void SelectMores_Click(object sender, EventArgs e)
        {
            this.Timer1.Enabled = false;
            string Start = this.StartTime.Text;
            string End = this.EndTime.Text;
            if (Start != "" && End != "")
            {
                if (DateTime.Parse(Start) <= DateTime.Parse(End))
                {
                    SelectInfo(Start, End); 
                } 
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('起始时间要小于结束时间！')", true);
                    DataList1.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('请选择时间段！')", true);
                DataList1.DataBind();
            }
        }

        protected void SelectOnes_Click(object sender, EventArgs e)
        {
            this.Timer1.Enabled = false;
            string Start = this.SelectOneDay.Text;
            string End = null;
            if (Start!="")
            {
                string AddTime = this.Calendar1.SelectedDate.AddDays(1).ToString();
                SelectInfo(Start, AddTime);  
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('请选择日期！')", true);
                DataList1.DataBind();
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if ( this.list.Items[list.SelectedIndex].Value=="OneDay")
            {
                this.SelectOneDay.Text = this.Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                this.StartTime.Text = this.Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            }
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.EndTime.Text = this.Calendar2.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void AddStaff_But_OnClick(object sender, EventArgs e)
        {
            this.Timer1.Enabled = false;
            if (!string.IsNullOrEmpty(this.StaffName.Text) && !string.IsNullOrEmpty(this.StaffPass.Text) && !string.IsNullOrEmpty(this.StaffTel.Text))
            {
                if (this.StaffPass.Text.Trim() == this.SureStaffPass.Text.Trim())
                {
                    string mySql = "select PhoneNumber from UserInfo where PhoneNumber='" + this.StaffTel.Text.Trim() + "'";
                    DataSet Sr = DataBase.RunDataSet(mySql);
                    DataTable s = Sr.Tables[0];
                    if (s.Rows.Count > 0)
                    {
                        MessageTxt.Value = "此号码已存在！";
                        this.StaffTel.Text = "";
                    }
                    else
                    {
                        string sqlconnection = "select * from UserInfo where 0=1";
                        DataSet mySet = DataBase.RunDataSet(sqlconnection);
                        DataTable myTable = mySet.Tables[0];
                        DataRow myRow = myTable.NewRow();
                        myRow["UserName"] = this.StaffName.Text.Trim();
                        try
                        {
                            myRow["Sex"] = this.Sex.SelectedItem.Value;
                        }
                        catch (Exception)
                        { }
                        myRow["RegisterTime"] = System.DateTime.Now;
                        myRow["PhoneNumber"] = this.StaffTel.Text.Trim();
                        myRow["UserRecID"] = 0;
                        myRow["Password"] = this.StaffPass.Text.Trim();
                        myRow["UserType"] = 3;
                        myTable.Rows.Add(myRow);
                        try
                        {
                            DataBase.update("UserInfo", "UserRecID", myTable);
                            this.Page.RegisterStartupScript("", "<script>alert('员工账号添加成功！');window.location.href= 'BossWeb.aspx'</script>");
                            this.StaffName.Text = "";
                            this.StaffTel.Text = "";
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    MessageTxt.Value = "密码与确认密码不符合，请重新输入！";
                }
            }
            else
            {
                MessageTxt.Value = "用户名或电话号码或密码不能为空！";
            }
        }

        protected void Cancel_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BossWeb.aspx");
        }

        protected void UserList_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            this.Timer1.Enabled = false;
            string ID = this.UserList.DataKeys[e.Item.ItemIndex].ToString();
            string mySql = "select UserRecID,UserName,Password,PhoneNumber from UserInfo where PhoneNumber='" + ID + "'";
            DataSet MySet = DataBase.RunDataSet(mySql);
            DataTable MyTable = MySet.Tables[0];
            try
            {
                MyTable.Rows[0]["UserName"] = ((TextBox)e.Item.FindControl("userText")).Text.Trim();
                MyTable.Rows[0]["Password"] = ((TextBox)e.Item.FindControl("pwdText")).Text.Trim();
                MyTable.Rows[0]["PhoneNumber"] = ((TextBox)e.Item.FindControl("phoneNumText")).Text.Trim();
                DataBase.update("UserInfo", "UserRecID", MyTable);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('更新成功！');window.location.href= 'BossWeb.aspx'", true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void UserList_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            string id = this.UserList.DataKeys[e.Item.ItemIndex].ToString();
            try
            {
                string mycon = "delete from UserInfo where PhoneNumber='" + id + "'";
                int k = DataBase.ExecuteNonQuery(mycon);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('删除成功！');window.location.href= 'BossWeb.aspx'", true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void UserList_EditCommand(object source, DataListCommandEventArgs e)
        {
            this.Timer1.Enabled = false;
            this.UserList.EditItemIndex = e.Item.ItemIndex;
            string ID = this.UserList.DataKeys[e.Item.ItemIndex].ToString();
            string mySql = "select UserName,Password,PhoneNumber from UserInfo where PhoneNumber='" + ID + "'";
            DataSet MySet = DataBase.RunDataSet(mySql);
            DataTable MyTable = MySet.Tables[0];
            UserList.DataSource = MyTable;
            UserList.DataBind();
        }

        protected void UserList_CancelCommand(object source, DataListCommandEventArgs e)
        {
            this.UserList.EditItemIndex = -1;
            Response.Redirect("BossWeb.aspx");
        }

        protected void ModifyPass_But_Click(object sender, EventArgs e)
        {
            this.Timer1.Enabled = false;
            if (!string.IsNullOrEmpty(this.BusiNewPass.Text))
            {
                if (this.BusiNewPass.Text.Trim() == this.SurePass.Text.Trim())
                {
                    DataTable dt = Session["UserTable"] as DataTable;
                    dt.Rows[0]["Password"] = this.BusiNewPass.Text.Trim();
                    DataBase.update("UserInfo", "UserRecID", dt);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('密码修改成功！');window.location.href= 'BossWeb.aspx'", true);
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
            Response.Redirect("BossWeb.aspx");
        }
    }
}