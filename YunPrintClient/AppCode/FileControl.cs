using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class FileControl
    {
        /**//// 
　　/// 对文件和文件夹的操作类 
　　/// 
　　
　　public FileControl() 
　　{ 
　　} 
　　/**//// 
　　/// 在根目录下创建文件夹 
　　/// 
　　/// 要创建的文件路径 
　　public void CreateFolder(string FolderPathName) 
　　{ 
　　if(FolderPathName.Trim().Length> 0) 
　　{ 
　　try 
　　{ 
　　string CreatePath = System.Web.HttpContext.Current.Server.MapPath("http://www.cnblogs.com/../Images/"+FolderPathName).ToString(); 
　　if(!Directory.Exists(CreatePath)) 
　　{ 
　　Directory.CreateDirectory(CreatePath); 
　　} 
　　} 
　　catch 
　　{ 
　　throw; 
　　} 
　　} 
　　} 
　　/**//// 
　　/// 删除一个文件夹下面的字文件夹和文件 
　　/// 
　　/// 
　　public void DeleteChildFolder(string FolderPathName) 
　　{ 
　　if(FolderPathName.Trim().Length> 0) 
　　{ 
　　try 
　　{ 
　　string CreatePath = System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString(); 
　　if(Directory.Exists(CreatePath)) 
　　{ 
　   　Directory.Delete(CreatePath,true); 
　　} 
　　} 
　　catch 
　　{ 
　　throw; 
　　} 
　　} 
　　} 
　　/**//// 
　　/// 删除一个文件 
　　/// 
　　public void DeleteFile(string FilePathName) 
　　{ 
　　try 
　　{ 
　　FileInfo DeleFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString()); 
　　DeleFile.Delete(); 
　　} 
　　catch 
　　{ 
　　} 
　　} 
　　public void CreateFile(string FilePathName) 
　　{ 
　　try 
　　{ 
　　//创建文件夹 
　　string[] strPath= FilePathName.Split('/'); 
　　CreateFolder(FilePathName.Replace("/" + strPath[strPath.Length-1].ToString(),"")); //创建文件夹 
　　FileInfo CreateFile =new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString()); //创建文件 
　　if(!CreateFile.Exists) 
　　{ 
　　FileStream FS=CreateFile.Create(); 
　　FS.Close(); 
　　} 
　　} 
　　catch 
　　{ 
　　} 
　　} 
　　/**//// 
　　/// 删除整个文件夹及其字文件夹和文件 
　　public void DeleParentFolder(string FolderPathName) 
　　{ 
　　try 
　　{ 
　　DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString()); 
　　if(DelFolder.Exists) 
　　{ 
　　DelFolder.Delete(); 
　　} 
　　} 
　　catch 
　　{ 
　　} 
　　}
  #region 暂时无用处
  /**/
  /// 
  /// 在文件里追加内容 
  public void ReWriteReadinnerText(string FilePathName, string WriteWord)
  {
      try
      {
          //建立文件夹和文件 
          //CreateFolder(FilePathName); 
          CreateFile(FilePathName);
          //得到原来文件的内容 
          FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.ReadWrite);
          StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
          string OldString = FileReadWord.ReadToEnd().ToString();
          OldString = OldString + WriteWord;
          //把新的内容重新写入 
          StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);
          FileWrite.Write(WriteWord);
          //关闭 
          FileWrite.Close();
          FileReadWord.Close();
          FileRead.Close();
      }
      catch
      {
          // throw; 
      }
  }
  /**/
  /// 
  /// 在文件里追加内容 
  public string ReaderFileData(string FilePathName)
  {
      try
      {
          FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.Read);
          StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
          string TxtString = FileReadWord.ReadToEnd().ToString();
          //关闭 
          FileReadWord.Close();
          FileRead.Close();
          return TxtString;
      }
      catch
      {
          throw;
      }
  }  
  #endregion
　　/**//// 
　　/// 读取文件夹的文件 
　　public DirectoryInfo checkValidSessionPath(string FilePathName) 
　　{ 
　　try 
　　{ 
　　DirectoryInfo MainDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName)); 
　　return MainDir; 
　　} 
　　catch 
　　{ 
　　throw; 
　　} 
　　} 
　　} 
　　}