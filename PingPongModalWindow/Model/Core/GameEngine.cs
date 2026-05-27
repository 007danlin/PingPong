using Model.Core.Abstract;
using Model.Core.Interfaces;
using System;
using System.Timers;

namespace Model.Core
{
    public partial class GameEngine
    {
        private IGameField _field;
        private BallBase _ball;
        private IRacket _player1Racket;
        private IRacket _player2Racket;
        private Timer _timer;
        public bool IsMultiplayer { get; set; }

        public int ScorePlayer1 { get; private set; }
        public int ScorePlayer2 { get; private set; }
        public bool IsRunning { get; private set; }
        public bool BallInPlay { get; private set; }

        // Делегат отвечает за забитое очко
        public Action<int, int> OnScoreChanged { get; set; }
        // Делегат отвечает за завершение игры при победе одного из игроков
        public Action<string> OnGameOver { get; set; }

        public GameEngine(IGameField field, BallBase ball, IRacket player1, IRacket player2)
        {
            _field = field;
            _ball = ball;
            _player1Racket = player1;
            _player2Racket = player2;
            // типо счетчика fps
            _timer = new Timer(16);
            _timer.Elapsed += Update;
            // перезапуск
            _timer.AutoReset = true;
        }

        public void Start()
        {
            IsRunning = true;
            BallInPlay = false;
            _timer.Start();
        }

        public void Stop()
        {
            IsRunning = false;
            _timer.Stop();
        }

        public void Serve()
        {
            if (!BallInPlay)
            {
                _ball.X = _field.Width / 2.0;
                _ball.Y = _field.Height / 2.0;
                _ball.Angle = 45;
                _ball.Speed = 5;
                BallInPlay = true;
            }
        }

        private void Update(object sender, ElapsedEventArgs e)
        {
            if (!BallInPlay) return;
            MoveBall();
            CheckWallCollision();
            CheckRacketCollision();
            CheckOutOfBounds();
            if (!IsMultiplayer && BallInPlay)
                MoveBot();
        }



        private void MoveBall()
        {
            double rad = _ball.Angle * Math.PI / 180.0;
            _ball.X += _ball.Speed * Math.Cos(rad);
            _ball.Y += _ball.Speed * Math.Sin(rad);
        }

        private void MoveBot()
        {
            double botCenter = _player2Racket.Y + _player2Racket.Height / 2.0;
            double ballCenter = _ball.Y + _ball.Radius;
            double botSpeed = 3.5;

            if (ballCenter < botCenter - 5)
                _player2Racket.Move(-botSpeed);
            else if (ballCenter > botCenter + 5)
                _player2Racket.Move(botSpeed);

            if (_player2Racket.Y < 0)
                _player2Racket.Y = 0;
            if (_player2Racket.Y + _player2Racket.Height > _field.Height)
                _player2Racket.Y = _field.Height - _player2Racket.Height;
        }

        private void CheckWallCollision()
        {
            if (_ball.Y <= 0)
            {
                _ball.Y = 0;
                _ball.Bounce(false);
            }
            if (_ball.Y >= _field.Height - _ball.Radius * 2)
            {
                _ball.Y = _field.Height - _ball.Radius * 2;
                _ball.Bounce(false);
            }
        }

        private void CheckRacketCollision()
        {
            if (_ball.X <= _player1Racket.X + _player1Racket.Width &&
                _ball.X >= _player1Racket.X &&
                _ball.Y + _ball.Radius * 2 >= _player1Racket.Y &&
                _ball.Y <= _player1Racket.Y + _player1Racket.Height)
            {
                _ball.X = _player1Racket.X + _player1Racket.Width;
                _ball.Bounce(true);
                _ball.Speed += _player1Racket.Power * 0.05;
            }

            if (_ball.X + _ball.Radius * 2 >= _player2Racket.X &&
                _ball.X <= _player2Racket.X + _player2Racket.Width &&
                _ball.Y + _ball.Radius * 2 >= _player2Racket.Y &&
                _ball.Y <= _player2Racket.Y + _player2Racket.Height)
            {
                _ball.X = _player2Racket.X - _ball.Radius * 2;
                _ball.Bounce(true);
                _ball.Speed += _player2Racket.Power * 0.05;
            }
        }

        private void CheckOutOfBounds()
        {
            if (_ball.X < 0)
            {
                ScorePlayer2++;
                BallInPlay = false;
                _ball.X = _field.Width / 2.0;
                _ball.Y = _field.Height / 2.0;
                OnScoreChanged?.Invoke(ScorePlayer1, ScorePlayer2);
                CheckVictory();
            }
            else if (_ball.X > _field.Width)
            {
                ScorePlayer1++;
                BallInPlay = false;
                _ball.X = _field.Width / 2.0;
                _ball.Y = _field.Height / 2.0;
                OnScoreChanged?.Invoke(ScorePlayer1, ScorePlayer2);
                CheckVictory();
            }
        }
    }
}