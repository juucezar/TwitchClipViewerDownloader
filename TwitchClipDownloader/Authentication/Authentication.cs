using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public class Authentication
    {
        private string url = "https://id.twitch.tv/oauth2/token?";
        public string client_id = "4zcl8fnt0vp56kxmei7c1il9upoh7n";
        private string client_secret = "ivl0h0oyon2a219dpt86jzb597cv0b";
        private string grant_type = "client_credentials";
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
