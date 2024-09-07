using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BraketsEngine;

namespace Template;

public class Test : Sprite
{
    public Test()
    : base("new_obj", "builtin/default_texture", 0, true) { }
	
	public override async Task Init()
	{
		this.Tint = Color.Aqua;
		
		
	}

    public override void Update()
    {

    }
}
