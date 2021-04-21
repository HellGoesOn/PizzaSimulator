using Microsoft.Xna.Framework.Audio;
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

        [Texture("Worker_Walk")]
        public static Texture2D Worker_Walk { get; set; }

        [Texture("Worker")]
        public static Texture2D Worker { get; set; }

        [Texture("GrassTile")]
        public static Texture2D GrassTile { get; set; }

        [Texture("FloorTile")]
        public static Texture2D FloorTile { get; set; }

        [Texture("WoodenFloorTile")]
        public static Texture2D WoodFloorTile { get; set; }

        [Texture("CashRegister")]
        public static Texture2D CashRegister { get; set; }

        [Texture("Wall")]
        public static Texture2D Wall { get; set; }

        [Texture("Pixel")]
        public static Texture2D Pixel { get; set; }

        [Texture("Pudge")]
        public static Texture2D Pudge { get; set; }

        [Texture("Logo")]
        public static Texture2D Logo { get; set; }

        [SoundEffect("Sounds/BKB")]
        public static SoundEffect BKB { get; set; }

        [Texture("BKB")]
        public static Texture2D BKBLogo { get; set; }

        [Texture("MainMenuBG")]
        public static Texture2D MainMenuBG { get; set; }

        [SoundEffect("Sounds/Build")]
        public static SoundEffect BuildSound { get; set; }

        public static SpriteFont DefaultFont { get; set; }
    }
}
