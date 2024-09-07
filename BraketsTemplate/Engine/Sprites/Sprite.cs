using BraketsTemplate.Engine.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace BraketsEngine;

public class Sprite
{
    public string Tag = "default";
    public Vector2 Position = Vector2.Zero;
    public float Rotation = 0;
    public float Scale = 1;
    public int Layer = 0;
    public float Opacity = 1;
    public Color Tint = Color.White;
    public bool visible = true;
    public bool drawHitbox = false;
    public bool drawOnLoading = false;
    public Rectangle Rect;

    protected SpriteEffects _effects;

    internal Texture2D texture;
    internal string textureName;

    internal bool overrideDraw = false;

    public Sprite(string tag="none", string texName="builtin/default_texture", int layer=0, bool auto_load=true)
    {
        this.Tag = tag;
        this.textureName = texName;
        this.Layer = layer;

        if (auto_load)
            Load();
    }

    public async void Load()
    {
        this.texture = await TextureManager.GetTexture(textureName);
        SpriteManager.AddSprite(this);
    }

#pragma warning disable CS1998
public virtual async Task Init() { }
#pragma warning restore CS1998

    public virtual void Update() { }
    public virtual void UpdateRect()
    {
        if (texture is null)
            return;

        this.Rect = new Rectangle(
            new Point((int)(this.Position.X - this.texture.Width * Scale / 2), (int)(this.Position.Y - this.texture.Height * Scale / 2)),
            new Point((int)(texture.Width * Scale), (int)(texture.Height * Scale))
        );
    }

    public void Draw()
    {
        if (!visible || texture is null || overrideDraw)
            return;

        Globals.ENGINE_SpriteBatch.Draw(
            texture, this.Position, null, this.Tint * this.Opacity,
            this.Rotation, new Vector2(texture.Width / 2, texture.Height / 2),
            this.Scale, this._effects, 0
        );

        if (this.drawHitbox)
        {
            DebugDraw.DrawRect(this.Rect, Color.Green);
        }
    }

    public Sprite HitsGet(string tag)
    {
        foreach (var sprite in SpriteManager.Sprites.ToList())
        {
            try
            {
                if (this.Rect.Intersects(sprite.Rect))
                {
                    if (sprite.Tag == tag)
                    {
                        return sprite;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Warning($"Failed to check for hit! EX: {ex}", this);
            }
        }

        return null;
    }

    public bool Hits(string tag)
    {
        return HitsGet(tag) is Sprite s;
    }

    public void DestroySelf()
    {
        SpriteManager.RemoveSprite(this);
    }
}