using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Interfaces;
using PizzaSimulator.Content.Components.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.World
{
    public abstract class Tile
    {
        protected Tile(Texture2D texture, TileCoordinates coords)
        {
            Texture = texture;
            Coordinates = coords;
        }

        public const int WIDTH = 32, HEIGHT = 32;

        public SubTile[,] SubTiles { get; } = new SubTile[2, 2];

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);

            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    SubTile subTile = SubTiles[i, j];

                    if (subTile != null)
                        subTile.Draw(spriteBatch, position + new Vector2(i * SubTile.WIDTH, j * SubTile.HEIGHT));
                }
            }

        }

        public override string ToString()
        {
            return $"{GetType()}: {Coordinates.X};{Coordinates.Y}";
        }
        
        public TileCoordinates Coordinates { get; set; }

        public Texture2D Texture { get; }
    }
}
