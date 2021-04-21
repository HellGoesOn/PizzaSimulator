using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.UI.Elements
{
    public class UITextButton : UIElement
    {
        public UITextButton(string text, int width, int height)
        {
            ConsumesMouse = true;
            Text = new UIText(text);
            Text.HAlign = 0.5f;
            Text.VAlign = 0.5f;
            Text.TakesPriority = false;

            this.TakesPriority = true;
            SetWidth(width);
            SetHeight(height);

            while (Width < Text.Width + 4)
                SetWidth(Width + 1);

            while (Height < Text.Height + 4)
                SetHeight(Height + 1);

            Append(Text);
        }

        public void UpdateText(string text)
        {
            Vector2 oldTextSize = Assets.DefaultFont.MeasureString(Text.Text);
            Text.SetTextSilent(text);

            if ((int)oldTextSize.X < Text.Width)
            {
                while (Width < Text.Width * 2)
                    SetWidth(Width + 1);
            }
            else if((int)oldTextSize.X > Text.Width)
            {
                while (Width > Text.Width * 2)
                    SetWidth(Width - 1);
            }

            if ((int)oldTextSize.Y < Text.Height)
            {
                while (Width < Text.Height * 2)
                    SetWidth(Height + 1);
            }
            else if ((int)oldTextSize.Y > Text.Height)
            {
                while (Width > Text.Height * 2)
                    SetWidth(Height - 1);
            }

            Text.RecalcAlignment();
        }

        public UIText Text { get; set; }
    }
}
