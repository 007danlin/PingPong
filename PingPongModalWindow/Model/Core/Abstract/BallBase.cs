using Model.Core.Interfaces;

namespace Model.Core.Abstract
{
    public abstract class BallBase : IBall
    {
        private double _radius = 9;

        public virtual double Speed { get; set; }
        public virtual double Angle { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public double Radius
        {
            get 
            { 
                return _radius; 
            }
            protected set 
            { 
                _radius = value; 
            }
        }

        public virtual void Bounce(bool fromVertical)
        {
            if (fromVertical)
                Angle = 180 - Angle;
            else
                Angle = -Angle;
        }
    }
}
