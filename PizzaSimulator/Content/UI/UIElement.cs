using Microsoft.Xna.Framework;
using PizzaSimulator.Helpers;
using System;
using System.Collections.Generic;

namespace PizzaSimulator.Content.UI
{
    public class UIElement
    {
        public event EventHandler OnClick;

        public UIElement()
        {
            Children = new List<UIElement>();
            Initialize();
        }

        public void Append(UIElement element)
        {
            element.Parent = this;
            element.RecalcAlignment();
            element.RecalcAlignmentForChildren();
            Children.Add(element);
        }

        public void Click()
        {
            OnClick?.Invoke(this, new EventArgs());
        }

        public void Update()
        {
            UpdateSelf();

            foreach (UIElement c in Children)
                c.Update();
        }

        public void RecalcAlignment()
        {
            bool hasParent = Parent != null;

            int screenWidth = ScreenManager.Instance.ScreenWidth;
            int screenHeight = ScreenManager.Instance.ScreenHeight;

            if(HAlign == 0 && VAlign == 0)
            {
                if (!hasParent)
                    RealPosition = Position;
                else
                    RealPosition = Parent.RealPosition + Position;
                return;
            }

            Console.WriteLine($"{Position}");
            Console.WriteLine($"{Center}");

            float hAlignX = screenWidth * HAlign;
            float hAlignY = screenHeight * VAlign;

            if (hasParent)
            {
                hAlignX = Parent.Width * HAlign - Width * 0.5f;
                hAlignY = Parent.Height * VAlign - Height * 0.5f;

                hAlignX = Math.Clamp(hAlignX, 4, Parent.Width - Width - 4);
                hAlignY = Math.Clamp(hAlignY, 4, Parent.Height - Height - 4);


                RealPosition = Parent.RealPosition + new Vector2(hAlignX, hAlignY);
                return;
            }

            RealPosition = new Vector2(hAlignX, hAlignY);
        }

        public void Draw()
        {
            DrawSelf();

            foreach (UIElement c in this.Children)
                c.Draw();
        }

        public void RecalcAlignmentForChildren()
        {
            foreach (UIElement c in this.Children)
                c.RecalcAlignment();
        }

        protected virtual void Initialize() { }

        protected virtual void UpdateSelf() { }

        protected virtual void DrawSelf()
        {
            DrawHelper.DrawRectangle(RealPosition + new Vector2(-2, 0), Width + 4, Height + 4, Color.Black * 0.5f);
            DrawHelper.DrawRectangle(RealPosition, Width, Height, Color.White * 0.75f);
        }

        public void SetPosition(Vector2 targetPos)
        {
            Position = targetPos;

            RecalcAlignment();
            RecalcAlignmentForChildren();
        }

        public void SetWidth(int width)
        {
            Width = width;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }

        public bool TakesPriority { get; set; } = false;

        public bool ConsumesMouse { get; set; } = true;

        public float HAlign { get; set; }
        public float VAlign { get; set; }

        public Vector2 Position { get; private set; }

        public Vector2 Center => Position + new Vector2((int)(Width * 0.5f), (int)(Height * 0.5f));

        public Vector2 RealPosition { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool IsMouseHovering => Area.Contains(InputManager.MouseWorldPosition) && ConsumesMouse;

        public Rectangle Area => new Rectangle((int)RealPosition.X, (int)RealPosition.Y, Width, Height);

        public UIElement Parent { get; private set; }

        public List<UIElement> Children { get; private set; }
    }
}
