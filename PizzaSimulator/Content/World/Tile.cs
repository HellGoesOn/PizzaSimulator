using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Enums;

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
            return $"Tile: {GetType().Name} at " + Coordinates;
        }

        public void TryAddSubtile(SubTile tile, SubTileOrientation orientnation)
        {
            switch(orientnation)
            {
                case SubTileOrientation.TopLeft:
                    SubTiles[0, 0] = tile;
                    break;
                case SubTileOrientation.TopRight:
                    SubTiles[1, 0] = tile;
                    break;
                case SubTileOrientation.BottomLeft:
                    SubTiles[0, 1] = tile;
                    break;
                case SubTileOrientation.BottomRight:
                    SubTiles[1, 1] = tile;
                    break;
            }
        }
        
        public TileCoordinates Coordinates { get; set; }

        public Texture2D Texture { get; }
    }
}
