using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YunPrintClient.AppCode;

namespace YunPrintClient
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = Session["UserTable"] as DataTable;
                EnOrder en = Session["OrderInfo"] as EnOrder;
                if (dt == null || en==null)
                    return;
                this.UserName.Text = dt.Rows[0]["UserName"].ToString();
                OrderNumber.Text = en.orderNumber.ToString();
                OrderTime.Text = en.placeOrderTime.ToString();
                
                Coments.Text = "";
                List<EnDoc> docslist = en.docs;
                Repeater1.DataSource = docslist;
                Repeater1.DataBind();
                string Sql = "select * from Address where UserRecID='" + dt.Rows[0]["UserRecID"].ToString() + "'";
                DataSet ST = DataBase.RunDataSet(Sql);
                DataTable S = ST.Tables[0];
                AddressList.DataSource = S;
                AddressList.DataValueField = "AddressRecID";
                AddressList.DataTextField = "AddressName";
                AddressList.DataBind();
                string mySql = "select * from OrderType";
                DataSet st = DataBase.RunDataSet(mySql);
                DataTable s = st.Tables[0];
                SendMothedList.DataSource = s;
                SendMothedList.DataValueField = "OrderTypeID";
                SendMothedList.DataTextField = "OrderTypeName";
                SendMothedList.DataBind();
                this.SendMothedList.SelectedIndex = 0;
                double addmoney = Convert.ToSingle(s.Rows[0]["SendPrice"].ToString()) * 1.0;
                this.AddMoney.Text = addmoney.ToString();
                float All = Convert.ToSingle(en.toalPrice.ToString()) + Convert.ToSingle(s.Rows[0]["SendPrice"].ToString());
                Money.Text = All.ToString();
            }
        }


        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void OrderBtu_Click(object sender, EventArgs e)
        {
            EnOrder en = Session["OrderInfo"] as EnOrder;
            CnOrders cn = new CnOrders();
            DataTable dt = cn.getDataTable("select * from Orders where OrderNumber='"+en.orderNumber.Trim()+"'");
            dt.Rows[0]["Coment"] = Coments.Text.Trim();
            DataSet ds = DataBase.RunDataSet("select * from Doc where 0=1");
            DataTable DocTable = ds.Tables[0];
            try
            {
                dt.Rows[0]["OrderTypeID"] = int.Parse(SendMothedList.SelectedItem.Value.Trim());
                dt.Rows[0]["AddressRecID"] = int.Parse(AddressList.SelectedItem.Value.Trim());
                dt.Rows[0]["ToalPrice"] = this.Money.Text;
                int k = DataBase.update("Orders", "OrderNumber", dt);
                List<EnDoc> docslist = en.docs;
                foreach (var item in docslist)
                {
                    DataRow dr = DocTable.NewRow();
                    dr["DocID"] = 0;
                    dr["OrderNumber"] = en.orderNumber.Trim();
                    dr["Comment"] = Coments.Text.Trim();
                    dr["DocName"] = item.docName;
                    dr["DocPath"] = item.docPath;
                    dr["DocTypeID"] = item.docTypeID;
                    dr["UploadTime"] = en.placeOrderTime;
                    dr["TotalPages"] = item.totalPages;
                    dr["PrintTypeID"] = item.printTypeID;
                    DocTable.Rows.Add(dr);
                }
               int j= DataBase.update("Doc", "DocID", DocTable);
                this.Page.RegisterStartupScript("",
                    "<script>alert('下单成功！');window.location.href= 'UserCenter.aspx'</script>");
            }
            catch (Exception)
            {
                int k =
                    DataBase.ExecuteNonQuery("delete Orders where OrderNumber='" +
                                             en.orderNumber + "'");
                int l = DataBase.ExecuteNonQuery("delete Doc where OrderNumber='" +
                                             en.orderNumber + "'");

                this.Page.RegisterStartupScript("",
                    "<script>alert('下单失败！');window.location.href= 'UserCenter.aspx'</script>");
            }
        }

        protected void SendMothedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnOrder en = Session["OrderInfo"] as EnOrder;
            string mySql = "select * from OrderType";
            DataSet st = DataBase.RunDataSet(mySql);
            DataTable s = st.Tables[0];
            float All = 0;
            if (this.SendMothedList.SelectedIndex==0)
            {
               double addmoney = Convert.ToSingle(s.Rows[0]["SendPrice"].ToString()) * 1.0;
               this.AddMoney.Text = addmoney.ToString();
               All = Convert.ToSingle(en.toalPrice.ToString()) + Convert.ToSingle(s.Rows[0]["SendPrice"].ToString());
            }
            else
            {
                if (this.SendMothedList.SelectedIndex ==1 )
                {
                    double addmoney = Convert.ToSingle(s.Rows[1]["SendPrice"].ToString()) * 1.0;
                    this.AddMoney.Text = addmoney.ToString();
                    All = Convert.ToSingle(en.toalPrice.ToString()) + Convert.ToSingle(s.Rows[1]["SendPrice"].ToString());
                }
                else
                {
                    double addmoney = Convert.ToSingle(s.Rows[2]["SendPrice"].ToString()) * 1.0;
                    this.AddMoney.Text = addmoney.ToString();
                    All = Convert.ToSingle(en.toalPrice.ToString()) + Convert.ToSingle(s.Rows[2]["SendPrice"].ToString());
                }
            }
            Money.Text = All.ToString();
        }

        protected void AddAdress_Click(object sender, EventArgs e)
        {
            this.AdressTex.Visible = true;
            this.SureAdress.Visible = true;

        }

        protected void SureAdress_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["UserTable"] as DataTable;
            string Sql = "select * from Address where UserRecID='" + dt.Rows[0]["UserRecID"].ToString() + "'";
            DataSet ST = DataBase.RunDataSet(Sql);
            DataTable S = ST.Tables[0];
            try
            {
                DataRow dr = S.NewRow();
                dr["AddressRecID"] = 0;
                dr["UserRecID"] = dt.Rows[0]["UserRecID"].ToString();
                dr["AddressName"] = this.AdressTex.Text.Trim();
                dr["IsUsuallyAddress"] = "0";
                S.Rows.Add(dr);
                int k = DataBase.update("Address", "AddressRecID", S);
                this.Page.RegisterStartupScript("", "<script>alert('增加成功！');window.location.href= 'Order.aspx'</script>");
            }
            catch (Exception)
            {
                this.Page.RegisterStartupScript("", "<script>alert('增加失败！');window.location.href= 'Order.aspx'</script>");
                this.SureAdress.Text = "";
            }
        }

        protected void Repeater1_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}