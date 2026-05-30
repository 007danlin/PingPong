namespace Model.Core
{
    public partial class GameState
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public bool Player1Ready { get; set; }
        public bool Player2Ready { get; set; }

        public void SetMultiplayer(string player1Name, string player2Name)
        {
            IsMultiplayer = true;
            Player1Name = player1Name;
            Player2Name = player2Name;
            Player1Ready = false;
            Player2Ready = false;
        }

        public bool BothPlayersReady()
        {
            return Player1Ready && Player2Ready;
        }

        public void SetPlayerReady(int playerNumber)
        {
            if (playerNumber == 1)
                Player1Ready = true;
            else if (playerNumber == 2)
                Player2Ready = true;
        }
        
        public string GetWinner()
        {
            if (ScorePlayer1 > ScorePlayer2)
                return Player1Name ?? "Игрок 1";
            else if (ScorePlayer2 > ScorePlayer1)
                return Player2Name ?? "Игрок 2";
            else
                return "Ничья";
        }
    }
}
