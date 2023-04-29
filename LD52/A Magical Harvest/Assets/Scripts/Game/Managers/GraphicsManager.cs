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

        [SerializeField]
        private List<ShardTypeToColor> _availableColors;

        [SerializeField]
        private List<ShardTypeToTrailMaterial> _availableTrailMaterials;

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

        public Color GetColor(ShardType type) => _availableColors.First(c => c.Type == type).Color;


        public Material GetTrail(ShardType type) => _availableTrailMaterials.First(t => t.Type == type).Material;
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

    [System.Serializable]
    public class ShardTypeToColor
    {
        public ShardType Type;

        public Color Color;
    }

    [System.Serializable]
    public class ShardTypeToTrailMaterial
    {
        public ShardType Type;

        public Material Material;
    }
}
