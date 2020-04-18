using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public static class GameManager
    {
        public static float DamageMultiplier { get; private set; }

        public static int MaxWaves { get; private set; }

        public static void Initialize(float multiplier, int maxWaves)
        {
            DamageMultiplier = multiplier;
            MaxWaves = maxWaves;
        }
    }
}
