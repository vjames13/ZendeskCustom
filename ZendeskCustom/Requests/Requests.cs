using System;
using System.Collections.Generic;
using System.Text;
using ZendeskCustom.Models.Requests;

namespace ZendeskCustom.Requests
{
    public class Requests : Core
    {
        public Requests(string yourZendeskUrl, string user, string password, string apiToken)
           : base(yourZendeskUrl, user, password, apiToken)
        {
        }
        public IndividualRequestResponse CreateRequest(Request request)
        {
            var body = new { request };
            return GenericPost<IndividualRequestResponse>("requests.json", body);
        }
    }
}
