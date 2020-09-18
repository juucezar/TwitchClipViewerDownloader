using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchClipDownloader.Clips;

namespace TwitchClipDownloader
{
    public class GamesModel : ViewModelBase
    {
        private List<GameInfo> data = null;
        public List<GameInfo> Data
        {
            get { return data; }
            set
            {
                if (value != this.data)
                    data = value;

                this.SetPropertyChanged("GamesInfo");
            }
        }
        public PaginationGames Pagination { get; set; }

        public GamesModel() { }
    }
    public class PaginationGames
    {
        public string Cursor { get; set; }
    }
    public class GameInfo
    {
        private string box_art_url = string.Empty;
        public string Box_art_url { get { return box_art_url; } 
            set 
            { 
                if(value != null)
                {
                    value = Regex.Replace(value, "{height}", "200", RegexOptions.IgnoreCase);
                    value = Regex.Replace(value, "{width}", "150", RegexOptions.IgnoreCase);
                    box_art_url = value;
                }
            }
        }
        public string Id { get; set; }
        public string Name { get; set; }

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

        private void Teste(object id)
        {
            ClipsView clipsView = new ClipsView(id.ToString());
            clipsView.Show();
        }
    }

}
