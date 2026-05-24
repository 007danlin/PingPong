using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Interfaces
{
    public interface IBall
    {
        double Speed { get; set; }
        double Angle { get; set; }

        double X { get; set; }
        double Y { get; set; }

        double Radius { get; }

        void Bounce(bool fromVertical);
    }
}
