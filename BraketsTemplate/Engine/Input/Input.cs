using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace BraketsEngine;

public class Input
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;

    static MouseState currentMouseState;
    static MouseState previousMouseState;

    public static KeyboardState GetKeyboardState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }
    public static MouseState GetMouseState()
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        return currentMouseState;
    }

    public static bool IsDown(Keys key)
    {
        return currentKeyState.IsKeyDown(key);
    }

    public static bool IsPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }

    public static Vector2 GetMousePositionScreen()
    {
        Point p = currentMouseState.Position;
        return new Vector2(p.X, p.Y);
    }
    public static Vector2 GetMousePositionWorld()
    {
        MouseState mouseState = Mouse.GetState();
        Point screenPosition = mouseState.Position;
        
        Vector2 mousePositionWorld = new Vector2(
            (screenPosition.X - (Globals.APP_Width / 2)) / Globals.Camera.Zoom + Globals.Camera.TargetPosition.X,
            (screenPosition.Y - (Globals.APP_Height / 2)) / Globals.Camera.Zoom + Globals.Camera.TargetPosition.Y
        );

        return mousePositionWorld;
    }
    public static bool IsMouseDown(int index)
    {
        if (currentMouseState.LeftButton == ButtonState.Pressed && index == 0)
            return true;
        else if (currentMouseState.RightButton == ButtonState.Pressed && index == 1)
            return true;

        return false;
    }
    public static bool IsMouseClicked(int index)
    {
        if (index == 0)
            return previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed;
        else if (index == 1)
            return previousMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed;

        return false;
    }
}