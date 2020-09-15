using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
    public class Game
    {
        private string url = ConfigurationManager.AppSettings["urlGame"];

        public Game() { }
        public async Task<GamesModel> GetGameByName(Authentication authentication, string name)
        {
            GamesModel games = new GamesModel();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "name=" + name;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);

                return games;
            }

        }
        public async Task<GamesModel> GetGameById(Authentication authentication, string id)
        {
            GamesModel games = new GamesModel();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "id=" + id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);

                return games;
            }

        }

        public GamesModel GetTopGames(Authentication authentication)
        {
            GamesModel games = new GamesModel();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["urlTopGame"]);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                HttpResponseMessage response = httpClient.GetAsync(builder.Uri).Result;

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);

                return games;
            }

        }

    }



}
