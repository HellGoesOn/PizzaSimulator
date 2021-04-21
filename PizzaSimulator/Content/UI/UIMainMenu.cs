using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.UI.Elements;
using PizzaSimulator.Helpers;
using System;

namespace PizzaSimulator.Content.UI
{
    public class UIMainMenu : UIElement
    {
        public UIMainMenu()
        {
            SetWidth(ScreenManager.Instance.ScreenWidth);
            SetHeight(ScreenManager.Instance.ScreenHeight);

            ConsumesMouse = false;

            UIImage logo = new UIImage(new SpriteAnimation(Assets.Logo, 1, 1, true));
            logo.HAlign = 0.6f;
            logo.VAlign = 0.4f;
            logo.Scale = 3f;

            UITextButton startGame = new UITextButton("New Game", 200, 50);
            startGame.Text.Scale = 1f;
            startGame.HAlign = 0.5f;
            startGame.VAlign = 0.2f;
            startGame.TakesPriority = true;
            startGame.OnClick += StartGame_OnClick;

            UITextButton endGame = new UITextButton("Quit", 200, 50);
            endGame.Text.Scale = 1f;
            endGame.HAlign = 0.5f;
            endGame.VAlign = 0.8f;
            endGame.TakesPriority = true;
            endGame.OnClick += EndGame_OnClick;

            UIElement panel = new UIElement();
            panel.SetWidth(300);
            panel.SetHeight(300);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.75f;

            Console.WriteLine($"{startGame.Width}{startGame.Height}");
            Console.WriteLine($"{startGame.RealPosition}");

            panel.Append(startGame);
            panel.Append(endGame);

            Append(logo);
            Append(panel);
        }

        private void EndGame_OnClick(object sender, EventArgs e)
        {
            GameLoop.Instance.Exit();
        }

        private void StartGame_OnClick(object sender, EventArgs e)
        {
            GameStateManager.Instance.SwitchState(GameState.StartPLay);
        }

        protected override void DrawSelf()
        {
            UIElement element = null;

            element = UIManager.Instance.GetSelectedElement();

            if(element != null)
            {
                DrawHelper.DrawBorderedString(element.GetType().Name, new Vector2(20), Color.White);
            }
        }
    }
}
