using Model.Core.Abstract;

namespace Model.Core
{
    public partial class GameState
    {
        public BallBase Ball { get; set; }
        public RacketBase Player1Racket { get; set; }
        public RacketBase Player2Racket { get; set; }
        public int ScorePlayer1 { get; set; }
        public int ScorePlayer2 { get; set; }
        public int MaxScore { get; set; }
        public string SaveFolderPath { get; set; }
        public bool IsMultiplayer { get; set; }
        public int ServerNumber { get; set; }

        public GameState()
        {
            ScorePlayer1 = 0;
            ScorePlayer2 = 0;
            MaxScore = 11;
        }

        // Перегрузка оператора
        public static GameState operator +(GameState a, GameState b)
        {
            return new GameState
            {
                ScorePlayer1 = a.ScorePlayer1 + b.ScorePlayer1,
                ScorePlayer2 = a.ScorePlayer2 + b.ScorePlayer2,
                MaxScore = a.MaxScore
            };
        }

        // Перегрузка оператора
        public static bool operator ==(GameState a, GameState b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.ScorePlayer1 == b.ScorePlayer1 &&
                   a.ScorePlayer2 == b.ScorePlayer2;
        }

        // Перегрузка оператора
        public static bool operator !=(GameState a, GameState b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is GameState other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return ScorePlayer1.GetHashCode() ^ ScorePlayer2.GetHashCode();
        }
    }
}
