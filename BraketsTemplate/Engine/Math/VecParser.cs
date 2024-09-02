using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraketsEngine
{
    public class VecParser
    {
        public static Vector4 ParseVec4(string value)
        {
            value = value.Trim('<', '>');

            // Split by spaces, assuming the format "1. 0. 0. 1"
            string[] components = value.Split(". ", StringSplitOptions.RemoveEmptyEntries);

            return new Vector4(
                float.Parse(components[0]),
                float.Parse(components[1]),
                float.Parse(components[2]),
                float.Parse(components[3])
            );
        }
        public static Vector3 ParseVec3(string value)
        {
            value = value.Trim('<', '>');

            // Split by spaces, assuming the format "1. 0. 0"
            string[] components = value.Split(". ", StringSplitOptions.RemoveEmptyEntries);

            return new Vector3(
                float.Parse(components[0]),
                float.Parse(components[1]),
                float.Parse(components[2])
            );
        }
    }
}