using Model.Core;
using System.IO;
using System.Xml.Serialization;

namespace Model.Data
{
    public class XmlSerializer: SerializerBase
    {
        public override void Serialize(GameState state, string folderPath)
        {
            string filePath = Path.Combine(folderPath, "pingpong.xml");
            var dto = new GameStateDTO(state);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GameStateDTO));
            using (var writer = new StreamWriter(filePath))
                serializer.Serialize(writer, dto);
        }

        public override GameState Deserialize(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "pingpong.xml");
            if (!File.Exists(filePath))
                return null;
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GameStateDTO));
            using (var reader = new StreamReader(filePath))
            {
                var dto = (GameStateDTO)serializer.Deserialize(reader);
                return dto.ToGameState();
            }
        }

        public override bool CanLoad(string folderPath)
        {
            try
            {
                GameState state = Deserialize(folderPath);

                if (state == null)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public class GameStateDTO
        {
            public int ScorePlayer1 { get; set; }
            public int ScorePlayer2 { get; set; }
            public int MaxScore { get; set; }
            public bool IsMultiplayer { get; set; }
            public string Player1Name { get; set; }
            public string Player2Name { get; set; }
            public int ServerNumber { get; set; }

            public GameStateDTO() { }

            public GameStateDTO(GameState state)
            {
                ScorePlayer1 = state.ScorePlayer1;
                ScorePlayer2 = state.ScorePlayer2;
                MaxScore = state.MaxScore;
                IsMultiplayer = state.IsMultiplayer;
                Player1Name = state.Player1Name;
                Player2Name = state.Player2Name;
                ServerNumber = state.ServerNumber;
            }

            public GameState ToGameState()
            {
                GameState state = new GameState();
                state.ScorePlayer1 = ScorePlayer1;
                state.ScorePlayer2 = ScorePlayer2;
                state.MaxScore = MaxScore;
                state.IsMultiplayer = IsMultiplayer;
                state.Player1Name = Player1Name;
                state.Player2Name = Player2Name;
                state.ServerNumber = ServerNumber;
                return state;
            }
        }
    }
}
