using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class MagicShard : ScriptableObject
    {
        public string Name { get; set; }

        public int Type { get; set; }

        public Sprite Image { get; set; }
    }
}
