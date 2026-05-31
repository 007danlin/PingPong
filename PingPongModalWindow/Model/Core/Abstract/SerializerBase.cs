using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class SerializerBase
    {
        public abstract void Serialize(GameState state, string folderPath);

        public abstract GameState Deserialize(string folderPath);

        public abstract bool CanLoad(string folderPath);
    }
}
