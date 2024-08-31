using Microsoft.Xna.Framework;
using BraketsEngine;
using System.Threading.Tasks;
using System.Threading;

namespace Test;

public class Cursor : Sprite
{
    ParticleEmitter cursorTrail, cursorClick;

    public Cursor()
    : base("test_tag", new Vector2(0, 0), "builtin/default_texture", 0, true)
    {
        cursorTrail = new ParticleEmitter("cursorParticles", new Vector2(0), new ParticleEmitterData
        {
            angleVariance = 8,
            lifeSpanMin = 0.4f,
            lifeSpanMax = 15.2f,
            emitCount = 2,
            sizeStartMin = 12,
            sizeStartMax = 16,
            sizeEndMin = 20,
            sizeEndMax = 24,
            interval = 0.045f,
            speedMin=25,
            speedMax=50 ,
            colorStart = Color.LightGreen,
            colorEnd = Color.LimeGreen,
            visible = true
        }, 2);

        cursorClick = new ParticleEmitter("click", new Vector2(0), new ParticleEmitterData
        {
            angleVariance = 360,
            lifeSpanMin = 1f,
            lifeSpanMax = 3.5f,
            emitCount = 64,
            sizeStartMin = 16,
            sizeStartMax = 20,
            sizeEndMin = 24,
            sizeEndMax = 28,
            interval = 0.075f,
            speedMin=250,
            speedMax=400 ,
            colorStart = Color.Lime,
            colorEnd = Color.LimeGreen,
            visible = false
        }, 3);
    }

    public override void Update(float dt)
    {
        this.Position = Vector2.Lerp(this.Position, Input.GetMousePositionWorld(), 7 * dt);
        cursorTrail.Position = this.Position;

        if (Input.IsMouseClicked(0))
        {
            cursorClick.Position = this.Position;   
            cursorClick.Burst(100);
        }
    }
}
