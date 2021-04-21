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
            Panel.HAlign = 0.015f;
            Panel.VAlign = 0.985f;
            Panel.RecalcAlignmentForChildren();

            EntityButton = new UITextButton("", 100, 40);
            EntityButton.OnClick += EntitySelect_OnClick;
            EntityButton.TakesPriority = true;
            EntityButton.SetPosition(new Vector2(30, 10));

            TileButton = new UITextButton("", 100, 40);
            TileButton.OnClick += TileButton_OnClick;
            TileButton.TakesPriority = true;
            TileButton.SetPosition(new Vector2(30, 60));

            UITextButton leftEntity = new UITextButton("<", 16, 40)
            {
                TakesPriority = true
            };
            leftEntity.SetPosition(new Vector2(10));
            leftEntity.OnClick += delegate { GameLoop.MyPlayer.SelectedEntityType--; };

            UITextButton leftTile = new UITextButton("<", 16, 40)
            {
                TakesPriority = true
            };
            leftTile.SetPosition(new Vector2(10, 60));
            leftTile.OnClick += delegate { GameLoop.MyPlayer.SelectedTile--; };

            rightTile = new UITextButton(">", 16, 40)
            {
                TakesPriority = true
            };
            rightTile.SetPosition(new Vector2(10, 60));
            rightTile.OnClick += delegate { GameLoop.MyPlayer.SelectedTile++; };

            rightEntity = new UITextButton(">", 16, 40)
            {
                TakesPriority = true
            };
            rightEntity.SetPosition(new Vector2(10, 10));
            rightEntity.OnClick += delegate { GameLoop.MyPlayer.SelectedEntityType++; };


            UITextButton quit = new UITextButton("Quit", 60, 40)
            {
                VAlign = 0.015f,
                HAlign = 0.015f
            };
            quit.OnClick += delegate { GameStateManager.Instance.SwitchState(GameState.GameMenu); };
            quit.TakesPriority = true;

            UIElement panel = new UIElement();
            panel.SetWidth(200);
            panel.SetHeight(100);
            panel.VAlign = 0.985f;
            panel.HAlign = 0.985f;

            actionText = new UIText("No Action Avaiable")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            panel.Append(actionText);


            Panel.Append(EntityButton);
            Panel.Append(TileButton);
            Panel.Append(leftTile);
            Panel.Append(rightTile);
            Panel.Append(rightEntity);
            Panel.Append(leftEntity);

            Append(panel);

            Append(quit);


            Append(Panel);
            Append(new UIFadeIn());
        }

        private void EntitySelect_OnClick(object sender, EventArgs e)
        {
            Player plr = GameLoop.MyPlayer;

            plr.ClearLeftClickEvents();
            actionText.SetText("Spawn Selected Entity");

            plr.OnLeftClick += plr.DoSpawn;
        }

        private void TileButton_OnClick(object sender, EventArgs e)
        {
            Player plr = GameLoop.MyPlayer;
            plr.ClearLeftClickEvents();
            plr.BuildMode = true;

            actionText.SetText("Place Selected Tile");

            plr.OnLeftClick += plr.DoBuild;
        }

        protected override void UpdateSelf()
        {
            Player plr = GameLoop.MyPlayer;

            EntityButton.UpdateText($"{plr.SelectedEntityType}");

            TileButton.UpdateText($"{(TileType)plr.SelectedTile}");

            rightTile.SetPosition(new Vector2(34 + TileButton.ScaledWidth, 60));

            rightEntity.SetPosition(new Vector2(34 + EntityButton.ScaledWidth, 10));

            if (!plr.HasActions)
                actionText.SetText($"{plr.Findtile(InputManager.MouseScreenPosition).SubTiles[0, 0]}" +
                    $"{plr.Findtile(InputManager.MouseScreenPosition).SubTiles[0, 1]}" +
                    $"{plr.Findtile(InputManager.MouseScreenPosition).SubTiles[1, 0]}" +
                    $"{plr.Findtile(InputManager.MouseScreenPosition).SubTiles[1, 1]}");
        }

        private UITextButton rightEntity;

        private UITextButton rightTile;

        private UIText actionText;

        public UIElement Panel { get; set; }

        public UITextButton TileButton { get; set; }

        public UITextButton EntityButton { get; set; }

        protected override void DrawSelf() { }
    }
}
