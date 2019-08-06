using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace ZendeskCustom
{
    public class RequestResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}
