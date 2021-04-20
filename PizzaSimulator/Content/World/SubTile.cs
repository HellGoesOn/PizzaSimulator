using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.World
{
    public abstract class SubTile
    {
        public const int WIDTH = 16, HEIGHT = 16;

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // TO-DO: Implement this
        }
    }
}
