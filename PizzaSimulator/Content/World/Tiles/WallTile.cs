using PizzaSimulator.Content.Components.Structs;

namespace PizzaSimulator.Content.World.Tiles
{
    public class WallTile : Tile
    {
        public WallTile(TileCoordinates c) : base(Assets.Wall, c)
        {
        }
    }
}
