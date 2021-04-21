using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public class Node
    {
        public Node(int x, int y, bool walkable = true)
        {
            X = x;
            Y = y;
            Walkable = walkable;
        }

        public static Vector2 ToWorldPos(Node node) => Tile.GetCenter(GameLoop.World.GetTile(node.X, node.Y));

        public int GCost { get; set; }

        public int HCost { get; set; }

        public int FCost => GCost + HCost;

        public Node Parent { get; set; }

        public bool Walkable { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
