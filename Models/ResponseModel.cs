using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Company_Solution.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseData { get; set; }
    }
}
