using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core.Abstract;

namespace Model.Core.Rackets
{
    public class PolyurethaneRacket : RacketBase
    {
        public override int Power => 9;
        public override int Spin => 8;
        public override int ServePower => 5;
    }
}
