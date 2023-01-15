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
    }

    [System.Serializable]
    public class ShardTypeToMaterial
    {
        public ShardType Type;

        public Material Material;
    }
}
