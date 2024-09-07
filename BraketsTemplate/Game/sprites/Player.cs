using System.Reflection.Metadata.Ecma335;
using BraketsEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Template;

public class Player : Sprite
{
    float speed = 5;

    public Player() 
        : base("Player", "builtin/default_texture", 0, true)
    {
        this.Scale = 3.5f;
    }

    public override void Update()
    {
        if (Input.IsDown(Keys.A)) Position.X -= speed;
        if (Input.IsDown(Keys.D)) Position.X += speed;
        if (Input.IsDown(Keys.W)) Position.Y -= speed;
        if (Input.IsDown(Keys.S)) Position.Y += speed;

        Globals.Camera.Follow(this, 5);

        if (Input.IsMouseClicked(0))
        {
            var particles = new ParticlesTwo();
            particles.Position = Input.GetMousePositionWorld();
            particles.Burst(100);

            Globals.Camera.Shake(5, 0.1f);
        }

        base.Update();
    }
}