using System;
using System.Threading.Tasks;
using System.Transactions;
using BraketsEngine;
using Breach;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Template;
using Test;

public class GameManager
{
    Player p;

    public async void Start()
    {                   
        Globals.Camera.Center();

        LoadingScreen.Initialize();
        LoadingScreen.Show();

        p = new Player();
        new Cursor();

        new PPP();

        await LoadingScreen.Hide();                
    }

    public void Update(float dt)
    {
        // if (Input.GetMouseState().ScrollWheelValue != 0)
        // {
        //     float scroll = Input.GetMouseState().ScrollWheelValue;
        //     float zoomScroll = scroll > 1 ? scroll / 1000.0f : scroll / 100000.0f;
        //     zoomScroll = MathHelper.Clamp(zoomScroll, 0.1f, 10);
        //     Globals.Camera.SetZoom(zoomScroll);
        // }
    }

    internal void Stop()
    {
            
    }
}
