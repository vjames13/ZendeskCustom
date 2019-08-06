using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models.Ticket
{
    public class CustomField
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
