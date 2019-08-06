using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskCustom.Models.Ticket;

namespace ZendeskCustom.Models.Shared
{
    public class MetaData
    {
        [JsonProperty("custom")]
        public Custom Custom { get; set; }

        [JsonProperty("system")]
        public System System { get; set; }
    }
}
