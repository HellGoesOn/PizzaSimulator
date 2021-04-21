using Microsoft.Xna.Framework;
using PizzaSimulator.Content.World;
using System;

namespace PizzaSimulator.Content.Components
{
    public class Camera
    {
        public Camera()
        {
            Zoom = 3f;
        }


        public Matrix Transform 
        { 
            get
            {
                var pos = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));

                var offset = Matrix.CreateTranslation(
                   ScreenManager.Instance.ScreenWidth / 2,
                   ScreenManager.Instance.ScreenHeight / 2,
                   0);

                return pos * GetZoom() * offset;
            }
        }

        public void TryZoom(float amount, Vector2 pos = default)
        {
            Zoom = Math.Clamp(Zoom + amount, 1f, 6f);

            if (pos != default)
                MoveBy(pos);
            else
                MoveBy(Vector2.Zero);
        }

        public void MoveBy(Vector2 by)
        {
            float offX = TargetView.Width / 2 / Zoom;
            float offY = TargetView.Height / 2 / Zoom;
            float x = Math.Clamp(Position.X + by.X, offX, GameWorld.WORLD_WIDTH * Tile.WIDTH - TargetView.Width / 2 / Zoom);
            float y = Math.Clamp(Position.Y + by.Y, offY, GameWorld.WORLD_HEIGHT * Tile.HEIGHT - TargetView.Height / 2 / Zoom);

            Position = new Vector2(x, y);
        }

        public Rectangle TargetView => new Rectangle((int)Position.X, (int)Position.Y, ScreenManager.Instance.ScreenWidth, ScreenManager.Instance.ScreenHeight);

        public Vector2 Position { get; set; }

        public Matrix GetZoom() => Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));

        public float Zoom { get; private set; }
    }
}
