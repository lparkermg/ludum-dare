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
        [SerializeField]
        private List<Player> _playersInMatch;

        // Turn countdown.
        [SerializeField] private float _maxTime = 5.0f;

        [SerializeField] private float _currentTime = 0.0f;

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
            if (GameplayManager.InGame && GameplayManager.InPlacementSelection)
                TimeCheck();

            if (_readyToStart && GameplayManager.LevelGenerated && !GameplayManager.InGame)
            {
                GameplayManager.UpdateInGame(true);
                GameplayManager.UpdateInPlacementSelection(true);
            }
        }

        public bool CanSelectTile()
        {
            return GameplayManager.InPlacementSelection;
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
            for (var p = 0; p < _playersInMatch.Count; p++)
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

            StartCoroutine(SetupNextTurn());
        }

        private void EndGame()
        {
            GameplayManager.UpdateInGame(false);
        }

        private void TimeCheck()
        {
            if (_currentTime >= _maxTime)
            {
                GameplayManager.UpdateInPlacementSelection(false);
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
            GameplayManager.UpdateInPlacementSelection(false);
            Debug.Log("Victory");

            // TODO: Show final two remaining players and display victory UI.
        }

        /*void OnDrawGizmos()
        {
            foreach (var player in _playersInMatch)
            {
                if (player.CurrentTarget != null && player.PlayerObject != null)
                {
                    Gizmos.DrawLine(player.PlayerObject.transform.position, player.CurrentTarget.PlayerObject.transform.position );
                }
            }
        }*/

        private IEnumerator SetupNextTurn()
        {
            Debug.Log("Setting up next turn...");
            for (var p = 0; p < _playersInMatch.Count - 1; p++)
            {
                if (_playersInMatch[p].CurrentTarget == null || _playersInMatch[p].CurrentTarget.PlayerObject == null)
                {
                    var index = Random.Range(0, _playersInMatch.Count - 1);

                    if (index == p)
                    {
                        continue;
                    }

                    _playersInMatch[p].CurrentTarget = _playersInMatch[index];
                }
            }

            foreach (var player in _playersInMatch)
            {
                
                if (!player.IsPlayerControlled)
                {
                    // Get current target position.
                    if (player.CurrentTarget != null)
                    {
                        var targetLocation = player.CurrentTarget.CurrentTile.TileLocationInGame();
                        var playerLocation = player.CurrentTile.TileLocationInGame();
                        // Select a tile + or - 1 around the current position
                        var tile = _levelManager.SelectNextTile((int) playerLocation.x, (int) playerLocation.y,
                            (int) targetLocation.x,
                            (int) targetLocation.y);
                        // Set as next selected tile.
                        player.SelectedTileForNextTurn = tile;
                    }
                }
                player.PlayerDebugCheck();

                yield return null;
            }
        }

        private IEnumerator TakeTurn()
        {
            Debug.Log("Taking Turn...");
            foreach (var player in _playersInMatch) 
            {
                player.PlayerDebugCheck();
                if (player.SelectedTileForNextTurn != null && !player.BeenHugged)
                {
                    Debug.Log("Moving Player.");
                    player.Move();
                   
                }

                if (player.CurrentTile != null && player.CurrentTile.IsTileSunk())
                {
                    Debug.Log(player.FirstName + " " + player.LastName + " Hugged the water.");
                    player.BeenHugged = true;
                }
            }

            var playersOutOfGame = _playersInMatch.Where(p => p.BeenHugged).ToList();

            if (_playersInMatch.Count <= 2)
            {
                _finishedGame = true;
                FinishGame();
                yield break;
            }

            _playersInMatch = _playersInMatch.Except(playersOutOfGame).ToList();

            foreach (var player in playersOutOfGame)
            {
                // TODO: Maybe spawn particles etc?
                player.CurrentTile.TryRemovePlayer(player);
                Destroy(player.PlayerObject);
            }

            // TODO: Duplicate code... Merge into one area later on.
           

            GameplayManager.UpdateInPlacementSelection(true);
            StartCoroutine(SetupNextTurn());
            yield return null;
        }

    }
}
