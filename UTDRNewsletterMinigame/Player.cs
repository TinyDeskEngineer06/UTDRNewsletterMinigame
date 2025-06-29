using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static UTDRNewsletterMinigame.Sprite;

namespace UTDRNewsletterMinigame
{
    internal class Player
    {
        private static Sprite[,] sprites = new Sprite[2, 2]
        {
            { 
                LoadSprite("resources/sprites/frisk_left0.png", new Vector2(28f, 50f)), 
                LoadSprite("resources/sprites/frisk_left1.png", new Vector2(28f, 50f)) 
            },
            {
                LoadSprite("resources/sprites/frisk_right0.png", new Vector2(12f, 50f)),
                LoadSprite("resources/sprites/frisk_right1.png", new Vector2(12f, 50f))
            }
        };

        private static Sprite trailSprite = LoadSprite("resources/sprites/frisk_trail.png", new Vector2(9, 8));

        private struct TrailData
        {

            public Vector2 pos;
            public int frameCounter;

            public TrailData(Vector2 pos)
            {
                this.pos = pos;
            }
        }

        private List<TrailData> trails = new List<TrailData>();

        private int walkDir = 0;
        private int walkCycle = 0;
        private int walkCounter = 0;

        public Vector2 pos;

        public Player()
        {
            pos = new Vector2(Program.ScrW / 2f, 400f);
        }

        public void Think()
        {
            const float speed = 8f;

            if (IsKeyDown(KeyboardKey.A))
            {
                pos.X -= speed;
                walkDir = 0;
                walkCounter++;
            }
            else if (IsKeyDown(KeyboardKey.D))
            {
                pos.X += speed;
                walkDir = 1;
                walkCounter++;
            }

            if (walkCounter >= 4)
            {
                trails.Add(new TrailData(pos));

                walkCounter = 0;
                walkCycle++;
                if (walkCycle > 1) walkCycle = 0;
            }

            pos.X = Math.Clamp(pos.X, 30f, Program.ScrW - 30f);
        }

        public void Draw()
        {
            sprites[walkDir, walkCycle].Draw(pos, 0f, 3f, Color.White);
            BoundingBox2D bbox = GetUmbrellaBounds();
        }

        public void DrawTrails()
        {
            for (int i = trails.Count - 1; i >= 0; i--)
            {
                TrailData trail = trails[i];
                float trailAlpha = 1f - (trail.frameCounter / 10f);

                trailSprite.Draw(trail.pos, 0f, 3f, new Color(1f, 1f, 1f, trailAlpha));

                trail.frameCounter++;

                if (trail.frameCounter > 10)
                {
                    trails.RemoveAt(i);
                    continue;
                }

                trails[i] = trail;
            }
        }

        public BoundingBox2D GetUmbrellaBounds()
        {
            Vector2 origin = pos + new Vector2((walkDir == 0 ? -25f : -5f) * 3f, -40f * 3f);
            Vector2 size = new Vector2(29, 4) * 3f;

            return new BoundingBox2D(origin, origin + size);
        }
    }
}
