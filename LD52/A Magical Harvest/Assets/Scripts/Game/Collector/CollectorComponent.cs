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

        private AudioSource _audio;

        [SerializeField]
        private AudioClip _putClip;

        // Start is called before the first frame update
        void Start()
        {
            _audio = GetComponent<AudioSource>();
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
            if (amount <= 0)
            {
                return;
            }

            AmountCollected += amount;
            _audio.PlayOneShot(_putClip);
        }

        public void SetTimePerShard(float time)
        {
            TimePerShard = time;
        }
    }
}
