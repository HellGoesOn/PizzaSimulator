﻿using Microsoft.Xna.Framework;

namespace PizzaSimulator.Content.Components
{
    public class Camera
    {
        public Camera()
        {
            Zoom = 3f;
        }

        public void Follow(Entity target)
        {
            var position = Matrix.CreateTranslation(new Vector3(
                -target.Position.X - (target.MyCollider.Width / 2),
                -target.Position.Y - (target.MyCollider.Height / 2),
                0)) * GetZoom();

            var offset = Matrix.CreateTranslation(
                ScreenManager.Instance.ScreenWidth / 2,
                ScreenManager.Instance.ScreenHeight / 2,
                0);

            Transform = position * offset;
        }

        public void SetPosition(Vector2 p)
        {
            var offset = Matrix.CreateTranslation(
                   ScreenManager.Instance.ScreenWidth / 2,
                   ScreenManager.Instance.ScreenHeight / 2,
                   0);

            var position = Matrix.CreateTranslation(-p.X, -p.Y, 0) * GetZoom();

            Transform = position * offset;
        }

        public Matrix GetZoom() => Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));

        public float Zoom { get; set; }

        public Matrix Transform { get; private set; }
    }
}
