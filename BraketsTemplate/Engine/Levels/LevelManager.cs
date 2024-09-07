using BraketsEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsEngine;

public class LevelManager
{
    public static List<Level> Levels = new List<Level>(); 

    public static async Task LoadLevel(string name)
    {
        await ResourceLoader.Load(ResourceType.Level, name);
    }

    public static void AddLevel(Level level)
    {
        Levels.Add(level);
    }

    public static Level GetLevel(string name)
    {
        foreach (var level in Levels)
        {
            if (level.Name == name)
                return level;
        }

        Debug.Error($"[LevelManager.GetLevel] No level of name '{name}' was found!");
        return null;
    }

    public static void UnloadLevel(string name)
    {
        var level = GetLevel(name);
        if (level is null)
        {
            Debug.Warning("[LevelManager.UnloadLevel] Can Not unload level as it is not loaded!");
            return;
        }

        level.Unload();
        Levels.Remove(level);
    }

    public static async Task ReloadLevel(string name, bool showLoadingScreen=true)
    {
        if (showLoadingScreen)
        {
            LoadingScreen.Show();
        }

        UnloadLevel(name);
        await LoadLevel(name);

        await LoadingScreen.Hide();
    }
}
