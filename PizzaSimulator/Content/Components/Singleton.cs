using System;

namespace PizzaSimulator.Content.Components
{
    public abstract class Singleton<T> where T : class
    {
        private static T instance;

        public static void Dispose()
        {
            instance = null;
        }

        protected Singleton() { }

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = (T)Activator.CreateInstance(typeof(T));

                return instance;
            }
        }
    }
}
