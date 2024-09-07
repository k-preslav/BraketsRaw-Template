using Microsoft.Xna.Framework.Audio;

namespace BraketsEngine;

public class BSound
{
    public string name;
    private SoundEffect sound;

    public BSound(string name, SoundEffect sound)
    {
        this.name = name;
        this.sound = sound;
    }

    public void Play(float volume)
    {
        var soundInstance = sound.CreateInstance();
        soundInstance.Volume = volume / 100.0f;
        soundInstance.Play();
    }
}