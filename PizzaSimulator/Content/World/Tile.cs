using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Extensions;

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
                        subTile.Draw(spriteBatch, position);
                }
            }
        }

        public override string ToString()
        {
            return $"Tile: {GetType().Name} at " + Coordinates;
        }

        public void TryAddSubtile(SubTile tile)
        {
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (GetSubTileBounds(this.Coordinates.X * WIDTH, this.Coordinates.Y * HEIGHT).Contains(InputManager.MouseScreenPosition))
                        SubTiles[i, j] = tile;
                }
            }
        }


        public Rectangle GetSubTileBounds(int i, int j) => new Rectangle(i, j, SubTile.WIDTH, SubTile.HEIGHT);

        public TileCoordinates Coordinates { get; set; }

        public Texture2D Texture { get; }
    }
}
