using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Result
/// </summary>
public class Result
{
    public bool IsError { set; get; }
    public string ErrorMessage { set; get; }
    public string InnerException { set; get; }
    public string StackTrace { set; get; }
}