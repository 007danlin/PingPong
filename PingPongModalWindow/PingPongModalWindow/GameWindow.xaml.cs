using Model.Core;
using Model.Core.Abstract;
using Model.Core.Interfaces;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;

namespace PingPongModalWindow
{
    public partial class Window1 : Window
    {
        private GameState _state;
        private GameEngine _engine;
        private DispatcherTimer _renderTimer;
        private HashSet<Key> _pressedKeys = new HashSet<Key>();

        private const double RacketSpeed = 5;
        private const double FieldWidth = 740;
        private const double FieldHeight = 286;

        public Window1() : this(new GameState()
        {
            Player1Racket = new Model.Core.Rackets.WoodenRacket(),
            Player2Racket = new Model.Core.Rackets.WoodenRacket(),
            Ball = new Model.Core.Balls.StandardBall()
        })
        {
        }


        public Window1(GameState state)
        {
            InitializeComponent();
            _state = state;
            InitGame();

            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.Closing += OnWindowClosing;
        }

        private void InitGame()
        {
            _state.Player1Racket.X = 30;
            _state.Player1Racket.Y = 108;
            _state.Player2Racket.X = 720;
            _state.Player2Racket.Y = 108;
            _state.Ball.X = 375;
            _state.Ball.Y = 134;

            _engine = new GameEngine(
                new Model.Core.GameField(),
                _state.Ball,
                _state.Player1Racket,
                _state.Player2Racket
            );

            _engine.MaxScore = _state.MaxScore;
            _engine.IsMultiplayer = _state.IsMultiplayer;

            _engine.OnScoreChanged = (s1, s2) =>
            {
                Dispatcher.Invoke(() =>
                {
                    tbPlayer1.Text = $"Игрок 1: {s1}";
                    tbPlayer2.Text = $"Игрок 2: {s2}";
                });
            };

            _engine.OnGameOver = (winner) =>
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"{winner} победил!");
                    _engine.ResetGame();
                    tbPlayer1.Text = "Игрок 1: 0";
                    tbPlayer2.Text = "Игрок 2: 0";
                });
            };

            _renderTimer = new DispatcherTimer();
            _renderTimer.Interval = TimeSpan.FromMilliseconds(16);
            _renderTimer.Tick += RenderFrame;
            _renderTimer.Start();

            _engine.Start();
        }

        private void RenderFrame(object sender, EventArgs e)
        {
            MoveRackets();

            if (_engine.ServerNumber == 1)
                tbServer.Text = "Подаёт: Игрок 1";
            else
                tbServer.Text = "Подаёт: Игрок 2";

            Canvas.SetLeft(leftRacket, _state.Player1Racket.X);
            Canvas.SetTop(leftRacket, _state.Player1Racket.Y);
            Canvas.SetLeft(rightRacket, _state.Player2Racket.X);
            Canvas.SetTop(rightRacket, _state.Player2Racket.Y);
            Canvas.SetLeft(ball, _state.Ball.X);
            Canvas.SetTop(ball, _state.Ball.Y);
        }

        private void MoveRackets()
        {
            if (_pressedKeys.Contains(Key.Up))
                _state.Player1Racket.Move(-RacketSpeed);
            if (_pressedKeys.Contains(Key.Down))
                _state.Player1Racket.Move(RacketSpeed);

            if (_state.IsMultiplayer)
            {
                if (_pressedKeys.Contains(Key.W))
                    _state.Player2Racket.Move(-RacketSpeed);
                if (_pressedKeys.Contains(Key.S))
                    _state.Player2Racket.Move(RacketSpeed);
            }

            ClampRacket(_state.Player1Racket);
            ClampRacket(_state.Player2Racket);
        }

        private void ClampRacket(IRacket racket)
        {
            if (racket.Y < 0) racket.Y = 0;
            if (racket.Y + racket.Height > FieldHeight)
                racket.Y = FieldHeight - racket.Height;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _pressedKeys.Add(e.Key);
            if (e.Key == Key.Space)
                _engine.Serve();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.Key);
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _engine.Stop();
            _renderTimer.Stop();

            Model.Data.SaveManager saveManager = new Model.Data.SaveManager();

            if (string.IsNullOrEmpty(_state.SaveFolderPath))
            {
                _state.SaveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"PingPongGame");
            }

            saveManager.Save(_state, _state.SaveFolderPath);
        }
    }
}