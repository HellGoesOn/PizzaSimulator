using Microsoft.Xna.Framework;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Content.UI.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.UI
{
    public class UIStats : UIElement
    {
        protected override void Initialize()
        {
            Scale = 1f;
            ConsumesMouse = false;

            SetWidth(ScreenManager.Instance.ScreenWidth);
            SetHeight(ScreenManager.Instance.ScreenHeight);

            Panel = new UIElement();
            Panel.SetWidth(300);
            Panel.SetHeight(200);
            Panel.HAlign = 0.005f;
            Panel.VAlign = 0.95f;
            Panel.RecalcAlignmentForChildren();

            EntitySelect = new UITextButton("", 100, 40);
            EntitySelect.OnClick += EntitySelect_OnClick;
            EntitySelect.TakesPriority = true;
            EntitySelect.SetPosition(new Vector2(10));

            TileButton = new UITextButton("", 100, 40);
            TileButton.OnClick += TileButton_OnClick;
            TileButton.TakesPriority = true;
            TileButton.SetPosition(new Vector2(30, 60));

            UITextButton left = new UITextButton("<", 16, 40);
            left.TakesPriority = true;
            left.SetPosition(new Vector2(10, 60));
            left.OnClick += delegate { GameLoop.MyPlayer.SelectedTile--; };

            right = new UITextButton(">", 16, 40);
            right.TakesPriority = true;
            right.SetPosition(new Vector2(10, 60));
            right.OnClick += delegate { GameLoop.MyPlayer.SelectedTile++; };

            UITextButton quit = new UITextButton("Quit", 60, 40);
            quit.VAlign = 0.95f;
            quit.HAlign = 0.95f;
            quit.OnClick += delegate { GameStateManager.Instance.SwitchState(GameState.GameMenu); };
            quit.TakesPriority = true;


            Panel.Append(EntitySelect);
            Panel.Append(TileButton);
            Panel.Append(left);
            Panel.Append(right);

            Panel.Append(quit);


            Append(Panel);
            Append(new UIFadeIn());
        }

        private void EntitySelect_OnClick(object sender, EventArgs e)
        {
            Player plr = GameLoop.MyPlayer;

            plr.OnLeftClick -= plr.DoSpawn;

            plr.OnLeftClick += plr.DoSpawn;
        }

        private void TileButton_OnClick(object sender, EventArgs e)
        {
            Player plr = GameLoop.MyPlayer;
            plr.OnLeftClick -= plr.DoBuild;
            plr.BuildMode = true;

            plr.OnLeftClick += plr.DoBuild;
        }

        protected override void UpdateSelf()
        {
            Player plr = GameLoop.MyPlayer;

            EntitySelect.UpdateText($"{plr.SelectedEntityType}");

            TileButton.UpdateText($"{(TileType)plr.SelectedTile}");

            right.SetPosition(new Vector2(34 + TileButton.ScaledWidth, 60));
        }

        private UITextButton right;

        public UIElement Panel { get; set; }

        public UITextButton TileButton { get; set; }

        public UITextButton EntitySelect { get; set; }

        protected override void DrawSelf() { }
    }
}
