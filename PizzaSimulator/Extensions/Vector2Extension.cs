using Microsoft.Xna.Framework;
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
    }
}
