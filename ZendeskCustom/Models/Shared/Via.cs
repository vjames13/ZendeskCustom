using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskCustom.Models;

namespace ZendeskCustom.Models
{
    public class Via
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
}
