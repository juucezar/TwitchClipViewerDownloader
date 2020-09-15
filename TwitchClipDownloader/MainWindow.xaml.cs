using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchClipDownloader.Games;

namespace TwitchClipDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Authentication authentication = new Authentication();
        public  MainWindow()
        {
            authentication = Authenticate();  
            InitializeComponent();
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

        private void btnTopJogos_Click(object sender, RoutedEventArgs e)
        {
            GamesView gamesView = new GamesView(authentication);
            gamesView.Show();
        }
    }
}
