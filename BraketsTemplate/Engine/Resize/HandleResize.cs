using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsEngine;

public class HandleResize
{
    public static void Handle(GameWindow win = null)
    {
        if (win != null)
        {
            Globals.APP_Width = win.ClientBounds.Width;
            Globals.APP_Height = win.ClientBounds.Height;

            Debug.Log($"Resized window to size: {Globals.APP_Width}x{Globals.APP_Height}");
        }

        float scaleX = (float)Globals.APP_Width / Globals.APP_ViewportWidth;
        float scaleY = (float)Globals.APP_Height / Globals.APP_ViewportHeight;

        Globals.Camera.viewportScale = Math.Min(scaleX, scaleY);
    }
}
