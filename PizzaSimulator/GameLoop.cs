using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.UI;
using PizzaSimulator.Content.UI.Elements;
using PizzaSimulator.Content.World;
using System;

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
            base.Initialize();

            ScreenManager.Instance.SetScreenSize(1280, 720);

            UIManager.Instance.Initialize();


            UIStats stats = new UIStats();

            UIManager.Instance.AddElement(stats);
        }

        protected override void LoadContent()
        {
            ScreenManager.Instance.Load();

            base.LoadContent();

            Loader.Load();

            Assets.DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

            World = new GameWorld();

            CameraManager.Camera.Position =
                new Vector2(World.WidthInPixels / 2 - ScreenManager.Instance.ScreenWidth / 2 - Tile.WIDTH / 2,
                World.HeightInPixels / 2 - ScreenManager.Instance.ScreenHeight / 2 - Tile.HEIGHT / 2);

            Mouse.SetPosition(ScreenManager.Instance.ScreenWidth / 2, ScreenManager.Instance.ScreenHeight / 2);

            MyPlayer = new Player();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Instance.SetDeltaTime(gameTime);

            EntityManager.Instance.Update();

            foreach (Entity e in EntityManager.Instance.Entities)
            {
                e.Update();
            }

            MyPlayer.Update();

            UIManager.Instance.UpdateInterfaces();

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Turquoise);

            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;

            // World render
            sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);
            World.DrawWorld(sb);
            sb.End();

            // Entity draw
            sb.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);

            foreach(Entity e in EntityManager.Instance.Entities)
                e.Draw(sb);

            sb.End();

            // UI Draw
            sb.Begin();

            UIManager.Instance.DrawInterfaces();

            sb.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            EntityManager.Dispose();
            ScreenManager.Dispose();
            RNGMachine.Dispose();

            Loader.Unload();

            Assets.DefaultFont = null;
        }

        public void CommitApocalypse()
        {
            World = new GameWorld();
            //EntityManager.Instance.Entities.Clear();
        }

        public static Player MyPlayer { get; private set; }

        public static GameWorld World { get; private set; }

        public static GameLoop Instance { get; private set; }
    }
}
