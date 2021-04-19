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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ScreenManager.Instance.Load();

            Customer.SpawnCustomer(new Vector2(150, 150));

            base.LoadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Instance.SetDeltaTime(gameTime);

            EntityManager.Instance.Update();

            //CameraManager.Camera.Follow(EntityManager.Instance.Entities[0]);

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;
            // Entity draw
            sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);

            foreach(Entity e in EntityManager.Instance.Entities)
                e.Draw(sb);

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
