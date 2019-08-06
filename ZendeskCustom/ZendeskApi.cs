using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using ZendeskCustom.Requests;

namespace ZendeskCustom
{
    public class ZendeskApi
    {
            public Tickets Tickets { get; set; }
            public Attachments Attachments { get; set; }
            public Requests.Requests Requests { get; set; }

            public string ZendeskUrl { get; set; }

            /// <summary>
            /// Constructor that takes 3 params.
            /// </summary>
            /// <param name="yourZendeskUrl">Will be formated to "https://yoursite.zendesk.com/api/v2"</param>
            /// <param name="user"></param>
            /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
            /// <param name="apiToken">Optional Param which is used if specified instead of the password</param>
            public ZendeskApi(string yourZendeskUrl, string user, string password = "", string apiToken = "")
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var formattedUrl = GetFormattedZendeskUrl(yourZendeskUrl).AbsoluteUri;

                Tickets = new Tickets(formattedUrl, user, password, apiToken);
                Attachments = new Attachments(formattedUrl, user, password, apiToken);
                Requests = new Requests.Requests(formattedUrl, user, password, apiToken);

                ZendeskUrl = formattedUrl;
            }

            Uri GetFormattedZendeskUrl(string yourZendeskUrl)
            {
                yourZendeskUrl = yourZendeskUrl.ToLower();

                //Make sure the Authority is https://
                if (yourZendeskUrl.StartsWith("http://"))
                    yourZendeskUrl = yourZendeskUrl.Replace("http://", "https://");

                if (!yourZendeskUrl.StartsWith("https://"))
                    yourZendeskUrl = "https://" + yourZendeskUrl;

                if (!yourZendeskUrl.EndsWith("/api/v2"))
                {
                    //ensure that url ends with ".zendesk.com/api/v2"
                    yourZendeskUrl = yourZendeskUrl.Split(new[] { ".zendesk.com" }, StringSplitOptions.RemoveEmptyEntries)[0] + ".zendesk.com/api/v2";
                }

                return new Uri(yourZendeskUrl);
            }
        }
}
