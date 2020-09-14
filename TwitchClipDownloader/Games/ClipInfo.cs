﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClipDownloader
{

    public class Clips
    {
        public ClipInfo[] data { get; set; }
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
    }

}
