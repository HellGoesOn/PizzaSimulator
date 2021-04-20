using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World.Tiles;

namespace PizzaSimulator.Content.World
{
    public class GameWorld
    {
        public const int WORLD_WIDTH = 60, WORLD_HEIGHT = 60;

        public GameWorld()
        {
            WorldRender = new RenderTarget2D(ScreenManager.Instance.Graphics.GraphicsDevice, Tile.WIDTH * WORLD_WIDTH, Tile.HEIGHT * WORLD_HEIGHT);

            TileGrid = new Tile[WORLD_WIDTH, WORLD_HEIGHT];

            for (int i = 0; i < WORLD_WIDTH; i++)
                for (int j = 0; j < WORLD_HEIGHT; j++)
                    TileGrid[i, j] = new GrassTile(new TileCoordinates(i, j));

            UpdateWorldRender();
        }

        public void DrawWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(WorldRender, Vector2.Zero, Color.White);
        }

        public void UpdateWorldRender()
        {
            ScreenManager.Instance.Graphics.GraphicsDevice.SetRenderTarget(WorldRender);

            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;

            sb.Begin();

            for(int i = 0; i < WORLD_WIDTH; i++)
            {
                for(int j = 0; j < WORLD_HEIGHT; j++)
                {
                    Tile tile = TileGrid[i, j];

                    tile.Draw(sb, new Vector2(i * Tile.WIDTH, j * Tile.HEIGHT));
                }
            }

            sb.End();

            ScreenManager.Instance.Graphics.GraphicsDevice.SetRenderTarget(null);
        }

        public void SetTile(Tile newTile, int i, int j)
        {
            TileGrid[i, j] = newTile;

            UpdateWorldRender();
        }

        public void SetTile(Tile newTile)
        {
            int i = newTile.Coordinates.X;
            int j = newTile.Coordinates.Y;
            TileGrid[i, j] = newTile;

            UpdateWorldRender();
        }

        public Rectangle GetTileBounds(int i, int j) => new Rectangle(i * Tile.WIDTH, j * Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT);

        public Tile GetTile(int i, int j) => TileGrid[i, j];

        public RenderTarget2D WorldRender { get; private set; }

        public Tile[,] TileGrid { get; set; }

        public int WidthInPixels => WORLD_WIDTH * Tile.WIDTH;

        public int HeightInPixels => WORLD_HEIGHT * Tile.HEIGHT;
    }
}
