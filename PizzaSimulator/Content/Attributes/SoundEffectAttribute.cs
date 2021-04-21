using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Attributes
{
    public class SoundEffectAttribute : Attribute
    {
        public SoundEffectAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
