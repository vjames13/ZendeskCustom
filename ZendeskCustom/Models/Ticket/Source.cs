using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models
{
    public class Source
    {
        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("to")]
        public To To { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }
    }
}
