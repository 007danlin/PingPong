using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Interfaces
{
    public interface IGameField
    {
        int Width { get; set; }
        int Height { get; set; }
    }
}
