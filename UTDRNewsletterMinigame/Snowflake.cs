using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static UTDRNewsletterMinigame.Sprite;

namespace UTDRNewsletterMinigame
{
    internal class Snowflake
    {
        private static readonly Sprite sprite = LoadSprite("resources/sprites/snowflake.png", new Vector2(2, 2));

        private static Random random = new Random();

        private int newDirCounter = 0;
        private int moveDir = 0;
        private int moveCounter = 0;
        public Vector2 pos;

        public Snowflake(Vector2 pos)
        {
            this.pos = pos;
        }

        public void Think()
        {
            newDirCounter--;

            if (newDirCounter <= 0)
            {
                moveDir = random.Next(-1, 1);
                newDirCounter = random.Next(15, 60);
            }

            moveCounter++;

            if (moveCounter >= 5)
            {
                pos.X += moveDir * 3;
                moveCounter = 0;
            }

            pos.Y += 3f;
        }

        public void Draw()
        {
            sprite.Draw(pos, 0f, 3f, Color.White);
        }

        public bool CheckHitUmbrella()
        {
            Vector2 boundsScale = new Vector2(2f, 2f) * 3f;
            BoundingBox2D a = new BoundingBox2D(pos - boundsScale, pos + boundsScale);
            BoundingBox2D b = Program.player!.GetUmbrellaBounds();
            return BoundingBox2D.CheckTouching(a, b);
        }
    }
}
