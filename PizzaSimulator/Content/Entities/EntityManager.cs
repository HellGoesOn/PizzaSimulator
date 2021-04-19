using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            if (InputManager.HasRightClicked)
                Customer.SpawnCustomer(InputManager.MouseScreenPosition);

            if(InputManager.HasLeftClicked)
            {
                for(int i = Entities.Count - 1; i >= 0; i--)
                {
                    if (Entities[i].Highlighted)
                        Customer.KillCustomer(Entities[i]);
                }
            }

            foreach (Entity e in Entities)
            {
                e.Highlighted = false;

                if (e.MyCollider.Hitbox.Contains(InputManager.MouseScreenPosition))
                    e.Highlighted = true;
            }
        }

        public void DrawInfo(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entity e = Entities[i];
                spriteBatch.DrawString(Loader.DefaultFont, e.Position.ToString(), new Vector2(20, 40 + 20 * i), Color.White);
            }
        }

        public float DeltaTime { get; private set; }

        public List<Entity> Entities { get; } = new List<Entity>();
    }
}
