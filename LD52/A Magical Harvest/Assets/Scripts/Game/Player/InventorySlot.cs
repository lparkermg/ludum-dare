using Game.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [System.Serializable]
    public class InventorySlot
    {
        public ShardType Type;

        public Sprite Image;

        public int Amount;
    }
}
