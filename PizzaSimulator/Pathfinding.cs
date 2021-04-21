using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using PizzaSimulator.Extensions;
using System;
using System.Collections.Generic;

namespace PizzaSimulator
{
    public static class Pathfinding
    {
        public static List<Node> FindPath(PathGrid grid, Vector2 startPos, Vector2 targetPos)
        {
            Node startNode = NodeFromCoordinates(grid, startPos);
            Node targetNode = NodeFromCoordinates(grid, targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node current = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < current.FCost || openSet[i].FCost == current.FCost)
                    {
                        if (openSet[i].HCost < current.HCost)
                            current = openSet[i];
                    }
                }

                openSet.Remove(current);
                closedSet.Add(current);

                if (current == targetNode)
                {
                    List<Node> path = RetracePath(startNode, targetNode);
                    return path;
                }

                foreach (Node neighbour in grid.GetNeighbours(current))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                        continue;

                    int newMovementCost = current.GCost + GetDistance(current, neighbour);

                    if(newMovementCost < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCost;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = current;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return new List<Node>();
        }

        public static List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node current = endNode;

            while(current != startNode)
            {
                path.Add(current);
                current = current.Parent;
            }

            path.Reverse();

            return path;
        }

        public static int GetDistance(Node a, Node b)
        {
            int dstX = Math.Abs(a.X - b.X);
            int dstY = Math.Abs(a.Y - b.Y);

            if (dstX > dstY)
                return 10 * (dstX - dstY);

            return  10 * (dstY - dstX);
        }

        public static Node NodeFromCoordinates(PathGrid grid, Vector2 position)
        {
            TileCoordinates coords = position.ToTileCoordinates();

            coords.X = Math.Clamp(coords.X, 0, GameWorld.WORLD_WIDTH);
            coords.Y = Math.Clamp(coords.Y, 0, GameWorld.WORLD_HEIGHT);

            return grid.Grid[coords.X, coords.Y];
        }
    }
}
