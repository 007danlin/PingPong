using Model.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class SaveManager
    {
        private JsonSerializer jsonSerializer = new JsonSerializer();

        public void Save(GameState state, string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            jsonSerializer.Serialize(state, folderPath);
        }

        public GameState Load(string folderPath)
        {
            return jsonSerializer.Deserialize(folderPath);
        }

        public bool CanLoad(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "pingpong.json");

            return File.Exists(filePath);
        }
    }
}
