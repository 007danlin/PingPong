using Model.Core.Abstract;

namespace Model.Core.Balls
{
    public class FastBall : BallBase
    {
        public FastBall()
        {
            Speed = 7;
            InitialSpeed = 4;
            Angle = 0;
            Radius = 8;
        }

        public override void Bounce(bool fromVertical)
        {
            base.Bounce(fromVertical);
            Speed = Speed * 1.02;
        }
    }
}
