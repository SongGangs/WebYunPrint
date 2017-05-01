using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using System.Text;
using DataTable = System.Data.DataTable;
using YunPrintClient;
using YunPrintClient.AppCode;
using iTextSharp.text;
using iTextSharp.text.pdf;
public partial class _Default : System.Web.UI.Page
{
   
   #region Private Member Variables

      //  private static string UPLOADFOLDER = @"打堆云打印\12月 2015年\28日";
     //   private static List<file> fileList;
    private static string UPLOADFOLDER = "打堆云打印\\" + DateTime.Today.ToString(@"MM月 yyyy年\\dd日");
        
       // private static List<EnOrderType> OrdreTypeList;
        #endregion

        #region Web Methods

        protected void Page_Load(object sender, EventArgs args)
        {
            TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
            if (!this.IsPostBack)
            {
                TimeTxt.Text = DateTime.Now.ToString("yyyy-M-d dddd");
                DataTable dt = Session["UserTable"] as DataTable;
                if (dt == null)
                    return;
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
                EnUserInfo user = new EnUserInfo(dt.Rows[0]);
                this .userName1 .Text =user.uerName;
                string sql = "select * from Orders where 0=1";
                DataSet ds = DataBase.RunDataSet(sql);
                DataTable order = ds.Tables[0];
                DataRow dr = order.NewRow();
                dr["OrderNumber"] = 0;
                dr["UserRecID"] = user.userRecID;
                order.Rows.Add(dr);
                int NewOrderUmber=DataBase.update("Orders", "OrderNumber", order);
                this.Session["UploadDetail"] = new UploadDetail { IsReady = false, OrderUmber = NewOrderUmber };
               // updatefileInfo(ref NewfilesInfo);
                string sel = "select * from PrintType";
                Session["OrdreTypeList"] = new DataTable();//订单类型列表
                DataSet ords = DataBase.RunDataSet(sel);
                if(ords!=null)Session["OrdreTypeList"] = ords.Tables[0];


            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static object GetUploadStatus()
        {
            //Get the length of the file on disk and divide that by the length of the stream
            UploadDetail info = (UploadDetail) HttpContext.Current.Session["UploadDetail"];
            if (info != null && info.IsReady)
            {
                int soFar = info.UploadedLength;
                int total = info.ContentLength;
                int percentComplete = (int) Math.Ceiling((double) soFar/(double) total*100);
                string message = "Uploading...";
                string fileName = string.Format("{0}", info.FileName);
                string downloadBytes = string.Format("{0} of {1} Bytes", soFar, total);
                return new
                {
                    percentComplete = percentComplete,
                    message = message,
                    fileName = fileName,
                    downloadBytes = downloadBytes
                };
            }
            //Not ready yet
            return null;
        }

        #endregion
      
        #region Web Callbacks

        protected void gvNewFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "eventMouseOver(this)");
                e.Row.Attributes.Add("onmouseout", "eventMouseOut(this)");
            }
        }

       
     
        #endregion

        #region Support Methods

      
     

        public string DeleteFile(string FileName)
        {
            string strMessage = "";
            try
            {
                string strPath = Path.Combine(UPLOADFOLDER, FileName);
                if (File.Exists(Server.MapPath(strPath)) == true)
                {
                    File.Delete(Server.MapPath(strPath));
                    strMessage = "File Deleted";
                }
                else
                    strMessage = "File Not Found";
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
            }
            return strMessage;
        }

        public string CalculateFileSize(double FileInBytes)
        {
            string strSize = "00";
            if (FileInBytes < 1024)
                strSize = FileInBytes + " B"; //Byte
            else if (FileInBytes > 1024 & FileInBytes < 1048576)
                strSize = Math.Round((FileInBytes/1024), 2) + " KB"; //Kilobyte
            else if (FileInBytes > 1048576 & FileInBytes < 107341824)
                strSize = Math.Round((FileInBytes/1024)/1024, 2) + " MB"; //Megabyte
            else if (FileInBytes > 107341824 & FileInBytes < 1099511627776)
                strSize = Math.Round(((FileInBytes/1024)/1024)/1024, 2) + " GB"; //Gigabyte
            else
                strSize = Math.Round((((FileInBytes/1024)/1024)/1024)/1024, 2) + " TB"; //Terabyte
            return strSize;
        }

        #endregion

      
     
      
        protected void UpFilesInfo_OnValueChanged(object sender, EventArgs e)
        {
            updatefileInfo(ref NewfilesInfo);
       }

        private void updatefileInfo(ref DataList NewfilesInfo)
        {
            List<file> files = getFilesList(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
            NewfilesInfo.DataSource = files;
            NewfilesInfo.DataBind();           
            ChangeTotalPrice();
        }

        private List<file> getFilesList(string sourcePath)
        {
            List<file> fileList = new List<file>();
            UploadDetail upDetail = Session["UploadDetail"] as UploadDetail;
            DataTable OrdreTypeList = Session["OrdreTypeList"] as DataTable;
            if ((Directory.Exists(sourcePath)))
            {

                DirectoryInfo dir = new DirectoryInfo(sourcePath);
                foreach (FileInfo files in dir.GetFiles("*.*"))
                {
                    string str = files.Name;
                    string orderUmber = str.Substring(0, str.IndexOf(" "));

                    if (orderUmber == upDetail.OrderUmber.ToString()) //根据订单号找对应文件
                    {
                        file theFile = new file();
                        theFile.orderID = upDetail.OrderUmber;
                        theFile.fileName = files.Name;
                        theFile.filepath = sourcePath;
                        theFile.printTypeTable = OrdreTypeList;
                        theFile.onePagePrice = float.Parse(OrdreTypeList.Rows[0]["SinglePagePrice"].ToString());//默认黑白打印的价格
                        theFile.count = 1;//默认打印一份
                        //(str.Substring(0, str.LastIndexOf("."))
                        //  string db = str.Trim();
                        string suffixName = str.Substring(str.LastIndexOf(".") + 1);
                        if (suffixName == "doc" || suffixName=="docx")

                        {
                            string path = sourcePath + @"\" + files.Name;

                            theFile.numberOfPages = GetPageCount(path); // 加这个方法时报错
                            theFile.price = (theFile.numberOfPages) * (theFile.onePagePrice) * (theFile.count);
                            fileList.Add(theFile);
                            
                        }
                        else if (suffixName == "pdf")
                        {
                            string path = sourcePath + @"\" + files.Name;
                            theFile.numberOfPages = GetPDFPageCountByDll(path); 
                            theFile.price = (theFile.numberOfPages) * (theFile.onePagePrice) * (theFile.count);
                            fileList.Add(theFile);
                        }
                    }
                }
             
                return fileList;
            }
            else
            {
                return null;
            }
         
        }
        //获取文件页数
        private int GetPageCount(string path)
        {
             Microsoft.Office.Interop.Word.Application myWordApp = new Microsoft.Office.Interop.Word.Application();
             object  oMissing = System.Reflection.Missing.Value;
            
            try
            {
                myWordApp.Visible = false;
                object filePath = path; //这里是Word文件的路径
                //打开文件
                //Microsoft.Office.Interop.Word.Document myWordDoc = myWordApp.Documents.Open(
                //    ref filePath, ref oMissing);
                
                Microsoft.Office.Interop.Word.Document myWordDoc;
                myWordDoc = myWordApp.Documents.Add(ref oMissing);
                   myWordDoc= myWordApp.Documents.Open(
                 ref filePath, ref oMissing);
               // myWordDoc = myWordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing);
                //下面是取得打开文件的页数

                int pages = myWordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, ref  oMissing);
                myWordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                oMissing = null;
                myWordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(myWordApp);
                myWordApp = null;
                // 垃圾回收
                GC.Collect();



              //myWordApp.Quit(ref oMissing, ref oMissing, ref oMissing);  
                
                return pages;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //<summary>
        //关闭Word.Application对象
        //</summary>
        //private void QuitWordApp()
        //{
        //    if (myWordApp != null)
        //    {
        //        //关闭文件
        //        myWordApp.Quit(ref oMissing);
        //    }
        //}

       //获取pdf的页数
        public int GetPDFPageCountByDll(string path)
        {
          
            // 创建一个PdfReader对象
            PdfReader reader = new PdfReader(path);
          //  PdfAWriter reader = new PdfAWriter(path);
            // 获得文档页数
            int pagecount = reader.NumberOfPages;
            return pagecount;
        }   
       

     //文件份数改变
        protected void CopiesTextBox_OnTextChanged(object sender, EventArgs e)
        {
           
            TextBox Copies = (TextBox) sender;
            int count = Convert.ToInt32(Copies.ValidationGroup);
            Label Price = NewfilesInfo.Items[count].FindControl("PriceLable") as Label;
            Label PageCount = NewfilesInfo.Items[count].FindControl("PageCount") as Label;
            Label PicePrice= NewfilesInfo.Items[count].FindControl("PicePrice") as Label;
            Decimal theLastPrice = Convert.ToDecimal(PicePrice.Text);
            Decimal price = Convert.ToInt32(Copies.Text)*Convert.ToInt32(PageCount.Text)*theLastPrice;                         
            Price.Text = price.ToString();           
            ChangeTotalPrice();
        }
        //更改总价
         private void ChangeTotalPrice()
        {
            Decimal totalPrice = 0;
            for (int i = 0; i < NewfilesInfo.Items.Count; i++)
            {
                totalPrice = totalPrice + Convert.ToDecimal((NewfilesInfo.Items[i].FindControl("PriceLable") as Label).Text);
            }
            int CTCount = NewfilesInfo.Controls.Count;
            Label dd = (Label)NewfilesInfo.Controls[CTCount - 1].FindControl("totalPrice");
            dd.Text = totalPrice.ToString();
        }
        //减少份数
        protected void TallyDown_OnClick(object sender, EventArgs e)
        {
            Button tally = (Button)sender;
            int count = Convert.ToInt32(tally.ValidationGroup);
            TextBox Copies = NewfilesInfo.Items[count].FindControl("CopiesTextBox") as TextBox;
            Copies.Text = (Convert.ToInt32(Copies.Text)-1).ToString();
            CopiesTextBox_OnTextChanged(Copies,null);
        }
        //增加份数
        protected void AddOne_OnClick(object sender, EventArgs e)
        {
            Button Add = (Button)sender;
            int count = Convert.ToInt32(Add.ValidationGroup);
            TextBox Copies = NewfilesInfo.Items[count].FindControl("CopiesTextBox") as TextBox;
            Copies.Text = (Convert.ToInt32(Copies.Text)+1).ToString();
            CopiesTextBox_OnTextChanged(Copies, null);
        }

        protected void PrintTypes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList PrintTypes = (DropDownList)sender;
            int count = Convert.ToInt32(PrintTypes.ValidationGroup);
            Label picePrice = NewfilesInfo.Items[count].FindControl("PicePrice") as Label;
            //根据选中的打印类型决定单页价格
            DataTable OrdreTypeList = Session["OrdreTypeList"] as DataTable;
            string selectType = PrintTypes.SelectedItem.Text;
            foreach (DataRow order in OrdreTypeList.Rows)
            {
                if (order["PrintName"].ToString() == selectType)
                {
                    picePrice.Text = order["SinglePagePrice"].ToString();
                }
            }
         //   picePrice.Text = "10";
            Label Price = NewfilesInfo.Items[count].FindControl("PriceLable") as Label;
            Label PageCount = NewfilesInfo.Items[count].FindControl("PageCount") as Label;
            TextBox Copies = NewfilesInfo.Items[count].FindControl("CopiesTextBox") as TextBox;
            Decimal nowSingelPrice = Convert.ToDecimal(picePrice.Text);
            Decimal price = Convert.ToInt32(Copies.Text) * Convert.ToInt32(PageCount.Text) * nowSingelPrice;
            Price.Text = price.ToString();        
            ChangeTotalPrice();


        }     
        protected void UpLoadBut_OnClick(object sender, EventArgs e)
        {
            EnOrder order=new EnOrder();
            UploadDetail upDetail = Session["UploadDetail"] as UploadDetail;
            order.orderNumber = upDetail.OrderUmber.ToString();
            order.orderStatusID = 1;
            order.placeOrderTime=DateTime.Now;
            int CTCount = NewfilesInfo.Controls.Count;
            Label toPrice = (Label)NewfilesInfo.Controls[CTCount - 1].FindControl("totalPrice");
            order.toalPrice = decimal.Parse(toPrice.Text);
            order.docs = new List<EnDoc>();
            for (int i = 0; i < NewfilesInfo.Items.Count; i++)
            {
                Label fn = (Label) NewfilesInfo.Items[i].FindControl("fileName");
                Label ph = (Label)NewfilesInfo.Items[i].FindControl("filepath");
                DropDownList pt = (DropDownList)NewfilesInfo.Items[i].FindControl("PrintTypes");
                Label pc = (Label)NewfilesInfo.Items[i].FindControl("PageCount");
                TextBox num = (TextBox)NewfilesInfo.Items[i].FindControl("CopiesTextBox");
                
                
                order.docs.Add(new EnDoc
                {                  
                    orderNumber = order.orderNumber,
                    docName = fn.Text,
                    docPath=ph.Text,
                    printTypeID = long.Parse(pt.SelectedItem.Value.Trim()),
                    totalPages = int.Parse(pc.Text.Trim()) * int.Parse(num.Text.Trim())
                });
            }
           CnOrders cn=new CnOrders();
           DataTable dt = cn.getOrderTabel(order);
           int k = DataBase.update("Orders", "OrderNumber", dt);
           Session["OrderInfo"] = new EnOrder();
          
           Session["OrderInfo"] = order;
           Session.Remove("UploadDetail");
           Session.Remove("OrdreTypeList");
       
           Response.Redirect("Order.aspx");
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>;window.location.replace('Login.aspx')</script>");
            Session.Abandon();
        }
   
}


