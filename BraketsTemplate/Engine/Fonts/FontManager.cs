using FontStashSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BraketsEngine;

public class FontManager
{
    public static List<BFont> Fonts { get; private set; } = new List<BFont>();

    public static void AddFont(BFont font) => Fonts.Add(font);

    public static async Task<SpriteFontBase> GetFont(string name, int size)
    {
        foreach (var font in Fonts)
        {
            if (font.name == name)
            {
                return font.fontSystem.GetFont(size);
            }
        }

        await ResourceLoader.Load(ResourceType.Font, name);
        return await GetFont(name, size);
    }
}
