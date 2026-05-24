using Model.Core.Interfaces;

namespace Model.Core.Abstract
{
    public abstract class RacketBase : IRacket
    {
        private double _width = 12;
        private double _height = 70;

        public abstract int Power { get; }
        public abstract int Spin { get; }
        public abstract int ServePower { get; }
        public double X { get; set; }
        public double Y { get; set; }

        public double Width => _width;
        public double Height => _height;

        public virtual void Move(double dy)
        {
            Y += dy;
        }
    }
}
