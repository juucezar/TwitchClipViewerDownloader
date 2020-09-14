using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public class Game
    {
        private string url = "https://api.twitch.tv/helix/games";
        
        public Game() { }
        public async Task<Games> GetGameByName(string name)
        {
            Authentication authentication = new Authentication();

            Task<Authentication> task = Task.Factory.StartNew(() => authentication.getAccessTokenAsync());
            task.Wait();
            while (task.IsCompleted)
            {
                authentication = task.Result;
                Games games = new Games();
                using (HttpClient httpClient = new HttpClient())
                {
                    UriBuilder builder = new UriBuilder(url);
                    builder.Query = "name=" + name;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                    httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                    var response = await httpClient.GetAsync(builder.Uri);

                    Task<string> responseJson = response.Content.ReadAsStringAsync();
                    responseJson.Wait();
                    games = JsonConvert.DeserializeObject<Games>(responseJson.Result);

                    return games;
                }
            }
            return null;
        }

    }


    public class Games
    {
        public GameInfo[] data { get; set; }
        public PaginationGames pagination { get; set; }
    }
    public class PaginationGames
    {
        public string cursor { get; set; }
    }
    public class GameInfo
    {
        public string box_art_url { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

}
