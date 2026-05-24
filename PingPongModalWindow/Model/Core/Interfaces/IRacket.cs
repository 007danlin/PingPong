using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Interfaces
{
    public interface IRacket
    {
        int Power { get; }
        int ServePower { get; }
        int Spin { get; }

        double X { get; set; }
        double Y { get; set; }

        double Width { get; }
        double Height { get; }

        void Move(double dy);
    }
}
