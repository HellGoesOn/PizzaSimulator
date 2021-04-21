using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Content.World.Tiles;
using System;
using System.Collections.Generic;

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

            PathingGrid = new PathGrid(this);

            ImportantTiles = new List<Tile>();

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

        public Tile SetTile(Tile newTile, int i, int j)
        {
            Tile tile = (Tile)Activator.CreateInstance(newTile.GetType(), new TileCoordinates(i, j));

            if (ImportantTiles.Contains(TileGrid[i, j]))
                ImportantTiles.Remove(TileGrid[i, j]);

            TileGrid[i, j] = tile;

            UpdateWorldRender();
            PathingGrid.CreateGrid(this);

            return TileGrid[i, j];
        }

        public void AddSubTile(SubTile subTile, int i, int j)
        {
            Tile tile = TileGrid[i, j];

            tile.TryAddSubtile(subTile);

            ImportantTiles.Add(tile);
            UpdateWorldRender();
        }

        public List<Entity> ContainingEntities(Tile tile)
        {
            List<Entity> entityList = new List<Entity>();

            foreach (Entity e in EntityManager.Instance.Entities)
            {
                if (GetTileBounds(tile.Coordinates.X, tile.Coordinates.Y).Contains(e.Position))
                    entityList.Add(e);
            }

            return entityList;
        }

        public Rectangle GetTileBounds(int i, int j) => new Rectangle(i * Tile.WIDTH, j * Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT);

        public Tile GetTile(int i, int j) => TileGrid[i, j];

        public Tile GetTile(TileCoordinates c) => TileGrid[c.X, c.Y];

        public RenderTarget2D WorldRender { get; private set; }

        public Tile[,] TileGrid { get; set; }

        public PathGrid PathingGrid { get; set; }

        public static int WidthInPixels => WORLD_WIDTH * Tile.WIDTH;

        public static int HeightInPixels => WORLD_HEIGHT * Tile.HEIGHT;

        public List<Tile> ImportantTiles { get; set; }
    }
}
