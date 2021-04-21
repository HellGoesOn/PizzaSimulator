using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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

        public static SoundEffect LoadSound(string path)
        {
            return GameLoop.Instance.Content.Load<SoundEffect>("Assets/" + path);
        }

        public static void Load()
        {
            foreach(PropertyInfo property in typeof(Assets).GetProperties())
            {
                TextureAttribute txAtr = (TextureAttribute)property.GetCustomAttribute(typeof(TextureAttribute));

                SoundEffectAttribute sfxAtr = (SoundEffectAttribute)property.GetCustomAttribute(typeof(SoundEffectAttribute));

                if(txAtr != null)
                    property.SetValue(null, LoadTexture(txAtr.Path));

                if(sfxAtr != null)
                    property.SetValue(null, LoadSound(sfxAtr.Path));
            }
        }

        public static void Unload()
        {
            foreach (PropertyInfo property in typeof(Assets).GetProperties())
            {
                TextureAttribute attr = (TextureAttribute)property.GetCustomAttribute(typeof(TextureAttribute));

                SoundEffectAttribute sfxAtr = (SoundEffectAttribute)property.GetCustomAttribute(typeof(SoundEffectAttribute));

                if (attr != null)
                    property.SetValue(null, null);

                if (sfxAtr != null)
                    property.SetValue(null, LoadSound(sfxAtr.Path));
            }
        }
    }
}
