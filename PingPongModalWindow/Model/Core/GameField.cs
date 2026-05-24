using Model.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class GameField : IGameField
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public GameField()
        {
            Width = 760;
            Height = 286;
        }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
