using Microsoft.Xna.Framework;
using BraketsEngine;

namespace Template;

public class ParticlesTwo : ParticleEmitter
{
    public ParticlesTwo()
    : base("ParticlesTwo", new Vector2(0, 0), new ParticleEmitterData().FromFile(@"testParticles"), 0)
    {
		
		
    }
}
