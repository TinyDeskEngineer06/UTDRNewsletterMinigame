using System;
using System.Numerics;

namespace UTDRNewsletterMinigame
{
    internal class BoundingBox2D
    {
        public Vector2 min, max;

        public BoundingBox2D(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public static bool CheckTouching(BoundingBox2D a, BoundingBox2D b)
        {
            return (
                a.min.X <= b.max.X &&
                a.max.X >= b.min.X &&
                a.min.Y <= b.max.Y &&
                a.max.Y >= b.min.Y
            );
        }
    }
}
