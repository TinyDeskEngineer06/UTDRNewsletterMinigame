using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace UTDRNewsletterMinigame
{
    internal class Sprite
    {
        public readonly Texture2D texture;
        public readonly Vector2 origin;

        public Sprite(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;
        }

        public static Sprite LoadSprite(string texturePath, Vector2 origin)
        {
            return new Sprite(LoadTexture(texturePath), origin);
        }

        public void Draw(Vector2 position, float rotation, float scale, Color tint)
        {
            DrawTextureEx(texture, position - origin * scale, rotation, scale, tint);
        }
    }
}
