using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Components.Structs
{
    public struct TileCoordinates
    {
        public TileCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator string(TileCoordinates c) => $"TileCoords: {'{'}X:{c.X};Y:{c.Y}{'}'}";

        public int X { get; set; }

        public int Y { get; set; }
    }
}
