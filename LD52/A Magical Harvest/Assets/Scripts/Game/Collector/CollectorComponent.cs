using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Collector
{
    public class CollectorComponent : MonoBehaviour
    {
        public ShardType Type;

        public int AmountCollected { get; private set; } = 0;

        public float TimePerShard { get; private set; } = 0.25f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
