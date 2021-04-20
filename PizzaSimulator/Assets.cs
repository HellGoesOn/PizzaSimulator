using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public static class Assets
    {
        [Texture("Customer")]
        public static Texture2D Customer { get; set; }

        [Texture("Customer_Walk")]
        public static Texture2D Customer_Walk { get; set; }

        [Texture("GrassTile")]
        public static Texture2D GrassTile { get; set; }

        [Texture("FloorTile")]
        public static Texture2D FloorTile { get; set; }

        [Texture("Walls")]
        public static Texture2D Walls { get; set; }

        public static SpriteFont DefaultFont { get; set; }
    }
}
