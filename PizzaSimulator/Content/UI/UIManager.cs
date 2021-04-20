using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSimulator.Content.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public void Initialize()
        {
            Elements = new List<UIElement>();
        }

        public void UpdateInterfaces()
        {
            foreach (UIElement e in Elements)
            {
                e.Update();
            }

            if (InputManager.HasLeftClicked)
                GetSelectedElement()?.Click();
        }

        public void DrawInterfaces()
        {
            foreach (UIElement e in Elements)
                e.Draw();
        }

        public void RecalcAll()
        {
            foreach(UIElement e in Elements)
            {
                e.RecalcAlignment();
                e.RecalcAlignmentForChildren();
            }
        }

        public void AddElement(UIElement e)
        {
            e.RecalcAlignment();
            e.RecalcAlignmentForChildren();
            Elements.Add(e);
        }

        public UIElement GetSelectedElement()
        {
            for(int i = Elements.Count - 1; i >= 0; i--)
            {
                UIElement e = Elements[i];

                if (e.Children.Count > 0)
                {
                    for(int j = e.Children.Count - 1; j >= 0; j--)
                    {
                        UIElement child = e.Children[j];

                        if (child.IsMouseHovering && child.TakesPriority)
                            return child;
                    }
                }

                if (e.IsMouseHovering)
                    return e;

            }

            return null;
        }

        public bool MouseConsumedByUI
        {
            get
            {
                return Elements.Count(x => x.IsMouseHovering || x.Children.Count(x => x.IsMouseHovering) > 0) > 0;
            }
        }

        public List<UIElement> Elements { get; private set; }
    }
}
