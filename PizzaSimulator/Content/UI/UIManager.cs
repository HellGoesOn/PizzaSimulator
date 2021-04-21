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
            ElementsToRemove = new HashSet<UIElement>();
        }

        public void UpdateInterfaces()
        {
            foreach (UIElement e in Elements)
            {
                e.Update();
            }

            if (InputManager.HasLeftClicked)
                GetSelectedElement()?.Click();

            foreach (UIElement e in ElementsToRemove)
                Elements.Remove(e);

            ElementsToRemove.Clear();
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

                if(e.Children.Count > 0)
                    return GetFinalElement(e);

                if (e.IsMouseHovering && e.TakesPriority)
                    return e;
            }

            return null;
        }

        public UIElement GetFinalElement(UIElement element)
        {
            if (element.Children.Count > 0)
            {
                foreach (UIElement child in element.Children)
                {
                    UIElement result = GetFinalElement(child);

                    if (result != null && result.TakesPriority)
                        return result;

                }
            }

            if(element.IsMouseHovering)
                return element;

            return null;
        }

        public bool MouseConsumedByUI
        {
            get
            {
                return Elements.Count(x => x.IsMouseHovering || x.Children.Count(x => x.IsMouseHovering) > 0) > 0;
            }
        }

        public HashSet<UIElement> ElementsToRemove { get; private set; }

        public List<UIElement> Elements { get; private set; }
    }
}
