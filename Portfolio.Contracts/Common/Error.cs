using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Contracts.Common
{
    public class Error
    {
        public int? ErrorCode { get; set; }
        public string Message { get; set; }
        public object AdditionalInfos { get; set; }
    }
}
