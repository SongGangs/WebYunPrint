using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnDoc
    {
        private long DocID;
        private long DocTypeID;
        private string  OrderNumber;
        private string DocName;
        private DateTime  UploadTime;
        private string Comment;
        private string DocPath;
        private long PrintTypeID;
        private int TotalPages;
        public long docID
        {
            get
            {
                return DocID;
            }
            set
            {
                DocID = value;
            }
        }
        public long docTypeID
        {
            get
            {
                return DocTypeID;
            }
            set
            {
                DocTypeID = value;
            }
        }
        public string docName
        {
            get
            {
                return DocName;
            }
            set
            {
                DocName = value;
            }
        }
        public string orderNumber
        {
            get
            {
                return OrderNumber;
            }
            set
            {
                OrderNumber = value;
            }
        }
        public DateTime uploadTime
        {
            get
            {
                return UploadTime;
            }
            set
            {
                UploadTime = value;
            }
        }
        public string comment
        {
            get
            {
                return Comment;
            }
            set
            {
                Comment = value;
            }
        }
        public string docPath
        {
            get
            {
                return DocPath;
            }
            set
            {
                DocPath = value;
            }
        }

        public long printTypeID
        {
            get
            {
                return PrintTypeID;
            }
            set
            {
                PrintTypeID = value;
            }
        }

        public int totalPages
        {
            get
            {
                return TotalPages;
            }
            set
            {
                TotalPages = value;
            }
        }
    }
}