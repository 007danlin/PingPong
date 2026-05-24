using Model.Core.Abstract;

namespace Model.Core
{
    public class Player
    {
        private int _score;

        public string Name { get; set; }
        public RacketBase Racket { get; set; }
        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
            }
        }

        public Player(string name, RacketBase racket)
        {
            Name = name;
            Racket = racket;
            Score = 0;
        }

        public void AddPoint()
        {
            Score++;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
