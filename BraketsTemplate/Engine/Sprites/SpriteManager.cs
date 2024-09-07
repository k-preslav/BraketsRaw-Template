using BraketsEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsTemplate.Engine.Sprites
{
    public class SpriteManager
    {
        public static List<Sprite> Sprites { get; private set; } = new List<Sprite>();

        public static void AddSprite(Sprite sp) => Sprites.Add(sp); 
        public static void RemoveSprite(Sprite sp) => Sprites.Remove(sp); 
    }
}
