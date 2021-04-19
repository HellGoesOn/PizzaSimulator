using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Entities;

namespace PizzaSimulator
{
    public class GameLoop : Game
    {
        public GameLoop()
        {
            ScreenManager.Instance.SetGraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
        }

        protected override void Initialize()
        {
            ScreenManager.Instance.Initialize();
            this.Window.AllowUserResizing = true;
            base.Initialize();
            ScreenManager.Instance.Graphics.PreferredBackBufferWidth = 1280;
            ScreenManager.Instance.Graphics.PreferredBackBufferHeight = 720;
            ScreenManager.Instance.Graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            ScreenManager.Instance.Load();

            base.LoadContent();

            Loader.DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Instance.SetDeltaTime(gameTime);

            EntityManager.Instance.Update();

            CameraManager.Camera.SetPosition(new Vector2(100, 100));

            foreach (Entity e in EntityManager.Instance.Entities)
            {
                e.Update();
            }

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Turquoise);

            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;
            // Entity draw
            sb.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);

            foreach(Entity e in EntityManager.Instance.Entities)
                e.Draw(sb);

            sb.End();

            sb.Begin();

            EntityManager.Instance.DrawInfo(sb);

            sb.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            EntityManager.Dispose();
            ScreenManager.Dispose();
            RNGMachine.Dispose();
        }

        public static GameLoop Instance { get; private set; }
    }
}
