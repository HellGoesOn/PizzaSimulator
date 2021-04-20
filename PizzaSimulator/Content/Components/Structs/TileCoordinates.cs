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

        public int X { get; set; }

        public int Y { get; set; }
    }
}
