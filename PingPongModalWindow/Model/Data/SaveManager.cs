using Model.Core;
using System.IO;

namespace Model.Data
{
    public class SaveManager
    {
        private JsonSerializer _jsonSerializer = new JsonSerializer();
        private XmlSerializer _xmlSerializer = new XmlSerializer();

        public void Save(GameState state, string folderPath, string format = "JSON")
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (format == "XML")
                _xmlSerializer.Serialize(state, folderPath);
            else
                _jsonSerializer.Serialize(state, folderPath);
        }

        public GameState Load(string folderPath, string format = "JSON")
        {
            if (format == "XML")
                return _xmlSerializer.Deserialize(folderPath);
            else
                return _jsonSerializer.Deserialize(folderPath);
        }

        public bool CanLoad(string folderPath, string format = "JSON")
        {
            if (format == "XML")
                return _xmlSerializer.CanLoad(folderPath);
            else
            {
                string filePath = Path.Combine(folderPath, "pingpong.json");
                return File.Exists(filePath);
            }
        }
    }
}