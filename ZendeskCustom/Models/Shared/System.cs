using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models.Shared
{
    public class System
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}
