using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Extensions;

namespace PizzaSimulator.Helpers
{
    public static class DrawHelper
    {
        public static void DrawShadowedString(string text, Vector2 position, Color color)
        {
            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;

            sb.DrawString(Assets.DefaultFont, text, position + Vector2.One, Color.Black);
            sb.DrawString(Assets.DefaultFont, text, position, color);
        }

        public static void DrawBorderedString(string text, Vector2 position, Color color)
        {
            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;

            for (int i = 0; i < 4; i++)
            {
                Vector2 offset = new Vector2(1, 0).RotatedBy(MathHelper.PiOver2 * i);
                sb.DrawString(Assets.DefaultFont, text, position + offset, Color.Black);
            }

            sb.DrawString(Assets.DefaultFont, text, position, color);
        }
    }
}
