using Microsoft.Xna.Framework;
using BraketsEngine;

namespace Template;

public class TrailEmitters : Sprite
{
    ParticleEmitter trails;
    float timer;

    public TrailEmitters()
    : base("trail_emitter", new Vector2(0, 0), "builtin/default_texture", 0, true)
    {
        trails = new ParticleEmitter("click2", new Vector2(0), new ParticleEmitterData
        {
            angleVariance = 360,
            lifeSpanMin = 1f,
            lifeSpanMax = 2f,
            emitCount = 8,
            sizeStartMin = 6,
            sizeStartMax = 8,
            sizeEndMin = 12,
            sizeEndMax = 14,
            interval = 0.075f,
            speedMin=100,
            speedMax=200,
            colorStart = Color.Cyan,
            colorEnd = Color.Red,
            visible = false
        }, 3);

        this.visible = false;
    }

    public override void Update(float dt)
    {
        trails.Position = this.Position;
        if (timer > 0)
            timer += dt;

        if (timer > 0.5f)
            DestroySelf();
    }

    public void Burst()
    {
        trails.Burst(100);
        timer = 0.1f;
    }
}
