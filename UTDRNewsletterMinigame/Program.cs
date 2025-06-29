using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace UTDRNewsletterMinigame
{
    internal class Program
    {
        public const int ScrW = 640;
        public const int ScrH = 480;

        public const int Framerate = 30;

        public static Player? player = null;
        public static int frameNum = 0;

        static void Main(string[] args)
        {
            InitWindow(ScrW, ScrH, "UT Holiday 2024 Minigame");

            player = new Player();
            List<Snowflake> snowflakes = new List<Snowflake>();
            Random random = new Random();

            int score = 0;

            SetTargetFPS(Framerate);

            while (!WindowShouldClose())
            {
                if ((frameNum % (Framerate * 2)) == 0)
                {
                    int pos = random.Next(5 * 3, ScrW - 5 * 3);

                    snowflakes.Add(new Snowflake(new System.Numerics.Vector2(pos, -10f)));
                }

                player.Think();

                for (int i = snowflakes.Count - 1; i >= 0; i--)
                {
                    Snowflake snowflake = snowflakes[i];

                    snowflake.Think();

                    if (snowflake.CheckHitUmbrella())
                    {
                        score++;
                        snowflakes.RemoveAt(i);
                        // Couldn't get a custom UT font working so I'm settling for displaying score in the window title.
                        SetWindowTitle($"UT Holiday 2024 Minigame (Score: {score})");
                        continue;
                    }

                    if (snowflake.pos.Y > player.pos.Y)
                    {
                        snowflakes.RemoveAt(i);
                        continue;
                    }
                }

                BeginDrawing();
                ClearBackground(Color.White);

                player.DrawTrails();

                foreach (Snowflake snowflake in snowflakes)
                {
                    snowflake.Draw();
                }

                player.Draw();

                EndDrawing();

                frameNum++;
            }

            CloseWindow();
        }
    }
}
