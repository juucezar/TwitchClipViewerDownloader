using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private string url = "https://api.twitch.tv/helix/clips";

        public List<Clips> data { get; set; }

        public Clip() { }


        public async Task<List<Clips>> getClipByGameId(string game_id)
        {

            data = new List<Clips>();
            Authentication authentication = new Authentication();

            Task<Authentication> task = Task.Factory.StartNew(() => authentication.getAccessTokenAsync());
            task.Wait();

            while (task.IsCompleted)
            {

                authentication = task.Result;

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
                    data.Add(clipInfo);

                    return data;
                }
            }
            return null;

        }
    }
}
