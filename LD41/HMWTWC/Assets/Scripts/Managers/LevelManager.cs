using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using ProBuilder.Core;
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
        private float _levelSinkTimerMax = 10f;
        private float _levelSinkTimerCurrent = 0.0f;

        private bool _sinkInProgress = false;

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
            if (_levelSinkTimerCurrent <= 0.0f && !_sinkInProgress)
            {
                StartCoroutine(UpdateLevel());
            }
            else
            {
                _levelSinkTimerCurrent = _levelSinkTimerCurrent - GameplayManager.DeltaTime;
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

        public float GetMultiplier()
        {
            return _multiplier;
        }

        public float GetCurrentSinkTime()
        {
            return _levelSinkTimerCurrent;
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
            }

            GameplayManager.UpdateLevelGenerated(true);
            yield return null;
        }

        private IEnumerator UpdateLevel()
        {
            _sinkInProgress = true;
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

            if (_levelSinkTimerMax > 0.5f)
            {
                _levelSinkTimerMax = _levelSinkTimerMax / 1.1f;
                _levelSinkTimerCurrent = _levelSinkTimerMax;
            }

            _sinkInProgress = false;
            yield return null;
        }

        public List<Vector2> ShowSelectableArea(PlayerDTO playerDto, bool showParticles = false)
        {
            var selectableArea = new List<Vector2>();
            var currentTile = playerDto.CurrentTile;
            var tileX = (int)currentTile.XLocationInGame();
            var tileY = (int)currentTile.YLocationInGame();
            Action2DArray(tileX - 1, tileY - 1, tileX + 1, tileY + 1, (x,y) =>
            {
                if ((x >= 0 || y >= 0 || y <= _currentYSize || x <= _currentXSize || (x != tileX || y != tileY)) &&
                    !_level[x, y].IsTileSunk())
                {
                    selectableArea.Add(new Vector2((x - tileX)/2.0f,(y - tileY)/2.0f));
                    if(showParticles)
                        _level[x, y].SetSelectable(true);
                }
            });

            return selectableArea;
        }

        public Tile SelectNextTile(int currentX, int currentY, int targetX, int targetY,
            bool divideByMultiplier = true)
        {
            Dictionary<Tile, int> tileScoreX = new Dictionary<Tile, int>();
            Dictionary<Tile, int> tileScoreY = new Dictionary<Tile, int>();

            var divideBy = divideByMultiplier ? _multiplier : 1.0f;

            Action2DArray(currentX - 1, currentY - 1, currentX + 1, currentY + 1, (x, y) =>
            {
                //Debug.Log("X: " + x + ", Y: " + y + ", currentYSize: " + _currentYSize + ", currentXSize: " + _currentXSize + ", currentX: " + currentX + ", currentY: " + currentY);
                if ((x >= 0 && y >= 0 && y <= _currentYSize - 1 && x <= _currentXSize - 1 &&
                     (x != currentX || y != currentY)) && !_level[x, y].IsTileSunk())
                {
                    tileScoreX.Add(_level[x,y], x - targetX);
                    tileScoreY.Add(_level[x,y], y - targetY);
                }
            });

            var tileX = tileScoreX.OrderBy(s => s.Value).FirstOrDefault();

            var tileY = tileScoreY.OrderBy(s => s.Value).FirstOrDefault();
            if (tileX.Key == null)
            {
                Debug.Log("TileX is null (" + currentX + ", " + currentY + ", " + targetX + ", " + targetY + ")");
            }

            if (tileY.Key == null)
            {
                Debug.Log("TileY is null (" + currentX + ", " + currentY + ", " + targetX + ", " + targetY + ")");
            }
            
            return tileX.Value <= tileY.Value ? tileX.Key : tileY.Key;
        }

        public Vector2 GetCurrentSize()
        {
            return new Vector2(_currentXSize,_currentYSize);
        }

        public bool TrySetInitialSpawn(int x, int y)
        {
            if (_level[x, y].HasBeenSelected())
                return false;

            _level[x,y].InitiallySelectTile();
            return true;
        }

        public Tile GetTileAtLocation(int x, int y)
        {
            return _level[x, y];
        }

        public void ResetSelectionVisuals()
        {
            Action2DArray(0,0,_currentXSize,_currentYSize, (x, y) =>
            {
                _level[x,y].SetSelectable(false);
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
