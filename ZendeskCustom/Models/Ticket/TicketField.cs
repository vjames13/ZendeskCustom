using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskCustom.Models.Ticket
{
    public class TicketFieldBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("collapsed_for_agents")]
        public bool CollapsedForAgents { get; set; }

        [JsonProperty("regexp_for_validation")]
        public object RegexpForValidation { get; set; }

        [JsonProperty("title_in_portal")]
        public string TitleInPortal { get; set; }

        [JsonProperty("visible_in_portal")]
        public bool VisibleInPortal { get; set; }

        [JsonProperty("editable_in_portal")]
        public bool EditableInPortal { get; set; }

        [JsonProperty("required_in_portal")]
        public bool RequiredInPortal { get; set; }

        [JsonProperty("tag")]
        public object Tag { get; set; }

        [JsonProperty("custom_field_options")]
        public IList<CustomFieldOptions> CustomFieldOptions { get; set; }

    }

    public class TicketField : TicketFieldBase
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

    public class CustomFieldOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
