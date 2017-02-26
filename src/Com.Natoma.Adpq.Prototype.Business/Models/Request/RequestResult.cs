using System;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Request
{
    public class RequestResult
    {
        public RequestStateEnum State { get; set; }
        public string Msg { get; set; }
        public Object Data { get; set; }
    }
}
