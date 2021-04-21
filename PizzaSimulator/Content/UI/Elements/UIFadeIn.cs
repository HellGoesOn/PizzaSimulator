using Microsoft.Xna.Framework;
using PizzaSimulator.Helpers;

namespace PizzaSimulator.Content.UI.Elements
{
    public class UIFadeIn : UIElement
    {
        public UIFadeIn()
        {
            SetWidth(ScreenManager.Instance.ScreenWidth);
            SetHeight(ScreenManager.Instance.ScreenHeight);
            Opacity = 1.25f;
            ConsumesMouse = true;
            TakesPriority = true;

            SetPosition(Vector2.Zero);
        }

        protected override void UpdateSelf()
        {
            Opacity -= Speed;

            if(Opacity <= 0.05f)
            {
                if (Parent != null)
                    Parent.ChildrenToDisown.Add(this);
                else
                {
                    UIManager.Instance.ElementsToRemove.Add(this);
                }
            }
        }

        protected override void DrawSelf()
        {
            int width = ScreenManager.Instance.ScreenWidth;
            int height = ScreenManager.Instance.ScreenHeight;

            DrawHelper.DrawRectangle(Vector2.Zero, width, height, Color.Black * Opacity);
        }

        public float Speed { get; set; } = 0.005f;
    }
}
