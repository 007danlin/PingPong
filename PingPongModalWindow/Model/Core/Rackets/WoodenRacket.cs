using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core.Abstract;

namespace Model.Core.Rackets
{
    public class WoodenRacket : RacketBase
    {
        public override int Power => 5;
        public override int Spin => 3;
        public override int ServePower => 2;
    }
}
