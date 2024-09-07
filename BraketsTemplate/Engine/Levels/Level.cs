using BraketsTemplate.Engine.Sprites;
using System.Collections.Generic;
using System.Linq;

namespace BraketsEngine
{
    public class Level
    {
        public string Name { get; private set; }
        public List<object> LevelObjects { get; private set; } = new List<object>();

        public Level(string name, List<object> objects)
        {
            Name = name;
            LevelObjects.AddRange(objects);

            LevelManager.AddLevel(this);
        }

        public Sprite GetSprite(string tag)
        {
            foreach (var obj in LevelObjects.Where(obj => obj is Sprite))
            {
                var sprite = obj as Sprite;
                if (sprite.Tag == tag)
                    return sprite;
            }

            Debug.Warning($"No sprite with tag '{tag}' found in level '{this.Name}'", this);
            return new Sprite();
        }

        public ParticleEmitter GetParticleEmitter(string name)
        {
            foreach (var obj in LevelObjects.Where(obj => obj is ParticleEmitter))
            {
                var pe = obj as ParticleEmitter;
                if (pe.Name == name)
                    return pe;
            }

            Debug.Warning($"No particle emitter with tag '{name}' found in level '{this.Name}'", this);
            return new ParticleEmitter("none", new(), 0);
        }

        public void Unload()
        {
            foreach (var obj in LevelObjects.ToList())
            {
                if (obj is Sprite sp)
                {
                    SpriteManager.RemoveSprite(sp);
                    LevelObjects.Remove(sp);
                }
                else if (obj is ParticleEmitter pe)
                {
                    ParticleManager.Unload(pe);
                }
            }
        }
    }
}
