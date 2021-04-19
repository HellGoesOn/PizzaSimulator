using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Components
{
    public class Collider
    {
        public Collider(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Hitbox => GetHibox(Position); 

        public Rectangle GetHibox(Vector2 pos) => new Rectangle((int)pos.X, (int)pos.Y, Width, Height);

        public Vector2 Center
        {
            get => Position + new Vector2(Width * 0.5f, Height * 0.5f);
            set => Position = value + new Vector2(Width * 0.5f, Height * 0.5f);
        }
    }
}
