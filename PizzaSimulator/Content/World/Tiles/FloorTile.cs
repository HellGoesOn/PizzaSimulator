using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World.Tiles.SubTiles;

namespace PizzaSimulator.Content.World.Tiles
{
    public class FloorTile : Tile
    {
        public FloorTile(TileCoordinates c) : base(Assets.FloorTile, c)
        {
        }
    }
}
