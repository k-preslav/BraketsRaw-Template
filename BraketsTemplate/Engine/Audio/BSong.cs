using Microsoft.Xna.Framework.Media;
namespace BraketsEngine;

public class BSong
{
    public string name;
    public bool repeat = true;
    private Song song;

    public BSong(string name, Song song, bool repeat)
    {
        this.name = name;
        this.song = song;
        this.repeat = repeat;
    }

    public void Play()
    {
        MediaPlayer.Stop();
        MediaPlayer.Play(song);
    }
    public void Pause() => MediaPlayer.Pause();
    public void Stop() => MediaPlayer.Stop();
    public void SetVolume(float value) => MediaPlayer.Volume = value / 100.0f;
}
