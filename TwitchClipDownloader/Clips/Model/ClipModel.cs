using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TwitchClipDownloader.Clips;

namespace TwitchClipDownloader
{

    public class ClipModel
    {
        public List<ClipInfo> data { get; set; }
        public PaginationClip pagination { get; set; }
    }

    public class PaginationClip
    {
        public string cursor { get; set; }
    }

    public class ClipInfo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string embed_url { get; set; }
        public string broadcaster_id { get; set; }
        public string broadcaster_name { get; set; }
        public string creator_id { get; set; }
        public string creator_name { get; set; }
        public string video_id { get; set; }
        public string game_id { get; set; }
        public string language { get; set; }
        public string title { get; set; }
        public int view_count { get; set; }
        public DateTime created_at { get; set; }
        public string thumbnail_url { get; set; }

        private bool CanExecute = true;
        private ICommand buttonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                if (buttonCommand == null)
                {
                    buttonCommand = new RelayCommand(p => this.CanExecute, p => this.Teste(p));
                }
                return buttonCommand;
            }
        }

        private void Teste(object url)
        {
            ClipsView clipsView = new ClipsView();
            clipsView.playMedia(url.ToString());
        }

    }

}
