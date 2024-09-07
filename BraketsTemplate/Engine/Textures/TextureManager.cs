using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsEngine;

public class TextureManager
{
    public static List<BTexture> Textures { get; private set; } = new List<BTexture>();

    public static async Task LoadTexture(string name)
    {
        await ResourceLoader.Load(ResourceType.Texture, name);
    }

    public static void AddTexture(BTexture tex) => Textures.Add(tex);

    public static async Task<Texture2D> GetTexture(string name)
    {
        foreach (var tex in Textures)
        {
            if (tex.name.Trim() == name.Trim())
            {
                return tex.tex;
            }
        }

        await LoadTexture(name);
        return await GetTexture(name);
    }
}
