using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Request
{
    public class RequestResult
    {
        public RequestStateEnum State { get; set; }
        public string Msg { get; set; }
        public Object Data { get; set; }
    }
}
