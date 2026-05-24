using Model.Core.Abstract;

namespace Model.Core.Balls
{
    public class StandardBall : BallBase
    {
        public StandardBall()
        {
            Speed = 5;
            Angle = 0;
            Radius = 9;
        }
    }
}
