using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components;
using System.Collections.Generic;

namespace PizzaSimulator.Content.Entities
{
    public class EntityManager : Singleton<EntityManager>
    {
        public void SetDeltaTime(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Update()
        {
            if (InputManager.HasLeftClicked)
                Customer.SpawnCustomer(InputManager.MouseScreenPosition);
        }

        public float DeltaTime { get; private set; }

        public List<Entity> Entities { get; } = new List<Entity>();
    }
}
