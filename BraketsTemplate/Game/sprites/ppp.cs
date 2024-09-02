using Microsoft.Xna.Framework;
using BraketsEngine;

namespace Template;

public class PPP : Sprite
{
    public PPP()
    : base("new_obj", new Vector2(0, 0), "builtin/particle_default", 0, true)
    {
		
        this.drawOnLoading = false;
        this.Scale = 2f;

        Debug.Log("Hello");
    }

    public override void Update(float dt)
    {

    }
}
