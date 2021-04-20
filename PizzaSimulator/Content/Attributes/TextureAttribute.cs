using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Attributes
{
    public class TextureAttribute : Attribute
    {
        public TextureAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
