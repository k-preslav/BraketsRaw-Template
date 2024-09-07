using Microsoft.Xna.Framework;

namespace BraketsEngine;

public class DebugDraw
{
    public static async void DrawRect(Rectangle rect, Color color)
    {
        Globals.ENGINE_SpriteBatch.Draw(
            await TextureManager.GetTexture("builtin/_debug_draw_rect"), rect, color
        );
    }
}