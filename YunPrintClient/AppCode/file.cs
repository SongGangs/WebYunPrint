using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// file 的摘要说明
/// </summary>
public class file
{
    private int OrderID;//订单号
    private int  NumberOfPages;//页数
    private List<string> PrintType;//打印类型
    private int Count;//份数
    private float OnePagePrice;//单页价格
    private string FileName;//文件名
    private string SurePrintType;
    private string Filepath;
    private float Price;
    private DataTable PrintTypeTable;

    public DataTable printTypeTable
    {
        get { return PrintTypeTable; }
        set { PrintTypeTable = value; }
    }

    public string filepath
    {
        get { return Filepath; }
        set { Filepath = value; }
    }
    public int orderID
    {
        get { return OrderID; }
        set { OrderID = value; }
    }
    public int  numberOfPages
    {
        get { return NumberOfPages; }
        set { NumberOfPages = value; }
    }
    public List<string> printType
    {
        get { return PrintType; }
        set
        {
            PrintType = value;
        }
    }
    public string surePrintType
    {
        get { return SurePrintType; }
        set
        {
         
            if (value == "黑白打印") onePagePrice = 0.50f;
            if (value == "彩印") onePagePrice = 1.00f;
            Price = numberOfPages * onePagePrice * count;
            SurePrintType = value;
        }
    }
    public float onePagePrice
    {
        get { return OnePagePrice; }
        set { OnePagePrice = value; }
    }
   
    public int count
    {
        get { return Count; }
        set
        { 
            Count = value;
            Price = numberOfPages * onePagePrice * value;

        }

    }
    public float price
    {
        get { return Price; }
        set { Price = value; }
    }
    public string fileName
    {
        get { return FileName; }
        set { FileName = value; }
    }
	public file()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
       
	}
    public file (string  defult)
    {
        if(defult=="defult")
        {
            orderID = 1001;

            numberOfPages = 10;
            surePrintType = "黑白打印";
            fileName = "201301110211毕业设计";
            count = 10;
            PrintType = new List<string>();
            PrintType.Add("黑白打印");
            PrintType.Add("彩印");          
        }
    }
}