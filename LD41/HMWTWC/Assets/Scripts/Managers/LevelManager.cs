using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [SerializeField]
        private GameObject _selectableTileObject;

        // TODO: Change this to exponetially get to 0.1f or something.
        private float _levelSinkTimerMax = 0.5f;
        private float _levelSinkTimerCurrent = 0.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(GameplayManager.InGame)
                TimeCheck();
        }

        private void TimeCheck()
        {
            if (_levelSinkTimerCurrent >= _levelSinkTimerMax)
            {
                StartCoroutine(UpdateLevel());
                _levelSinkTimerCurrent = 0.0f;
            }
            else
            {
                _levelSinkTimerCurrent = _levelSinkTimerCurrent + GameplayManager.DeltaTime;
            }
        }

        public void InitialiseLevel(int xSize, int ySize, float multiplier)
        {
            GameplayManager.UpdateLevelGenerated(false);
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
                    
                }
                yield return null;
            }

            GameplayManager.UpdateLevelGenerated(true);
            yield return null;
        }

        private IEnumerator UpdateLevel()
        {
            Action2DArray(0,0,_currentXSize, _currentYSize, (x, y) =>
            {
                var shouldSink = Random.Range(0, 1000) % 10 == 1;
                if (shouldSink)
                {
                    if (!_level[x, y].IsTileSunk())
                        _level[x, y].SinkTile();
                    else
                        _level[x, y].gameObject.SetActive(false);
                }
            });
            yield return null;
        }

        private void ShowSelectableArea(Player player)
        {
            var currentTile = player.CurrentTile;
            var tileX = (int)currentTile.XLocation();
            var tileY = (int)currentTile.YLocation();
            Action2DArray(tileX - 1, tileY - 1, tileX + 1, tileY + 1, (x,y) =>
            {
                if((x >= 0 || y >= 0 || y <= _currentYSize || x <= _currentXSize || (x != tileX && y != tileY)) && !_level[x,y].IsTileSunk())
                    _level[x,y].SetSelectable(true);

            });
        }

        // TODO: Move this to the helper namespace in a static state.
        private void Action2DArray(int xStart, int yStart, int xMax, int yMax, Action<int, int> actionToRun)
        {
            for (var x = xStart; x < xMax; x++)
            {
                for (var y = yStart; y < yMax; y++)
                {
                    actionToRun.Invoke(x,y);
                }
            }
        }
    }
}
