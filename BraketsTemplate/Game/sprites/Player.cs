using System.Reflection.Metadata.Ecma335;
using BraketsEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breach;

public class Player : Sprite
{
    float speed = 250;

    public Player() 
        : base("Player", new Vector2(0), "builtin/default_texture", 0, true)
    {
        this.Scale = 3.5f;
    }

    public override void Update(float dt)
    {
        if (Input.IsDown(Keys.A)) Position.X -= speed * dt;
        if (Input.IsDown(Keys.D)) Position.X += speed * dt;
        if (Input.IsDown(Keys.W)) Position.Y -= speed * dt;
        if (Input.IsDown(Keys.S)) Position.Y += speed * dt;

        Globals.Camera.Follow(this, 5);

        base.Update(dt);
    }
}