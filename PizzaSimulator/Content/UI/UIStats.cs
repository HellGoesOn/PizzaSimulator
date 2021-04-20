using Microsoft.Xna.Framework;
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
            SetWidth(ScreenManager.Instance.ScreenWidth);
            SetWidth(ScreenManager.Instance.ScreenHeight);
            ConsumesMouse = false;

            SetWidth(ScreenManager.Instance.ScreenWidth);
            SetHeight(ScreenManager.Instance.ScreenHeight);

            BuildModeText = new UIText("");
            BuildModeText.SetPosition(new Vector2(5, 20));

            HighlightedTileText = new UIText("");
            HighlightedTileText.SetPosition(new Vector2(5, 40));
            HighlightedTileText.SetColor(Color.Yellow);

            SelectedPlaceableTileText = new UIText("");
            SelectedPlaceableTileText.SetPosition(new Vector2(5, 60));
            SelectedPlaceableTileText.SetColor(Color.DarkGoldenrod);


            Panel = new UIElement();
            Panel.SetWidth(400);
            Panel.SetHeight(100);
            Panel.HAlign = 0.5f;
            Panel.VAlign = 1f;
            Panel.TakesPriority = true;

            Panel.Append(BuildModeText);
            Panel.Append(SelectedPlaceableTileText);
            Panel.Append(HighlightedTileText);
            Panel.RecalcAlignmentForChildren();

            Append(Panel);
        }

        protected override void UpdateSelf()
        {
            Player plr = GameLoop.MyPlayer;

            BuildModeText.SetText($"Build Mode: {GameLoop.MyPlayer.BuildMode}");
            BuildModeText.SetColor(plr.BuildMode ? Color.LimeGreen : Color.Red);

            string highlightedText = plr.HighlightedTile != null ? plr.HighlightedTile.ToString() : "No tile selected";
            HighlightedTileText.SetText(highlightedText);

            string selectedPlaceableText = plr.AvailableTiles[plr.SelectedTile].ToString();
            SelectedPlaceableTileText.SetText(selectedPlaceableText);
        }

        public UIElement Panel { get; set; }

        public UIText BuildModeText { get; set; }

        public UIText HighlightedTileText { get; set; }

        public UIText SelectedPlaceableTileText { get; set; }

        protected override void DrawSelf() { }
    }
}
