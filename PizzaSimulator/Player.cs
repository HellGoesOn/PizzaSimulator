using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.Enums;
using PizzaSimulator.Content.UI;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Content.World.Tiles.SubTiles;
using PizzaSimulator.Extensions;
using PizzaSimulator.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator
{
    public class Player
    {
        public event EventHandler OnLeftClick;

        public void Update()
        {
            float scrollWheelValue = InputManager.CurrentMouseState.ScrollWheelValue;
            float oldScrollWheelValue = InputManager.OldMouseState.ScrollWheelValue;

            Vector2 mousePos = InputManager.MouseScreenPosition;

            if (InputManager.HasLeftClicked && !UIManager.Instance.MouseConsumedByUI)
            {
                OnLeftClick?.Invoke(this, new EventArgs());
            }

            if (InputManager.HasRightClicked)
            {
                BuildMode = false;
                ClearLeftClickEvents();
            }

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

        public void ClearLeftClickEvents()
        {
            if (OnLeftClick != null)
            {
                foreach (var d in OnLeftClick.GetInvocationList())
                    OnLeftClick -= (d as EventHandler);
            }
        }

        public Tile Findtile(Vector2 mousePos)
        {
            TileCoordinates coords = mousePos.ToTileCoordinates();

            return GameLoop.World.GetTile(coords);
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
        public EntityType SelectedEntityType
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

        public Tile HighlightedTile => GameLoop.World.GetTile(InputManager.MouseScreenPosition.ToTileCoordinates());

        public bool BuildMode { get; set; }

        public void DoBuild(object sender, EventArgs args)
        {
            Tile tile = Findtile(InputManager.MouseScreenPosition);

            if (tile != null)
            {
                Assets.BuildSound.Play();

                TileCoordinates c = tile.Coordinates;
                GameLoop.World.SetTile(AvailableTiles[SelectedTile], c.X, c.Y);

                if (SelectedTile == (int)TileType.Floor)
                    tile.TryAddSubtile(new CashRegister());
            }    
        }

        public bool HasActions => OnLeftClick?.GetInvocationList().Length > 0;

        public void DoSpawn(object sender, EventArgs args)
        {
            EntityManager.CreateEntity(GameLoop.MyPlayer.SelectedEntityType, InputManager.MouseScreenPosition);
        }
    }
}
