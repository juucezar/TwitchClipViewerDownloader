﻿using System;
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
        public  MainWindow()
        {
            Authentication authentication = Authenticate();          


            InitializeComponent();

            //Game game = new Game();
            //Task taskGame = new Task(async () => await game.GetGameByName("Overwatch"));
            //taskGame.Start();
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
            GamesView gamesView = new GamesView();
            gamesView.Show();
        }
    }
}
