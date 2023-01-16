using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Managers
{
    public class GraphicsManager : MonoBehaviour
    {
        [SerializeField]
        private List<ShardTypeToMaterial> _availableMaterials;

        [SerializeField]
        private Sprite[] _growthStages;

        [SerializeField]
        private List<ShardTypeToSprite> _availableSprites;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Material GetMaterial(ShardType type)
        {
            return _availableMaterials.First(m => m.Type == type).Material;
        }

        public Sprite GetSprite(ShardType type)
        {
            return _availableSprites.First(s => s.Type == type).Sprite;
        }

        public Sprite[] GrowthStages() => _growthStages;
    }

    [System.Serializable]
    public class ShardTypeToMaterial
    {
        public ShardType Type;

        public Material Material;
    }

    [System.Serializable]
    public class ShardTypeToSprite
    {
        public ShardType Type;

        public Sprite Sprite;
    }
}
