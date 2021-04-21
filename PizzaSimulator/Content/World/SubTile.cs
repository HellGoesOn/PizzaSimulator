using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PizzaSimulator.Content.World
{
    public abstract class SubTile
    {
        protected SubTile(Texture2D texture)
        {
            Texture = texture;
        }

        public const int WIDTH = 16, HEIGHT = 16;

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }

        public Texture2D Texture { get; }
    }
}
