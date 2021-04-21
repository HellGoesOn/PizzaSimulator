using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Song song;

        public GameLoop()
        {
            ScreenManager.Instance.SetGraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;

            UIManager.Instance.Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            ScreenManager.Instance.Load();

            ScreenManager.Instance.SetScreenSize(1280, 720);

            Mouse.SetPosition(ScreenManager.Instance.ScreenWidth / 2, ScreenManager.Instance.ScreenHeight / 2);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.song = Content.Load<Song>("Assets/Music/MainMenu");

            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.25f;

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            Loader.Load();

            Assets.DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

            CameraManager.Camera.Position =
                new Vector2(GameWorld.WidthInPixels / 2 - ScreenManager.Instance.ScreenWidth / 2 - Tile.WIDTH / 2,
                GameWorld.HeightInPixels / 2 - ScreenManager.Instance.ScreenHeight / 2 - Tile.HEIGHT / 2);
        }

        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            MediaPlayer.Volume = 0.25f;
            MediaPlayer.Play(song);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!this.IsActive)
                return;

            if(GameStateManager.Instance.CurrentGameState == GameState.None)
                GameStateManager.Instance.SwitchState(GameState.GameMenu);

            EntityManager.Instance.SetDeltaTime(gameTime);

            EntityManager.Instance.Update();

            foreach (Entity e in EntityManager.Instance.Entities)
            {
                e.Update();
            }

            MyPlayer?.Update();

            UIManager.Instance.UpdateInterfaces();

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;

            if (!this.IsActive)
            {
                MediaPlayer.Stop();
                return;
            }
            else
            {
                MediaPlayer.Resume();
            }

            // World render
            sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);
            World?.DrawWorld(sb);
            sb.End();

            // Entity draw
            sb.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Camera.Transform);

            foreach(Entity e in EntityManager.Instance.Entities)
                e.Draw(sb);

            sb.End();

            // UI Draw
            sb.Begin(samplerState: SamplerState.PointClamp);

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
        }

        public static Player MyPlayer { get; internal set; }

        public static GameWorld World { get; internal set; }

        public static GameLoop Instance { get; private set; }
    }
}
