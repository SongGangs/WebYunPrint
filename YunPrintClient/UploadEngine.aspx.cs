using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace YunPrintClient
{
    public partial class UploadEngine : System.Web.UI.Page
    {
        #region Web Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                UploadDetail Upload = (UploadDetail)this.Session["UploadDetail"];
                //Let the webservie know that we are not yet ready
                Upload.IsReady = false;
                if (this.fileUpload.PostedFile != null && this.fileUpload.PostedFile.ContentLength > 0)
                {

                    //START : Saving File in Database
                    SaveFile saveFileInDB = new SaveFile();
                    saveFileInDB.FileName =this.fileUpload.PostedFile.FileName;
                    saveFileInDB.FileExtension = Path.GetExtension(this.fileUpload.PostedFile.FileName);
                    saveFileInDB.FileContent = this.fileUpload.FileBytes;
                    Result Result = saveFileInDB.SaveFileInDB();
                    if (Result.IsError == false)
                    {
                        //File Save in Database Successfully!
                    }
                    else
                    {
                        //Error in Saving File in Database!
                        //Error: Result.ErrorMessage
                        //InnerException: Result.InnerException
                        //StackTrace: Result.StackTrace
                    }
                    //END : Saving File in Database



                    //build the local path where upload all the files
                  //  string path = this.Server.MapPath(@"打堆云打印\12月 2015年\28日");
                    string filepath =this.Server.MapPath("打堆云打印\\" +DateTime.Today.ToString(@"MM月 yyyy年\\dd日"));
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    //string path = @"F:\打堆云打印\12月 2015年\28日";
                    string fileName = Upload.OrderUmber.ToString()+" " + Path.GetFileName(this.fileUpload.PostedFile.FileName);

                    //Build the strucutre and stuff it into session
                    Upload.ContentLength = this.fileUpload.PostedFile.ContentLength;
                    Upload.FileName = fileName;
                    Upload.UploadedLength = 0;
                    //Let the polling process know that we are done initializing ...
                    Upload.IsReady = true;


                    //set the buffer size to something larger.
                    //the smaller the buffer the longer it will take to download, 
                    //but the more precise your progress bar will be.
                    int bufferSize = 1;
                    byte[] buffer = new byte[bufferSize];

                    //Writing the byte to disk
                    using (FileStream fs = new FileStream(Path.Combine(filepath, fileName), FileMode.Create))
                    {
                        //Aslong was we haven't written everything ...
                        while (Upload.UploadedLength < Upload.ContentLength)
                        {
                            //Fill the buffer from the input stream
                            int bytes = this.fileUpload.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                            //Writing the bytes to the file stream
                            fs.Write(buffer, 0, bytes);
                            //Update the number the webservice is polling on to the session
                            Upload.UploadedLength += bytes;
                        }
                    }
                    //Call parent page know we have processed the uplaod
                    const string js = "window.parent.onComplete(1,'文件上传成功.','{0}','{1} of {2} Bytes');";
                    ScriptManager.RegisterStartupScript(this, typeof(UploadEngine), "progress", string.Format(js, fileName, Upload.UploadedLength, Upload.ContentLength), true);
                }
                else
                {
                    //Call parent page know we have processed the uplaod
                    const string js = "window.parent.onComplete(4, 'There was a problem with the file.','','0 of 0 Bytes');";
                    ScriptManager.RegisterStartupScript(this, typeof(UploadEngine), "progress", js, true);
                }
                //Let webservie know that we are not yet ready
                Upload.IsReady = false;
            }
        }
        #endregion
    }

}