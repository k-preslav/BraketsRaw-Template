using Microsoft.Xna.Framework;
using BraketsEngine;
using System.Threading.Tasks;
using System.Threading;
using Template;
using Microsoft.Xna.Framework.Input;

namespace Test;

public class Cursor : Sprite
{
    ParticleEmitter cursorTrail, cursorClick;

    public Cursor()
    : base("test_tag", new Vector2(0, 0), "builtin/default_texture", 0, true)
    {
        cursorTrail = new ParticleEmitter("cursorParticles", new Vector2(0), new ParticleEmitterData
        {
            angleVariance = 16,
            lifeSpanMin = 0.4f,
            lifeSpanMax = 15.2f,
            emitCount = 1,
            sizeStartMin = 6,
            sizeStartMax = 8,
            sizeEndMin = 10,
            sizeEndMax = 12,
            interval = 0.01f,
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
            emitCount = 128,
            sizeStartMin = 6,
            sizeStartMax = 8,
            sizeEndMin = 12,
            sizeEndMax = 14,
            interval = 0.075f,
            speedMin=250,
            speedMax=400 ,
            colorStart = Color.Lime,
            colorEnd = Color.LimeGreen,
            visible = false
        }, 3);

        this.visible = false;
    }

    public override void Update(float dt)
    {
        this.Position = Vector2.Lerp(this.Position, Input.GetMousePositionWorld(), 7 * dt);
        cursorTrail.Position = this.Position;

        if (Input.IsMouseClicked(0))
        {
            var emitter = new ParticlesTwo();
            emitter.Position = this.Position;
            emitter.Burst(100);
            Globals.Camera.Shake(5, 0.1f);
        }


        if (Input.IsDown(Keys.F))
        {
            cursorTrail.ModifyParticleDataProp("textureName", "test");
        }
    }
}
