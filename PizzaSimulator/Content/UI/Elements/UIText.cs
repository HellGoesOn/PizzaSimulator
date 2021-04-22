using Microsoft.Xna.Framework;
using PizzaSimulator.Helpers;

namespace PizzaSimulator.Content.UI.Elements
{
    public class UIText : UIElement
    {
        public UIText(string text)
        {
            SetText(text);

            TextColor = Color.White;
        }

        public void SetText(string text)
        {
            Text = text;

            Vector2 size = Assets.DefaultFont.MeasureString(text);
            SetWidth((int)size.X);
            SetHeight((int)size.Y);
            RecalcAlignment();
        }

        public void SetTextSilent(string text)
        {
            Text = text;

            Vector2 size = Assets.DefaultFont.MeasureString(text);
            SetWidth((int)size.X);
            SetHeight((int)size.Y);
        }

        public void SetColor(Color color)
        {
            TextColor = color;
            RecalcAlignment();
        }

        protected override void DrawSelf()
        {
            DrawHelper.DrawBorderedString(Text, new Vector2((int)this.RealPosition.X, (int)this.RealPosition.Y), TextColor, Scale);
        }

        public Color TextColor { get; private set; }

        public string Text { get; private set; }
    }
}
