using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunPrintClient
{
    public class EnDocType
    {
        private long DocTypeID;
        private string DocTypeName;
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
        public string docTypeName
        {
            get
            {
                return DocTypeName;
            }
            set
            {
                DocTypeName = value;
            }
        }
    }
}