using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using UnityEngine;

namespace Managers
{
    public class TurnbasedManager : MonoBehaviour
    {
        // Players
        private List<Player> _playersInMatch;

        // Turn countdown.
        [SerializeField] private float _maxTime = 5.0f;

        private float _currentTime = 0.0f;

        private bool _inPlacementSelection = false;

        private bool _finishedGame = false;

        private bool _readyToStart = false;

        [SerializeField]
        private GameObject _selectorObject;

        //Other Managers
        private LevelManager _levelManager;

        void Start()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            if (_inPlacementSelection && GameplayManager.InGame && GameplayManager.InPlacementSelection)
                TimeCheck();

            if (_readyToStart && GameplayManager.LevelGenerated && !GameplayManager.InGame)
                GameplayManager.UpdateInGame(true);
        }

        public bool CanSelectTile()
        {
            return _inPlacementSelection;
        }

        private void InputCheck()
        {
            var horiz = 0.0f;
            var vert = 0.0f;


        }

        public void StartGame(List<Player> players)
        {
            _readyToStart = true;
            _playersInMatch = players;
            _levelManager = GetComponent<LevelManager>();
            // TODO: Allow player to set their starting location for the moment though randomize it.
            var maxSizes = _levelManager.GetCurrentSize();
            for (var p = 0; p < _playersInMatch.Count - 1; p++)
            {
                var randomX = Random.Range(0, (int) maxSizes.x);
                var randomY = Random.Range(0, (int) maxSizes.y);

                if (!_levelManager.TrySetInitialSpawn(randomX, randomY))
                {
                    p--;
                    continue;
                }

                _playersInMatch[p].CurrentTile = _levelManager.GetTileAtLocation(randomX, randomY);
                _playersInMatch[p].SpawnPlayerObject();
            }
        }

        private void EndGame()
        {
            GameplayManager.UpdateInGame(false);
        }

        private void TimeCheck()
        {
            if (_currentTime >= _maxTime)
            {
                _inPlacementSelection = false;
                StartCoroutine(TakeTurn());
                _currentTime = 0.0f;
            }
            else
            {
                _currentTime = _currentTime + GameplayManager.DeltaTime;
            }
        }

        private void FinishGame()
        {
            GameplayManager.UpdateInGame(false);

            // TODO: Show final two remaining players and display victory UI.
        }

        private IEnumerator TakeTurn()
        {
            foreach (var player in _playersInMatch) 
            {
                if (player.SelectedTileForNextTurn != null && !player.BeenHugged)
                {
                    player.Move();
                    yield return null;
                }

                if (player.CurrentTile.IsTileSunk())
                {
                    player.BeenHugged = true;
                }
            }

            var playersOutOfGame = _playersInMatch.Where(p => p.BeenHugged).ToList();

            if (playersOutOfGame.Count == 2 && _playersInMatch.Count == 2)
            {
                _finishedGame = true;
                FinishGame();
                yield break;
            }

            _playersInMatch = _playersInMatch.Except(playersOutOfGame).ToList();

            foreach (var player in playersOutOfGame)
            {
                // TODO: Maybe spawn particles etc?
                Destroy(player.PlayerObject);
                yield return null;
            }

            // TODO: Duplicate code... Merge into one area later on.
            for (var p = 0; p < _playersInMatch.Count - 1; p++)
            {
                if (_playersInMatch[p].CurrentTarget == null)
                {
                    var index = Random.Range(0, _playersInMatch.Count - 1);

                    if (index == p)
                    {
                        p--;
                        continue;
                    }

                    _playersInMatch[p].CurrentTarget = _playersInMatch[index];
                }
            }

            _inPlacementSelection = true;
            yield return null;
        }

    }
}
