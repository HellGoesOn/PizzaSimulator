using PizzaSimulator.Content.Components.Structs;

namespace PizzaSimulator.Content.World.Tiles
{
    public class FloorTile : Tile
    {
        public FloorTile(TileCoordinates c) : base(Assets.FloorTile, c)
        {
        }
    }
}
