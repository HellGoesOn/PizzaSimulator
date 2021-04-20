using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Helpers;
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
            foreach (Entity e in Entities)
            {
                e.Highlighted = false;

                if (e.MyCollider.Hitbox.Contains(InputManager.MouseScreenPosition))
                    e.Highlighted = true;
            }
        }

        public static void CreateEntity(EntityType entity, Vector2 position)
        {
            switch(entity)
            {
                case EntityType.Customer:
                    Customer.SpawnCustomer(position);
                    break;
                case EntityType.Worker:
                    Worker.SpawnWorker(position);
                    break;
            }
        }

        public static void KillEntity(Entity e)
        {
            Entity entityRef = null;

            foreach(Entity entity in Instance.Entities)
                if (entity == e)
                    entityRef = entity;

            Instance.Entities.Remove(entityRef);
        }

        public float DeltaTime { get; private set; }

        public List<Entity> Entities { get; } = new List<Entity>();
    }
}
