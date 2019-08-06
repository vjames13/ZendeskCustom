using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models.Shared
{
    public class Upload
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }
    }

    public class UploadResult
    {
        [JsonProperty("upload")]
        public Upload Upload { get; set; }
    }
}
