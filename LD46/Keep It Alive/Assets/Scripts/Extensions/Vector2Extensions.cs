using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 v, float zPos = 0.0f)
        {
            return new Vector3(v.x, v.y, zPos);
        }
    }
}
