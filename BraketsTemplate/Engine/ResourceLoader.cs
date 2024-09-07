using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BraketsEngine;

public enum ResourceType
{
    Texture,
    Font,
    Level,
    Sound,
    Song
}

public static class ResourceLoader
{
    public static async Task Load(ResourceType type, string filename)
    {
        string path = Globals.CurrentDir + "/content/";
        if (type is ResourceType.Texture)
        {
            path += $"images/{filename}.png";

            try
            {
                TextureManager.AddTexture(
                    new BTexture
                    {
                        name = filename.Trim(),
                        tex = Texture2D.FromFile(Globals.ENGINE_GraphicsDevice, path)
                    }
                );
            }
            catch (Exception ex)
            {
                Debug.Error($"[ContentLoader.Load] Failed to load texture! \n EX: {ex}");
            }
        }
        else if (type is ResourceType.Font)
        {
            path += $"fonts/{filename}.ttf";
            try
            {
                FontSystem _fontSys = new FontSystem();
                _fontSys.AddFont(await File.ReadAllBytesAsync(path));

                FontManager.AddFont(
                    new BFont
                    {
                        name = filename,
                        fontSystem = _fontSys
                    }
                );
            }
            catch (Exception ex)
            {
                Debug.Error($"[ContentLoader.Load] Failed to load font! \n EX: {ex}");
            }

        }
        else if (type is ResourceType.Level)
        {
            path += $"levels/{filename}.level";

            try
            {
                using StreamReader reader = new(path);
                string levelData = await reader.ReadToEndAsync();

                List<object> objects = new List<object>();
                foreach (var line in levelData.Split('\n'))
                {
                    if (line == string.Empty) 
                        continue;

                    string[] lineSplit = line.Split(" ");
                    
                    string className = lineSplit[0];
                    object obj = await ClassCreator.Create(className, []);
                    
                    float posx = float.Parse(lineSplit[1]);
                    float posy = float.Parse(lineSplit[2]);
                    Vector2 position = new Vector2(posx, posy);

                    if (obj is Sprite sp) sp.Position = position;
                    else if (obj is ParticleEmitter pe) pe.Position = position;

                    objects.Add(obj);
                }

                Level level = new Level(filename, objects);
            }
            catch (Exception ex)
            {
                Debug.Error($"[ContentLoader.Load] Failed to load level! \n EX: {ex}");
            }
        }
        else if (type is ResourceType.Sound)
        {
            path += $"sounds/{filename}.wav";

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    AudioManager.AddSound(
                        new BSound(
                            name: filename,
                            sound: SoundEffect.FromStream(fs)
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.Error($"[ContentLoader.Load] Failed to load sound! \n EX: {ex}");
            }
        }
        else if (type is ResourceType.Song)
        {
            path += $"songs/{filename}.ogg";

            try
            {
                AudioManager.AddSong(
                    new BSong(
                        name: filename,
                        song: Song.FromUri(filename, new Uri(Path.GetFullPath(path))),
                        repeat: true
                    )
                );
            }
            catch (Exception ex)
            {
                Debug.Error($"[ContentLoader.Load] Failed to load song! \n EX: {ex}");
            }
        }
        // TODO: Implement other content types
    }
}