using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;

namespace PizzaSimulator
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public GraphicsDeviceManager Graphics => graphicsDeviceManager;

        public void SetGraphicsDeviceManager(Game game) => graphicsDeviceManager = new GraphicsDeviceManager(game);

        public SpriteBatch SpriteBatch { get; private set; }

        public void Load()
        {
            SpriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        public void SetScreenSize(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;

            ScreenWidth = width;
            ScreenHeight = height;

            Graphics.ApplyChanges();
        }

        public int ScreenWidth { get; set; }

        public int ScreenHeight { get; set; }
    }
}
