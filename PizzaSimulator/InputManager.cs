using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;

namespace PizzaSimulator
{
    public static class InputManager
    {
        public static void Update()
        {
            bool foundTile = false;

            for (int i = 0; i < GameWorld.WORLD_WIDTH; i++)
            {
                for (int j = 0; j < GameWorld.WORLD_HEIGHT; j++)
                {
                    HighlightedTile = null;
                    if (GameLoop.World.GetTileBounds(i, j).Contains(MouseScreenPosition.ToPoint()))
                    {
                        HighlightedTile = GameLoop.World.GetTile(i, j);
                        foundTile = true;
                        break;
                    }
                }

                if (foundTile)
                    break;
            }

            if (HasRightClicked)
                Customer.SpawnCustomer(MouseScreenPosition);

            if (HasLeftClicked)
            {
                for (int i = EntityManager.Instance.Entities.Count - 1; i >= 0; i--)
                {
                    if (EntityManager.Instance.Entities[i].Highlighted)
                        Customer.KillCustomer(EntityManager.Instance.Entities[i]);
                }
            }

            if (IsKeyPressed(Keys.R))
                GameLoop.Instance.CommitApocalypse();

            if (oldScrollValue > CurrentMouseState.ScrollWheelValue)
                CameraManager.Camera.TryZoom(-0.25f);
            else if (oldScrollValue < CurrentMouseState.ScrollWheelValue)
                CameraManager.Camera.TryZoom(0.25f);

            if (HasLeftClicked)
            {
                if (HighlightedTile != null)
                {
                    int x = HighlightedTile.Coordinates.X;
                    int y = HighlightedTile.Coordinates.Y;
                    GameLoop.World.SetTile(new FloorTile(new TileCoordinates(x, y)));
                }
            }

            float cameraSpeed = 5f;

            if (IsKeyHeld(Keys.Left))
                CameraManager.Camera.MoveBy(new Vector2(-cameraSpeed, 0));

            if (IsKeyHeld(Keys.Right))
                CameraManager.Camera.MoveBy(new Vector2(cameraSpeed, 0));

            if (IsKeyHeld(Keys.Up))
                CameraManager.Camera.MoveBy(new Vector2(0, -cameraSpeed));

            if (IsKeyHeld(Keys.Down))
                CameraManager.Camera.MoveBy(new Vector2(0, cameraSpeed));

            OldMouseState = Mouse.GetState();
            OldKeyboardState = Keyboard.GetState();
            oldScrollValue = CurrentMouseState.ScrollWheelValue;
        }

        public static void DrawInfo(SpriteBatch spriteBatch)
        {
            if(HighlightedTile != null)
            spriteBatch.DrawString(Assets.DefaultFont, HighlightedTile.ToString(), new Vector2(20, 40), Color.White);
        }

        public static bool HasLeftClicked =>
            CurrentMouseState.LeftButton == ButtonState.Pressed
            && OldMouseState.LeftButton == ButtonState.Released;

        public static bool HasRightClicked =>
            CurrentMouseState.RightButton == ButtonState.Pressed
            && OldMouseState.RightButton == ButtonState.Released;

        public static bool IsKeyHeld(Keys k) => Keyboard.GetState().IsKeyDown(k);

        public static bool IsKeyPressed(Keys k) => CurrentKeyboardState.IsKeyDown(k) && !OldKeyboardState.IsKeyDown(k);

        public static MouseState CurrentMouseState => Mouse.GetState();
        public static MouseState OldMouseState { get; private set; }

        public static KeyboardState CurrentKeyboardState => Keyboard.GetState();
        public static KeyboardState OldKeyboardState { get; private set; }

        public static Vector2 MouseScreenPosition
        {
            get
            {
                return ScreenToWorldSpace(Mouse.GetState().Position.ToVector2());
            }
        }

        public static Vector2 MouseWorldPosition => CurrentMouseState.Position.ToVector2();

        public static Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(CameraManager.Camera.Transform);
            return Vector2.Transform(point, invertedMatrix);
        }

        public static Tile HighlightedTile { get; set; }

        private static float oldScrollValue;
    }
}
