using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Collector
{
    public class CollectorComponent : MonoBehaviour
    {
        public ShardType Type;

        public int AmountCollected { get; private set; } = 0;

        public float TimePerShard { get; private set; } = 0.25f;

        [SerializeField]
        private Image _collectorDisplay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitialiseCollector(Sprite typeSprite)
        {
            _collectorDisplay.sprite = typeSprite;
            AmountCollected = 0;
            TimePerShard = 0.25f;
        }

        public void PutInCollector(int amount)
        {
            AmountCollected += amount;
            // TODO: Update GameManager to add time.
        }

        public void SetTimePerShard(float time)
        {
            TimePerShard = time;
        }
    }
}
