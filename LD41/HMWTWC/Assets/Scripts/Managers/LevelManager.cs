using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        private Tile[,] _level;

        private int _currentXSize;
        private int _currentYSize;
        private float _multiplier;

        private bool _generatingLevel;

        [SerializeField] private GameObject[] _availableTiles;

        // Use this for initialization
        void Start()
        {
            // TODO: Remove on full implementation.
            InitialiseLevel(5,5,2.25f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitialiseLevel(int xSize, int ySize, float multiplier)
        {
            _generatingLevel = true;
            _level = new Tile[xSize, ySize];
            _currentXSize = xSize;
            _currentYSize = ySize;
            _multiplier = multiplier;
            StartCoroutine(BuildLevel());
        }

        private IEnumerator BuildLevel()
        {
            for (var x = 0; x < _currentXSize; x++)
            {
                for (var y = 0; y < _currentYSize; y++)
                {
                    var tileGO = _availableTiles[Random.Range(0, _availableTiles.Length)];
                    var placedTile = GameObject.Instantiate(tileGO, new Vector3(x * _multiplier, 0.5f, y * _multiplier),
                        tileGO.transform.rotation) as GameObject;
                    var tile = placedTile.GetComponent<Tile>();
                    tile.Initialise(x, y, _multiplier);
                    _level[x, y] = tile;
                    yield return null;
                }
            }

            _generatingLevel = false;
            yield return null;
        }
    }
}
