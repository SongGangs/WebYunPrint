using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Office.Interop.Word;

namespace YunPrintClient
{
    public class Common
    {
        private static Microsoft.Office.Interop.Word.Application myWordApp = new Microsoft.Office.Interop.Word.Application();
        private static object oMissing = System.Reflection.Missing.Value;
        public Common()
        {
           
        }
        //获取文档页数
        public  int GetPageCount(string path)
        {
            try
            {
                myWordApp.Visible = false;
                object filePath = path; //这里是Word文件的路径
                //打开文件
                Microsoft.Office.Interop.Word.Document myWordDoc = myWordApp.Documents.Open(
                    ref filePath, ref oMissing);
                //下面是取得打开文件的页数
                int pages = myWordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, ref  oMissing);
                myWordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
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
        public  void QuitWordApp()
        {
            if (myWordApp != null)
            {
                //关闭文件
                myWordApp.Quit(ref oMissing);
            }
        }
        //弹出窗口小信息
        public static void Alert(string AlertStr)
        {
            string Alert = "";
            Alert = "<script language='javascript'>alert('"
                + AlertStr + "')</script>";
            HttpContext.Current.Response.Write(Alert);
        }
        //弹出提示并跳转
        public static void AlertAndRedirect
            (string Message, string RedirectUrl)
        {
            string js = "";
            js = "<script language='javascript'> " +
                 "alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write
                (string.Format(js, Message, RedirectUrl));
        }
        //弹出新页面
        public static void AlertNewWebForm
            (string url, int width, int heigth, int top, int left)
        {
            string js = @"<Script language='JavaScript'>window.open('" +
                url + @"','','height=" + heigth +
                ",width=" + width + ",top=" + top + ",left=" +
                left + ",location=no,menubar=no,resizable=yes,scrollbars=yes," +
                "status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        //普通字符串到MD5编码的代码
        public static string StringToMD5(string password)
        {
            Byte[] by = Encoding.Default.GetBytes(password);
            Byte[] by1 = MD5.Create().ComputeHash(by);
            string len = BitConverter.ToString(by1).Replace("-", "");
            return len;
        }
    }
}
