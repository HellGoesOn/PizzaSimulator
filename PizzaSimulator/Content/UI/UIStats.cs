using Microsoft.Xna.Framework;
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
            Panel.HAlign = 0.1f;
            Panel.VAlign = 1f;
            Panel.RecalcAlignmentForChildren();

            EntitySelect = new UITextButton("", 100, 40);
            EntitySelect.OnClick += delegate { GameLoop.MyPlayer.SelectedEtntityType++; };
            EntitySelect.TakesPriority = true;
            EntitySelect.SetPosition(new Vector2(10));

            TileButton = new UITextButton("", 100, 40);
            TileButton.OnClick += delegate { GameLoop.MyPlayer.SelectedTile++; };
            TileButton.TakesPriority = true;
            TileButton.SetPosition(new Vector2(10, 60));

            Panel.Append(EntitySelect);
            Panel.Append(TileButton);


            Append(Panel);
        }

        protected override void UpdateSelf()
        {
            Player plr = GameLoop.MyPlayer;

            EntitySelect.UpdateText($"{plr.SelectedEtntityType}");

            TileButton.UpdateText($"{(TileType)plr.SelectedTile}");
        }

        public UIElement Panel { get; set; }

        public UITextButton TileButton { get; set; }

        public UITextButton EntitySelect { get; set; }

        protected override void DrawSelf() { }
    }
}
