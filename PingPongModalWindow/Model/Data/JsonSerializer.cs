using Model.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Model.Data
{
    public class JsonSerializer
    {
        public void Serialize(GameState state, string folderPath)
        {
            string filePath = Path.Combine(folderPath, "pingpong.json");

            JObject jObject = new JObject()
            {
                [nameof(GameState.ScorePlayer1)] = state.ScorePlayer1,
                [nameof(GameState.ScorePlayer2)] = state.ScorePlayer2,
                [nameof(GameState.MaxScore)] = state.MaxScore,
                [nameof(GameState.IsMultiplayer)] = state.IsMultiplayer,
                [nameof(GameState.Player1Name)] = state.Player1Name,
                [nameof(GameState.Player2Name)] = state.Player2Name,
                [nameof(GameState.ServerNumber)] = state.ServerNumber,
            };

            File.WriteAllText(filePath, jObject.ToString());
        }

        public GameState Deserialize(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "pingpong.json");

            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            JObject jObject = JObject.Parse(json);

            GameState state = new GameState();

            state.ScorePlayer1 = jObject[nameof(GameState.ScorePlayer1)].Value<int>();
            state.ScorePlayer2 = jObject[nameof(GameState.ScorePlayer2)].Value<int>();
            state.MaxScore = jObject[nameof(GameState.MaxScore)].Value<int>();
            state.IsMultiplayer = jObject[nameof(GameState.IsMultiplayer)].Value<bool>();
            state.Player1Name = jObject[nameof(GameState.Player1Name)].Value<string>();
            state.Player2Name = jObject[nameof(GameState.Player2Name)].Value<string>();
            state.ServerNumber = jObject[nameof(GameState.ServerNumber)].Value<int>();

            return state;
        }
    }
}
