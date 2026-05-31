using Model.Core.Abstract;

namespace Model.Core.Balls
{
    public class HeavyBall : BallBase
    {
        public HeavyBall()
        {
            Speed = 4;
            InitialSpeed = 1;
            Angle = 0;
            Radius = 11;
        }

        public override void Bounce(bool fromVertical)
        {
            base.Bounce(fromVertical);
            Speed = Speed * 0.98;
        }
    }
}
