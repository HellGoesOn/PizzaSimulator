using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;

namespace PizzaSimulator.Content.UI.Elements
{
    public class UIImage : UIElement
    {
        public UIImage(SpriteAnimation s)
        {
            Texture = s;

            SetWidth(Texture.FrameRect.Width);
            SetHeight(Texture.FrameRect.Height);
        }

        protected override void UpdateSelf()
        {
            Texture.Update();

            Texture.Scale = this.Scale;
        }

        protected override void DrawSelf()
        {
            SpriteBatch sb = ScreenManager.Instance.SpriteBatch;
            SpriteAnimation animation = Texture;
            Texture2D texture = animation.SpriteSheet;
            Rectangle frame = animation.FrameRect;
            Color color = Color.White;
            float rotation = animation.Rotation;
            Vector2 drawOrigin = animation.DrawOrigin;
            float scale = animation.Scale;
            SpriteEffects spriteEffects = animation.SpriteFX;

            sb.Draw(texture, RealPosition - Center, frame, color, rotation, drawOrigin, scale, spriteEffects, 1f);
        }

        public SpriteAnimation Texture { get; set; }
    }
}
