using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models.Requests
{
    public class IndividualRequestResponse
    {
        [JsonProperty("request")]
        public Request Request { get; set; }
    }
}
