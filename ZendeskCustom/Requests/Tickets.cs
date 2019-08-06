using System;
using System.Collections.Generic;
using System.Text;
using ZendeskCustom.Models.Ticket;

namespace ZendeskCustom.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";
        private const string _views = "views";
        private const string _organizations = "organizations";


        public Tickets(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }
        public IndividualTicketResponse CreateTicket(Ticket ticket)
        {
            var body = new { ticket };
            return GenericPost<IndividualTicketResponse>(_tickets + ".json", body);
        }
        public IndividualTicketResponse GetTicket(long id)
        {
            return GenericGet<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }
        public GroupTicketFieldResponse GetTicketFields()
        {
            return GenericGet<GroupTicketFieldResponse>("ticket_fields.json");
        }
    }
}
