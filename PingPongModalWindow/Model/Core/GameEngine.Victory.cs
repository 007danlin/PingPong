namespace Model.Core
{
    public partial class GameEngine
    {
        public int MaxScore { get; set; } = 11;

        private void CheckVictory()
        {
            if (ScorePlayer1 >= MaxScore && ScorePlayer1 - ScorePlayer2 >= 2)
            {
                Stop();
                OnGameOver?.Invoke("Игрок 1");
            }
            else if (ScorePlayer2 >= MaxScore && ScorePlayer2 - ScorePlayer1 >= 2)
            {
                Stop();
                OnGameOver?.Invoke("Игрок 2");
            }
        }

        public void ResetGame()
        {
            ScorePlayer1 = 0;
            ScorePlayer2 = 0;
            BallInPlay = false;
            _ball.X = _field.Width / 2.0;
            _ball.Y = _field.Height / 2.0;
        }
    }
}