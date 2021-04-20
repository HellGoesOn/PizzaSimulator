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
                spriteBatch.DrawString(Assets.DefaultFont, e.Position.ToString(), new Vector2(20, 60 + 20 * i), Color.White);
            }
        }

        public float DeltaTime { get; private set; }

        public List<Entity> Entities { get; } = new List<Entity>();
    }
}
