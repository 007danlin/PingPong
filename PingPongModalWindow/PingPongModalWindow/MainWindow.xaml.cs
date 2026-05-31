using Microsoft.Win32;
using Model.Core;
using Model.Core.Abstract;
using Model.Core.Balls;
using Model.Core.Rackets;
using System;
using System.IO;
using System.Windows;
using static System.Windows.Forms.AxHost;
using Model.Data;

namespace PingPongModalWindow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContinueGameButton.IsEnabled = false;
        }

        // Включение кнпопки продолжить игру после паузы
        public MainWindow(string folderPath, string format) : this()
        {
            FolderPathBox.Text = folderPath;
            SaveManager saveManager = new SaveManager();
            if (saveManager.CanLoad(folderPath, format))
                ContinueGameButton.IsEnabled = true;
            else
                ContinueGameButton.IsEnabled = false;

            SaveFormatComboBox.SelectedIndex = format == "XML" ? 1 : 0;
        }

        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPathBox.Text = dialog.SelectedPath;
                string format = SaveFormatComboBox.SelectedIndex == 1 ? "XML" : "JSON";
       
                Model.Data.SaveManager saveManager = new Model.Data.SaveManager();
                ContinueGameButton.IsEnabled = saveManager.CanLoad(dialog.SelectedPath, format);
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
                case 2: return new FastBall();
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
            state.SaveFormat = SaveFormatComboBox.SelectedIndex == 1 ? "XML" : "JSON";
            Window1 gameWindow = new Window1(state);
            gameWindow.Show();
            this.Hide();
        }

        private void ContinueGameButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Data.SaveManager saveManager = new Model.Data.SaveManager();
            string format = SaveFormatComboBox.SelectedIndex == 1 ? "XML" : "JSON";
            GameState state = saveManager.Load(FolderPathBox.Text, format);
            if (state == null)
            {
                MessageBox.Show("Не удалось найти сохраненную игру");
                return;
            }
            state.Player1Racket = GetRacket(racket1ComboBox.SelectedIndex);
            state.Player2Racket = GetRacket(racket2ComboBox.SelectedIndex);
            state.Ball = GetBall(BallComboBox.SelectedIndex);
            state.SaveFolderPath = FolderPathBox.Text;
            state.SaveFormat = format;
            Window1 gameWindow = new Window1(state);
            gameWindow.Show();
            this.Hide();
        }
    }
}