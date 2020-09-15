using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{
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
