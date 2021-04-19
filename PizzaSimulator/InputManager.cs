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

        public static MouseState OldMouseState { get; private set; }

        public static Vector2 MouseScreenPosition
        {
            get
            {
                Camera camera = CameraManager.Camera;

                return Camera.ScreenToWorldSpace(Mouse.GetState().Position.ToVector2());
            }
        }
    }
}
