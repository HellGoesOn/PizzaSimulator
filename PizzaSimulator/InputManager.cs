using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PizzaSimulator.Content.Components;

namespace PizzaSimulator
{
    public static class InputManager
    {
        public static void Update()
        {
            OldMouseState = Mouse.GetState();
        }

        public static bool HasLeftClicked =>
            Mouse.GetState().LeftButton == ButtonState.Pressed
            && OldMouseState.LeftButton == ButtonState.Released;

        public static bool HasRightClicked =>
            Mouse.GetState().RightButton == ButtonState.Pressed
            && OldMouseState.RightButton == ButtonState.Released;

        public static MouseState OldMouseState { get; private set; }

        public static Vector2 MouseScreenPosition
        {
            get
            {
                return ScreenToWorldSpace(Mouse.GetState().Position.ToVector2());
            }
        }

        public static Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(CameraManager.Camera.Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
