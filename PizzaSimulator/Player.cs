using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Content.UI;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public class Player
    {
        public void Update()
        {
            bool foundTile = false;

            float scrollWheelValue = InputManager.CurrentMouseState.ScrollWheelValue;
            float oldScrollWheelValue = InputManager.OldMouseState.ScrollWheelValue;

            Vector2 mousePos = InputManager.MouseScreenPosition;

            if (BuildMode)
            {
                if (InputManager.IsKeyPressed(Keys.Q))
                    SelectedTile--;

                if (InputManager.IsKeyPressed(Keys.E))
                    SelectedTile++;

                DoBuildMode(ref foundTile, ref mousePos);

                if (InputManager.HasRightClicked && !UIManager.Instance.MouseConsumedByUI)
                    BuildMode = false;
            }
            else
            {
                if (InputManager.IsKeyPressed(Keys.Q))
                    SelectedEtntityType--;

                if (InputManager.IsKeyPressed(Keys.E))
                    SelectedEtntityType++;

                HighlightedTile = null;

                if (InputManager.HasRightClicked && !UIManager.Instance.MouseConsumedByUI)
                    EntityManager.CreateEntity(SelectedEtntityType, mousePos);

                if (InputManager.HasLeftClicked && !UIManager.Instance.MouseConsumedByUI)
                {
                    for (int i = EntityManager.Instance.Entities.Count - 1; i >= 0; i--)
                    {
                        Entity e = EntityManager.Instance.Entities[i];

                        if (e.Highlighted)
                        {
                            EntityManager.KillEntity(e);
                            break;
                        }
                    }
                }
            }

            if (InputManager.IsKeyPressed(Keys.Space))
                BuildMode = !BuildMode;

            if (InputManager.IsKeyPressed(Keys.R))
                GameLoop.Instance.CommitApocalypse();

            if (oldScrollWheelValue > scrollWheelValue)
                CameraManager.Camera.TryZoom(-0.25f);
            else if (oldScrollWheelValue < scrollWheelValue)
                CameraManager.Camera.TryZoom(0.25f);

            float cameraSpeed = 5f;

            if (InputManager.IsKeyHeld(Keys.A))
                CameraManager.Camera.MoveBy(new Vector2(-cameraSpeed, 0));

            if (InputManager.IsKeyHeld(Keys.D))
                CameraManager.Camera.MoveBy(new Vector2(cameraSpeed, 0));

            if (InputManager.IsKeyHeld(Keys.W))
                CameraManager.Camera.MoveBy(new Vector2(0, -cameraSpeed));

            if (InputManager.IsKeyHeld(Keys.S))
                CameraManager.Camera.MoveBy(new Vector2(0, cameraSpeed));
        }

        private void DoBuildMode(ref bool foundTile, ref Vector2 mousePos)
        {
            for (int i = 0; i < GameWorld.WORLD_WIDTH; i++)
            {
                for (int j = 0; j < GameWorld.WORLD_HEIGHT; j++)
                {
                    HighlightedTile = null;
                    if (GameLoop.World.GetTileBounds(i, j).Contains(mousePos.ToPoint()))
                    {
                        HighlightedTile = GameLoop.World.GetTile(i, j);
                        foundTile = true;
                        break;
                    }
                }

                if (foundTile)
                    break;
            }

            if (InputManager.HasLeftClicked && !UIManager.Instance.MouseConsumedByUI)
            {
                if (HighlightedTile != null)
                {
                    int x = HighlightedTile.Coordinates.X;
                    int y = HighlightedTile.Coordinates.Y;
                    GameLoop.World.SetTile(AvailableTiles[SelectedTile], x, y);
                }
            }
        }

        private int selectedTile;
        public int SelectedTile
        {
            get => selectedTile;
            set
            {
                selectedTile = value;

                if (value > AvailableTiles.Length - 1)
                    selectedTile = 0;
                else if (value < 0)
                    selectedTile = AvailableTiles.Length - 1;
            }
        }

        public Tile[] AvailableTiles { get; } = new Tile[]
        {
            new GrassTile(new TileCoordinates()),
            new FloorTile(new TileCoordinates()),
            new WoodFloorTile(new TileCoordinates())
        };

        private EntityType _selectedEntityType;
        public EntityType SelectedEtntityType
        {
            get => _selectedEntityType;
            set
            {
                _selectedEntityType = value;

                if (value > (EntityType)Entity.ENTITY_TYPES_COUNT - 1)
                    _selectedEntityType = 0;
                else if (value < (EntityType)0)
                    _selectedEntityType = (EntityType)Entity.ENTITY_TYPES_COUNT - 1;
            }
        }

        public Tile HighlightedTile { get; set; }

        public bool BuildMode { get; set; }
    }
}
