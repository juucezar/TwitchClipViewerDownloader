using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TwitchClipDownloader
{
    public class Authentication
    {
        private string url = ConfigurationManager.AppSettings["urlAuthentication"];
        public string client_id = ConfigurationManager.AppSettings["client_id"];
        private string client_secret = ConfigurationManager.AppSettings["client_secret"];
        private string grant_type = ConfigurationManager.AppSettings["grant_type"];
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }

        public Authentication(){}

        public Authentication getAccessTokenAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id",  this.client_id),
                    new KeyValuePair<string, string>("client_secret",  this.client_secret),
                    new KeyValuePair<string, string>("grant_type",  this.grant_type)
                });

                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                
                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                Authentication authentication = JsonConvert.DeserializeObject<Authentication>(responseJson.Result);

                return authentication;
            }
        }
    }
}
