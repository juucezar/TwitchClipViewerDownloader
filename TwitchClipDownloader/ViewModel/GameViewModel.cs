using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace TwitchClipDownloader
{
    class GameViewModel : ViewModelBase
    {
       
        private Authentication authentication;
        private ObservableCollection<GameInfo> data= new ObservableCollection<GameInfo>();
        public ObservableCollection<GameInfo> Data
        {
            get { return data; }
            set
            {
                if (value != this.data)
                    data = value;

                this.SetPropertyChanged("GamesInfo");
            }
        }

        private string url = ConfigurationManager.AppSettings["urlGame"];

        public GameViewModel() 
        {
            authentication = Authenticate();
            GetTopGames(authentication);
        }

        public Authentication Authenticate()
        {
            Authentication authentication = new Authentication();

            Task<Authentication> task = Task.Factory.StartNew(() => authentication.getAccessTokenAsync());
            task.Wait();
            while (task.IsCompleted)
            {
                return task.Result;
            }
            return null;
        }

        public async void GetGameByName(Authentication authentication, string name)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "name=" + name;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                GamesModel games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);
                foreach (var v in games.Data)
                    Data.Add(v);
            }

        }
        public async void GetGameById(Authentication authentication, string id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "id=" + id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                GamesModel games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);
                foreach (var v in games.Data)
                    Data.Add(v);
            }

        }
        public void GetTopGames(Authentication authentication)
        {
             
            this.Data = new ObservableCollection<GameInfo>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["urlTopGame"]);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                HttpResponseMessage response = httpClient.GetAsync(builder.Uri).Result;

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                GamesModel games = JsonConvert.DeserializeObject<GamesModel>(responseJson.Result);

                foreach (var itens in games.Data)
                    Data.Add(itens);

            }

            
        }
    }
}
