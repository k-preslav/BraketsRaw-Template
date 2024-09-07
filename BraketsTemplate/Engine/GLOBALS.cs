using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BraketsEngine;

public static class Globals
{
    #region ENGINE SYSTEMS
    public static GraphicsDevice ENGINE_GraphicsDevice;
    public static SpriteBatch ENGINE_SpriteBatch;
    public static Main ENGINE_Main;
    #endregion

    #region APPLICATION PROPERTIES
    public static string ENGINE_Version = "1";
    public static string APP_Title = "Brakets Game";
    public static string APP_Version = "1";
    public static int APP_Width = 1280;
    public static int APP_Height = 720;
    public static int APP_ViewportWidth = 1920;
    public static int APP_ViewportHeight = 1080;
    public static bool APP_Resizable = true;
    public static bool APP_VSync = true;
    public static Color APP_ViewportColor = Color.Cyan;
    public static void LOAD_APP_P()
    {
        Debug.Log("[GLOBALS] Loading application properties...");
        CurrentDir = AppDomain.CurrentDomain.BaseDirectory;

        string[] properties = File.ReadAllLines($"{Globals.ArgsPath}/game.properties");
        foreach (var line in properties)
        {
            string[] split = line.Trim().Split(":");
            string key = split[0];      
            string value = split[1];

            switch (key)
            {
                case "engine_ver":
                    ENGINE_Version = value;
                    Debug.Log($"\t - Engine Version: {value}");
                    break;
                case "app_ver":
                    APP_Version = value;
                    Debug.Log($"\t - APP Version: {value}");
                    break;
                case "app_title":
                    APP_Title = value;
                    Debug.Log($"\t - APP Title: {value}");
                    break;
                case "app_width":
                    APP_Width = int.Parse(value);
                    Debug.Log($"\t - APP Width: {value}");
                    break;
                case "app_height":
                    APP_Height = int.Parse(value);
                    Debug.Log($"\t - APP Height: {value}");
                    break;
                case "app_view_width":
                    APP_ViewportWidth = int.Parse(value);
                    Debug.Log($"\t - APP Viewport Width: {value}");
                    break;
                case "app_view_height":
                    APP_ViewportHeight = int.Parse(value);
                    Debug.Log($"\t - APP Viewport Height: {value}");
                    break;
                case "app_view_color":
                    APP_ViewportColor = new Color(VecParser.ParseVec4(value));
                    Debug.Log($"\t - APP Viewport Color: {APP_ViewportColor}");
                    break;
                case "app_resizable":
                    APP_Resizable = bool.Parse(value);
                    Debug.Log($"\t - APP Resizable: {value}");
                    break;
                case "app_vsync":
                    APP_VSync = bool.Parse(value);
                    Debug.Log($"\t - APP VSync: {value}");
                    break;
            }
        }
    }
    #endregion

    #region APPLICATION STATUS
    public static bool STATUS_Loading = false;
    #endregion

    #region GAME
    public static Camera Camera;
    public static GameManager GameManager;
    #endregion

    #region DEBUG
    public static float DEBUG_DT = 0;
    public static float DEBUG_FPS = 0;
    public static float DEBUG_CPU_USAGE;
    public static float DEBUG_MEMORY = 0;
    public static int DEBUG_GC_CALLS = 0;
    public static int DEBUG_THREADS_COUNT = 0;
    public static bool DEBUG_Overlay;
    public static DebugUI DEBUG_UI;
    #endregion      

    #region BRIDGE
    public static BridgeClient BRIDGE_Client;
    public static bool BRIDGE_Run = false;
    public static float BRIDGE_RefreshRate = 0;
    public static string BRIDGE_Hostname = "";
    public static int BRIDGE_Port = 0;
    #endregion

    #region Paths
    public static string ArgsPath = "";
    public static string CurrentDir;
    #endregion
}
