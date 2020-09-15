using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TwitchClipDownloader.Games
{
    /// <summary>
    /// Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GamesView : Window
    {
        private GamesModel gamesModel = new GamesModel();
        public GamesView(Authentication authentication)
        {
            
            InitializeComponent();

            gamesModel = Start(authentication);
            //datagridGames.ItemsSource = gamesModel.data;
            lbGames.ItemsSource = gamesModel.data;
        }

        private GamesModel Start(Authentication authentication)
        {
            Game game = new Game();
            Task<GamesModel> taskGame = Task.Factory.StartNew(() => game.GetTopGames(authentication));
            taskGame.Wait();           

            while (taskGame.IsCompleted)
            {
                foreach (var item in taskGame.Result.data)
                {
                    item.box_art_url = Regex.Replace(item.box_art_url, "{height}", "200", RegexOptions.IgnoreCase);
                    item.box_art_url = Regex.Replace(item.box_art_url, "{width}", "150", RegexOptions.IgnoreCase);                
                }
                return taskGame.Result;
            }
            return null;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var v = sender as GamesModel;
        }
    }
}
