using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskCustom.Models.Shared;

namespace ZendeskCustom.Models.Ticket
{
    public class IndividualTicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }

        [JsonProperty("audit")]
        public Audit Audit { get; set; }
    }
}
