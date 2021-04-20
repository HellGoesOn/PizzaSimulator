using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.Entities;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Helpers;

namespace PizzaSimulator
{
    public static class InputManager
    {
        public static void Update()
        {
            OldMouseState = Mouse.GetState();
            OldKeyboardState = Keyboard.GetState();
        }

        public static bool HasLeftClicked =>
            CurrentMouseState.LeftButton == ButtonState.Pressed
            && OldMouseState.LeftButton == ButtonState.Released;

        public static bool HasRightClicked =>
            CurrentMouseState.RightButton == ButtonState.Pressed
            && OldMouseState.RightButton == ButtonState.Released;

        public static bool IsKeyHeld(Keys k) => Keyboard.GetState().IsKeyDown(k);

        public static bool IsKeyPressed(Keys k) => CurrentKeyboardState.IsKeyDown(k) && !OldKeyboardState.IsKeyDown(k);

        public static MouseState CurrentMouseState => Mouse.GetState();
        public static MouseState OldMouseState { get; private set; }

        public static KeyboardState CurrentKeyboardState => Keyboard.GetState();
        public static KeyboardState OldKeyboardState { get; private set; }

        public static Vector2 MouseScreenPosition
        {
            get
            {
                return ScreenToWorldSpace(Mouse.GetState().Position.ToVector2());
            }
        }

        public static Vector2 MouseWorldPosition => CurrentMouseState.Position.ToVector2();

        public static Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(CameraManager.Camera.Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
