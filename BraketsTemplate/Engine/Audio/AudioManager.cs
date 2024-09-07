using FontStashSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsEngine;

public class AudioManager
{
    public static List<BSong> Songs { get; private set; } = new List<BSong>();
    public static List<BSound> Sounds { get; private set; } = new List<BSound>();

    public static void AddSong(BSong song) => Songs.Add(song);
    public static void AddSound(BSound sound) => Sounds.Add(sound);

    public static BSong GetSong(string name)
    {
        foreach (var song in Songs)
        {
            if (song.name == name)
            {
                return song;
            }
        }

        ResourceLoader.Load(ResourceType.Song, name).RunSynchronously();
        return GetSong(name);
    }

    public static BSound GetSound(string name)
    {
        foreach (var sound in Sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }

        ResourceLoader.Load(ResourceType.Sound, name).RunSynchronously();
        return GetSound(name);
    }
}
