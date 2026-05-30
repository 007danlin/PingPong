using Microsoft.Win32;
using Model.Core;
using Model.Core.Abstract;
using Model.Core.Balls;
using Model.Core.Rackets;
using System;
using System.IO;
using System.Windows;
using static System.Windows.Forms.AxHost;

namespace PingPongModalWindow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContinueGameButton.IsEnabled = false;
        }

        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPathBox.Text = dialog.SelectedPath;
                string savePath = Path.Combine(dialog.SelectedPath, "pingpong.json");
                ContinueGameButton.IsEnabled = File.Exists(savePath);
            }
        }

        private RacketBase GetRacket(int index)
        {
            switch (index)
            {
                case 0: return new WoodenRacket();
                case 1: return new LeatherRacket();
                case 2: return new PolyurethaneRacket();
                default: return new WoodenRacket();
            }
        }

        private BallBase GetBall(int index)
        {
            switch (index)
            {
                case 0: return new StandardBall();
                case 1: return new HeavyBall();
                default: return new StandardBall();
            }
        }

        private GameState BuildGameState()
        {
            GameState state = new GameState();
            state.Player1Racket = GetRacket(racket1ComboBox.SelectedIndex);
            state.Player2Racket = GetRacket(racket2ComboBox.SelectedIndex);
            state.Ball = GetBall(BallComboBox.SelectedIndex);
            state.MaxScore = pointsComboBox.SelectedIndex == 0 ? 11 : 21;
            state.IsMultiplayer = playersComboBox.SelectedIndex == 1;
            state.SaveFolderPath = FolderPathBox.Text;
            return state;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameState state = BuildGameState();
            Window1 gameWindow = new Window1(state);
            gameWindow.Show();
            this.Hide();
        }

        private void ContinueGameButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Data.SaveManager saveManager = new Model.Data.SaveManager();

            GameState state = saveManager.Load(FolderPathBox.Text);

            if (state == null)
            {
                MessageBox.Show("Не удалось найти сохраненную игру");
                return;
            }

            state.Player1Racket = GetRacket(racket1ComboBox.SelectedIndex);
            state.Player2Racket = GetRacket(racket2ComboBox.SelectedIndex);
            state.Ball = GetBall(BallComboBox.SelectedIndex);
            state.SaveFolderPath = FolderPathBox.Text;

            Window1 gameWindow = new Window1(state);
            gameWindow.Show();
            this.Hide();
        }
    }
}