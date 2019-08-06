using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ZendeskCustom
{
    public class Core
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";
        protected string User;
        protected string Password;
        protected string ZendeskUrl;
        protected string ApiToken;

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zendeskApiUrl"></param>
        /// <param name="user"></param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Optional Param which is used if specified instead of the password</param>
        public Core(string zendeskApiUrl, string user, string password, string apiToken)
        {
            User = user;
            Password = password;
            ZendeskUrl = zendeskApiUrl;
            ApiToken = apiToken;
        }
        
        //Most of this code was just copied from the c# wrapper

        public T RunRequest<T>(string resource, string requestMethod, object body = null)
        {
            var response = RunRequest(resource, requestMethod, body);
            var obj = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return obj;
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null)
        {
            try
            {
                var requestUrl = ZendeskUrl;
                if (!requestUrl.EndsWith("/"))
                    requestUrl += "/";

                requestUrl += resource;

                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = "application/json";


                //req.Credentials = new NetworkCredential(User, Password);
                //req.Credentials = new System.Net.CredentialCache
                //                      {
                //                          {
                //                              new System.Uri(ZendeskUrl), "Basic",
                //                              new System.Net.NetworkCredential(User, Password)
                //                              }
                //                      };

                req.Headers["Authorization"] = GetPasswordOrTokenAuthHeader();
                req.PreAuthenticate = true;

                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";
                req.ContentLength = 0;

                if (body != null)
                {
                    var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    byte[] formData = UTF8Encoding.UTF8.GetBytes(json);
                    req.ContentLength = formData.Length;

                    var dataStream = req.GetRequestStream();
                    dataStream.Write(formData, 0, formData.Length);
                    dataStream.Close();
                }
                var res = req.GetResponse();
                HttpWebResponse response = res as HttpWebResponse;
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                string responseFromServer = reader.ReadToEnd();

                return new RequestResult()
                {
                    Content = responseFromServer,
                    HttpStatusCode = response.StatusCode
                };
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message + " " + ex.Response.Headers.ToString(), ex);
            }
        }
        protected string GetPasswordOrTokenAuthHeader()
        {
            if (!String.IsNullOrEmpty(ApiToken) && ApiToken.Trim().Length >= 0)
                return GetAuthHeader(User + "/token", ApiToken);

            return GetAuthHeader(User, Password);
        }
        protected string GetAuthHeader(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));
            return string.Format("Basic {0}", auth);
        }

        //Method for creating a ticket
        protected T GenericPost<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, "POST", body);
            return res;
        }
        //method for searching for a ticket by ticket ID
        protected T GenericGet<T>(string resource)
        {
            return RunRequest<T>(resource, "GET");
        }
    }
}
