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
using TwitchClipDownloader.Clips;

namespace TwitchClipDownloader
{
    class ClipViewModel : ViewModelBase
    {
        private Authentication authentication;
        private string url = ConfigurationManager.AppSettings["urlClip"];
        private ObservableCollection<ClipInfo> data = new ObservableCollection<ClipInfo>();
        public ObservableCollection<ClipInfo> Data
        {
            get { return data; }
            set
            {
                if (value != this.data)
                    data = value;

                this.SetPropertyChanged("GamesInfo");
            }
        }

        public string GameId { get; set; }

        public ClipViewModel()
        {
            authentication = Authenticate();
        }
        public ClipViewModel(string id)
        {
            authentication = Authenticate();
            getClipByGameId(id);
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
        public void getClipByGameId(string gameId)
        {
            List<ClipModel> clips = new List<ClipModel>();
            this.Data = new ObservableCollection<ClipInfo>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "game_id=" + gameId;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                HttpResponseMessage response = httpClient.GetAsync(builder.Uri).Result;

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                ClipModel clipInfo = JsonConvert.DeserializeObject<ClipModel>(responseJson.Result);
                clips.Add(clipInfo);

                foreach (var item in clips)
                    foreach (var v in item.data)
                        Data.Add(v);

                //return clips;
            }
        }
        public async Task<List<ClipModel>> getClipByBroadcasterId(string broadcaster_id)
        {
            List<ClipModel> clips = new List<ClipModel>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "broadcaster_id=" + broadcaster_id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                ClipModel clipInfo = JsonConvert.DeserializeObject<ClipModel>(responseJson.Result);
                clips.Add(clipInfo);

                return clips;
            }
        }
        public async Task<List<ClipModel>> getClipById(string id)
        {
            List<ClipModel> clips = new List<ClipModel>();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = "id=" + id;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.access_token);
                httpClient.DefaultRequestHeaders.Add("Client-ID", authentication.client_id);
                var response = await httpClient.GetAsync(builder.Uri);

                Task<string> responseJson = response.Content.ReadAsStringAsync();
                responseJson.Wait();
                ClipModel clipInfo = JsonConvert.DeserializeObject<ClipModel>(responseJson.Result);
                clips.Add(clipInfo);

                return clips;
            }
        }
    }
}
