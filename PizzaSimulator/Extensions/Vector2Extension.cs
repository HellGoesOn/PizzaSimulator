using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 ToNormalized(this Vector2 v)
        {
            v.Normalize();
            return v;
        }

        public static Vector2 RotatedBy(this Vector2 v, float radians, Vector2 center = default)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            Vector2 vector = v - center;
            Vector2 result = center;
            result.X += vector.X * cos - vector.Y * sin;
            result.Y += vector.X * sin + vector.Y * cos;
            return result;
        }

        public static TileCoordinates ToTileCoordinates(this Vector2 v) => new TileCoordinates((int)(v.X / Tile.WIDTH), (int)(v.X / Tile.HEIGHT));
    }
}
