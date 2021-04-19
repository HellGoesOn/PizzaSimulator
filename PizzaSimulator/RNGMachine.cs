using PizzaSimulator.Content.Components;
using System;

namespace PizzaSimulator
{
    public class RNGMachine : Singleton<RNGMachine>
    {
        public RNGMachine() { Generator = new Random(); }

        public Random Generator { get; }
    }
}
