using Microsoft.Xna.Framework;
using PizzaSimulator.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void SetColor(Color color)
        {
            TextColor = color;
            RecalcAlignment();
        }

        protected override void DrawSelf()
        {
            DrawHelper.DrawBorderedString(Text, this.RealPosition, TextColor);
        }

        public Color TextColor { get; private set; }

        public string Text { get; private set; }
    }
}
