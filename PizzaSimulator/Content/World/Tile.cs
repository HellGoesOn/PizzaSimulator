using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using System;

namespace PizzaSimulator.Content.World
{
    public abstract class Tile
    {
        protected Tile(Texture2D texture, TileCoordinates coords)
        {
            Texture = texture;
            Coordinates = coords;
            SubTiles = new SubTile[2, 2];
        }

        public const int WIDTH = 32, HEIGHT = 32;

        public SubTile[,] SubTiles { get; set; }

        public bool HasSubtile(Type t)
        {
            foreach (SubTile tile in SubTiles)
                if (tile != null && tile.GetType() == t)
                    return true;

            return false;
        }

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
            return $"Tile: {GetType().Name} at " + Coordinates;
        }

        public void TryAddSubtile(SubTile subTile)
        {
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (GetSubTileBounds(i, j).Contains(InputManager.MouseScreenPosition))
                    {
                        SubTiles[i, j] = subTile;
                        return;
                    }
                }
            }
        }

        public static Vector2 GetCenter(Tile tile) => new Vector2(tile.Coordinates.X * WIDTH + WIDTH * 0.5f, tile.Coordinates.Y * HEIGHT + HEIGHT * 0.5f);

        public Rectangle GetSubTileBounds(int i, int j) => new Rectangle(Coordinates.X * WIDTH + i * SubTile.WIDTH, Coordinates.Y * HEIGHT + j * SubTile.HEIGHT, SubTile.WIDTH, SubTile.HEIGHT);

        public TileCoordinates Coordinates { get; set; }

        public Texture2D Texture { get; }
    }
}
