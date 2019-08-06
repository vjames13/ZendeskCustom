using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskCustom.Models.Ticket;

namespace ZendeskCustom.Models.Requests
{
    public class Request
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}
