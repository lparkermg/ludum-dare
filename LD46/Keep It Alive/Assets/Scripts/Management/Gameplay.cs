using LPSoft.LD46.Entities;
using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LPSoft.LD46.Management
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private SpawnLocationPair[] _spawnLocations;

        [SerializeField]
        private Element[] _availableElements;

        [SerializeField]
        private float _spawnEnemiesEvery = 20.0f;
        private int _timesSpawned = 0;

        private float _currentSpawnTimer;

        private int _currentWave = 1;

        private UI _ui;

        private void Awake()
        {
            _currentSpawnTimer = 20.0f;
            _availableElements = GameManager.EnemyElements;
            TryGetComponent(out _ui);
        }

        // Start is called before the first frame update
        void Start()
        {
            _ui.UpdateWave(_currentWave);
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentWave > GameManager.MaxWaves)
            {
                Success();
                return;
            }

            if (_currentSpawnTimer >= _spawnEnemiesEvery)
            {
                _currentSpawnTimer = 0.0f;
                _timesSpawned++;
                _spawnEnemiesEvery -= 1.0f;

                if (_timesSpawned > 3)
                {
                    _currentWave++;
                    _timesSpawned = 0;
                    _ui.UpdateWave(_currentWave);
                }

                SpawnEnemies(_timesSpawned);
            }
            else
            {
                _currentSpawnTimer += Time.deltaTime;
            }
        }

        public void Gameover()
        {
            GameManager.UpdateEndMessage("Failure", "The carriers cargo and your ship are lost forever more.");
            SceneManager.LoadScene(2);
        }

        private void Success()
        {
            if(!GameManager.Slot3Unlocked)
            {
                GameManager.UpdateEndMessage("SUCCESS!", "3rd Barrier element slot unlocked.");
                GameManager.UnlockSlot(3);
            }
            else if (!GameManager.Slot4Unlocked)
            {
                GameManager.UpdateEndMessage("SUCCESS!", "4th Barrier element slot unlocked.");
                GameManager.UnlockSlot(4);
            }
            else if (!GameManager.Slot5Unlocked)
            {
                GameManager.UpdateEndMessage("SUCCESS!", "Final Barrier element slot unlocked.");
                GameManager.UnlockSlot(5);
            }
            else
            {
                GameManager.UpdateEndMessage("SUCCESS!", "The carriers cargo was successfully delivered.");
            }

            SceneManager.LoadScene(2);
        }

        private void SpawnEnemies(int numberOfEnemies)
        {
            var availableSpawnLocations = _spawnLocations.ToList();
            for(var i = 0; i < numberOfEnemies; i++)
            {
                var toAdd = Random.Range(0, availableSpawnLocations.Count - 1);
                var spawnLocation = availableSpawnLocations[toAdd];
                var enemyGo = GameObject.Instantiate(_enemyPrefab, spawnLocation.OffScreenSpawn.position, _enemyPrefab.transform.rotation);
                var enemy = enemyGo.GetComponent<Enemy>();
                enemy.Initialize(1, _availableElements.ToList(), new List<Vector2> { spawnLocation.OffScreenSpawn.position, spawnLocation.OnScreenAttack.position }, spawnLocation.OffScreenSpawn.position, _bulletPrefab);
                availableSpawnLocations.RemoveAt(toAdd);
            }
        }
    }
}
