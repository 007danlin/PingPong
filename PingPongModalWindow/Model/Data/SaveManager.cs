using Model.Core;
using System.IO;

namespace Model.Data
{
    public class SaveManager
    {
        private SerializerBase _jsonSerializer = new JsonSerializer();
        private SerializerBase _xmlSerializer = new XmlSerializer();

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
                return _jsonSerializer.CanLoad(folderPath);
        }

        public void ChangeFormat(string folderPath, string oldFormat, string newFormat)
        {
            if (oldFormat == newFormat)
                return;

            if (!CanLoad(folderPath, oldFormat))
                return;

            GameState state = Load(folderPath, oldFormat);

            Save(state, folderPath, newFormat);
        }
    }
}