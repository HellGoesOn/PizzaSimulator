using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Attributes;
using System;
using System.IO;
using System.Reflection;

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

        public static void Load()
        {
            foreach(PropertyInfo property in typeof(Assets).GetProperties())
            {
                TextureAttribute attr = (TextureAttribute)property.GetCustomAttribute(typeof(TextureAttribute));

                if (attr == null)
                    continue;

                property.SetValue(null, LoadTexture(attr.Path));
            }
        }

        public static void Unload()
        {
            foreach (PropertyInfo property in typeof(Assets).GetProperties())
            {
                TextureAttribute attr = (TextureAttribute)property.GetCustomAttribute(typeof(TextureAttribute));

                if (attr == null)
                    continue;

                property.SetValue(null, null);
            }
        }
    }
}
