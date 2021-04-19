using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace PizzaSimulator
{
    public class Loader
    {
        public static Texture2D LoadTexture(string path)
        {
            Texture2D texture;

            FileStream str = new FileStream("Content/Assets/Sprites/" + path + ".png", FileMode.Open);

            texture = Texture2D.FromStream(ScreenManager.Instance.Graphics.GraphicsDevice, str);

            str.Dispose();

            if (texture != null)
                return texture;

            throw new Exception("Texture load attempt failed");
        }
    }
}
