using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TwitchClipDownloader.Clips
{
    /// <summary>
    /// Interaction logic for ClipsView.xaml
    /// </summary>
    public partial class ClipsView : Window
    {
        public ClipsView()
        {
        }

        public ClipsView(string id)
        {
            InitializeComponent();
            this.DataContext = new ClipViewModel(id);

            myMediaElement.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void playMedia(string url)
        {
            //using (var client = new WebClient())
            //{
            //    client.DownloadFile("http://clips-media-assets2.twitch.tv/AT-cm%7C"+url+".mp4", @"C:\Users\juuce\Desktop\download\teste.mp4");
            //}
            var uri = new Uri(@"C:\Users\juuce\Desktop\download\teste.mp4");
            myMediaElement.Source = uri;
            myMediaElement.Play();

        }

    }
}
