using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public class Clip
    {
        private string url = ConfigurationManager.AppSettings["urlClip"];
        public Clip() { }
        public async Task<List<Clips>> getClipByGameId(Authentication authentication, string game_id)
        {
            List<Clips> clips = new List<Clips>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "game_id=" + game_id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                Clips clipInfo = JsonConvert.DeserializeObject<Clips>(responseJson.Result);
                clips.Add(clipInfo);

                return clips;
            }
        }

        public async Task<List<Clips>> getClipByBroadcasterId(Authentication authentication, string broadcaster_id)
        {
            List<Clips> clips = new List<Clips>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "broadcaster_id=" + broadcaster_id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                Clips clipInfo = JsonConvert.DeserializeObject<Clips>(responseJson.Result);
                clips.Add(clipInfo);

                return clips;
            }
        }

        public async Task<List<Clips>> getClipById(Authentication authentication, string id)
        {
            List<Clips> clips = new List<Clips>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "id=" + id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                Clips clipInfo = JsonConvert.DeserializeObject<Clips>(responseJson.Result);
                clips.Add(clipInfo);

                return clips;
            }
        }
    }
}
