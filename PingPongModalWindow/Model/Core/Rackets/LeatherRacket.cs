using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core.Abstract;

namespace Model.Core.Rackets
{
    public class LeatherRacket : RacketBase
    {
        public override int Power => 7;
        public override int Spin => 6;
        public override int ServePower => 3;
    }
}
