using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public class PathGrid
    {
        public PathGrid(GameWorld world)
        {
            worldSizeX = GameWorld.WORLD_WIDTH;
            worldSizeY = GameWorld.WORLD_HEIGHT;
            CreateGrid(world);
        }

        public void CreateGrid(GameWorld world)
        {
            Grid = new Node[worldSizeX, worldSizeY];

            for(int x = 0; x < worldSizeX; x++)
            {
                for (int y = 0; y < worldSizeY; y++)
                {
                    bool walkable = world.TileGrid[x, y].GetType() != typeof(WallTile);
                    Grid[x, y] = new Node(x, y, walkable);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y<= 1; y++)
                {
                    bool center = x == 0 && y == 0;

                    if (center || (Math.Abs(x) == 1 && Math.Abs(y) == 1))
                        continue;

                    int checkX = node.X + x;
                    int checkY = node.Y + y;

                    if(checkX >= 0 && checkX < worldSizeX && checkY >= 0 && checkY < worldSizeY)
                        neighbours.Add(Grid[checkX, checkY]);
                }
            }


            /// [-1;-1] [0;-1] [1;-1] 
            /// 

            return neighbours;
        }

        readonly int worldSizeX, worldSizeY;

        public Node[,] Grid { get; private set; }

        public Vector2 WorldSize { get; set; }
    }
}
