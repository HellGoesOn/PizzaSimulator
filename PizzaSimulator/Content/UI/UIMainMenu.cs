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

            Logo = new UIImage(new SpriteAnimation(Assets.Logo, 1, 1, true))
            {
                HAlign = 0.5f,
                VAlign = 0.2f,
                Scale = 3f
            };

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
            panel.SetWidth(240);
            panel.SetHeight(180);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.95f;

            panel.Append(startGame);
            panel.Append(endGame);

            Append(Logo);
            Append(panel);

            UIFadeIn fadeIn = new UIFadeIn() { Opacity = 3f };

            UIBKBLogo logo = new UIBKBLogo
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                Scale = 0.5f
            };

            fadeIn.Append(logo);

            Append(fadeIn);
            logoScale = 0;
        }

        private void EndGame_OnClick(object sender, EventArgs e)
        {
            GameLoop.Instance.Exit();
        }

        private void StartGame_OnClick(object sender, EventArgs e)
        {
            GameStateManager.Instance.SwitchState(GameState.StartPLay);
        }

        protected override void UpdateSelf()
        {
            base.UpdateSelf();

            logoScale += reverseScaleGain ? -0.005f : 0.005f;

            if (Math.Abs(logoScale) > 0.5f)
                reverseScaleGain = !reverseScaleGain;

            Console.WriteLine($"{Logo.RealPosition}{Logo.Width}{Logo.Height}");

            if (Logo != null)
            {
                Logo.Texture.Rotation = logoScale * 0.12f;
                Logo.Scale = 3 + logoScale;
            }
        }

        protected override void DrawSelf()
        {
            UIElement element = UIManager.Instance.GetSelectedElement();

            if(element != null)
            {
                DrawHelper.DrawBorderedString(element.GetType().Name, new Vector2(20), Color.White);
            }
        }

        private float logoScale;

        private bool reverseScaleGain;

        protected UIImage Logo { get; set; }
    }
}
