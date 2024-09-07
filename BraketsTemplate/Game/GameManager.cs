using BraketsEngine;
using Microsoft.Xna.Framework.Input;

public class GameManager
{
    public async void Start()
    {                   
        Globals.Camera.Center();
        Globals.Camera.BackgroundColor = Globals.APP_ViewportColor;

        LoadingScreen.Initialize();
        LoadingScreen.Show();

        await LevelManager.LoadLevel("test");

        await LoadingScreen.Hide();                
    }

    public async void Update()
    {
        // if (Input.GetMouseState().ScrollWheelValue != 0)
        // {
        //     float scroll = Input.GetMouseState().ScrollWheelValue;
        //     float zoomScroll = scroll > 1 ? scroll / 1000.0f : scroll / 100000.0f;
        //     zoomScroll = MathHelper.Clamp(zoomScroll, 0.1f, 10);
        //     Globals.Camera.SetZoom(zoomScroll);
        // }

        if (Input.IsPressed(Keys.U))
        {
            LevelManager.UnloadLevel("test");
        }
        if (Input.IsPressed(Keys.R))
        {
            await LevelManager.ReloadLevel("test");
        }
    }

    internal void Stop()
    {
            
    }
}
