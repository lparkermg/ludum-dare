using LPSoft.LD46.Entities;
using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        private float _spawnEnemiesEvery = 30.0f;
        private int _timesSpawned = 0;

        private float _currentSpawnTimer;

        private int _currentWave = 1;

        private void Awake()
        {
            _currentSpawnTimer = 20.0f;
            _availableElements = GameManager.EnemyElements;
        }

        // Start is called before the first frame update
        void Start()
        {

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

                if (_timesSpawned > 5)
                {
                    _currentWave++;
                    _timesSpawned = 0;
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
            // TODO: Replace with scene detailing gameover.
            Debug.Log("GAMEOVER!");
        }

        private void Success()
        {
            // TODO: Replace with going to scene detailing info.
            Debug.Log("SUCCESS!");
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
