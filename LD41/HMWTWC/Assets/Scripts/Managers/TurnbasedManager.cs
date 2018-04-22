using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class TurnbasedManager : MonoBehaviour
    {
        // Players
        [SerializeField]
        private List<PlayerDTO> _playersInMatch;

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

        private UIManager _uiManager;

        private Player _player;

        void Start()
        {
            _player = ReInput.players.GetPlayer(0);
            _uiManager = GetComponent<UIManager>();

        }
        // Update is called once per frame
        void Update()
        {
            if (GameplayManager.InGame && GameplayManager.InPlacementSelection && !_finishedGame)
            {
                TimeCheck();
                InputCheck();
            }
            else if (_finishedGame && !GameplayManager.InGame)
            {
                FinishInputCheck();
            }

            if (_readyToStart && GameplayManager.LevelGenerated && !GameplayManager.InGame && !_finishedGame)
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
            var multiplier = _levelManager.GetMultiplier();
            if (!GameplayManager.InPlacementSelection)
                return;

            var playerControlled = _playersInMatch.SingleOrDefault(p => p.IsPlayerControlled);

            if (playerControlled == null)
                return;

            var selectableList = _levelManager.ShowSelectableArea(playerControlled,true);

            var selectionLocation = _player.GetAxis2D("Horizontal", "Vertical");
            var select = _player.GetButtonDown("Select");
            var back = _player.GetButtonDown("Back");

            if (selectionLocation.x != 0.0f && selectionLocation.y != 0.0f)
                _selectorObject.SetActive(true);

            var selectedX = 0;
            var selectedY = 0;
            var canSelect = false;

            if (selectionLocation.x >= 0.25f)
            {
                _selectorObject.transform.position = new Vector3(_selectorObject.transform.position.x + multiplier,
                    _selectorObject.transform.position.y, _selectorObject.transform.position.z);
                selectedX = 1;
                canSelect = true;
            }

            if (selectionLocation.x <= -0.25f)
            {
                _selectorObject.transform.position = new Vector3(_selectorObject.transform.position.x - multiplier,
                    _selectorObject.transform.position.y, _selectorObject.transform.position.z);
                selectedX = -1;
                canSelect = true;
            }


            if (selectionLocation.y >= 0.25f)
            {
                _selectorObject.transform.position = new Vector3(_selectorObject.transform.position.x,
                    _selectorObject.transform.position.y, _selectorObject.transform.position.z + multiplier);
                selectedY = 1;
                canSelect = true;
            }


            if (selectionLocation.y <= -0.25f)
            {
                _selectorObject.transform.position = new Vector3(_selectorObject.transform.position.x,
                    _selectorObject.transform.position.y, _selectorObject.transform.position.z - multiplier);
                selectedY = -1;
                canSelect = true;
            }

            if (selectionLocation.x == 0.0f)
            {
                selectedX = 0;
                if (selectedY == 0)
                {
                    _selectorObject.transform.position = playerControlled.CurrentTile.gameObject.transform.position;
                    canSelect = false;
                }
            }

            if (selectionLocation.y == 0.0f)
            {
                selectedY = 0;
                if (selectedX == 0)
                {
                    _selectorObject.transform.position = playerControlled.CurrentTile.gameObject.transform.position;
                    canSelect = false;
                }
            }

            if (select && canSelect)
            {
                 var tilePos = playerControlled.CurrentTile.TileLocationInGame();
                playerControlled.SelectNextTile(_levelManager.GetTileAtLocation((int)tilePos.x + selectedX,(int)tilePos.y + selectedY));
            }

        }

        public void StartGame(List<PlayerDTO> players)
        {
            _readyToStart = true;
            _playersInMatch = players;
            _levelManager = GetComponent<LevelManager>();
            // TODO: Allow player to set their starting location for the moment though randomize it.
            var maxSizes = _levelManager.GetCurrentSize();
            for (var p = 0; p < _playersInMatch.Count; p++)
            {
                var randomX = Random.Range(1, (int) maxSizes.x - 1);
                var randomY = Random.Range(1, (int) maxSizes.y - 1);

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

        private void TimeCheck()
        {
            if (_currentTime <= 0.0f)
            {
                GameplayManager.UpdateInPlacementSelection(false);
                StartCoroutine(TakeTurn());
                _currentTime = _maxTime;
            }
            else
            {
                _currentTime = _currentTime - GameplayManager.DeltaTime;
            }

            _uiManager.UpdateGameHud(_currentTime, _levelManager.GetCurrentSinkTime());
        }

        private void FinishGame(bool victory, string message)
        {
            _finishedGame = true;
            GameplayManager.UpdateInGame(false);
            GameplayManager.UpdateInPlacementSelection(false);

            _uiManager.ShowFinishCanvas(message);

            // TODO: Show final two remaining players and display victory UI.
        }

        private void FinishInputCheck()
        {
            var select = _player.GetButtonDown("Select");

            if (select)
                SceneManager.LoadScene(0);
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
            _levelManager.ResetSelectionVisuals();
            Debug.Log("Taking Turn...");
            foreach (var player in _playersInMatch) 
            {
                player.PlayerDebugCheck();
                if (player.SelectedTileForNextTurn != null && !player.BeenHugged && !player.CurrentTile != player.SelectedTileForNextTurn)
                {
                    if(player.IsPlayerControlled)
                        Debug.Log("Moving Player.");
                    player.Move();
                   
                }

                if (player.CurrentTile != null && player.CurrentTile.IsTileSunk())
                {
                    Debug.Log(player.FirstName + " " + player.LastName + " Hugged the water.");
                    if (player.IsPlayerControlled)
                    {
                        _finishedGame = true;
                        FinishGame(false, "It's a watery end for you " + player.FirstName + " " + player.LastName + ".");
                    }
                    player.BeenHugged = true;
                }
            }

            var playersOutOfGame = _playersInMatch.Where(p => p.BeenHugged).ToList();

            if (_playersInMatch.Count <= 2)
            {
                var player = _playersInMatch.SingleOrDefault(p => p.IsPlayerControlled);
                _finishedGame = true;
                FinishGame(true, "Congratulations!\n " + player.FirstName + " " + player.LastName + " You and one other are managed to be the last two remaining and hugged it out until the world collapsed around you.");
                yield break;
            }

            _playersInMatch = _playersInMatch.Except(playersOutOfGame).ToList();

            foreach (var player in playersOutOfGame)
            {
                // TODO: Maybe spawn particles etc?
                player.CurrentTile.TryRemovePlayer(player);
                Destroy(player.PlayerObject);
            }

            var controlledPlayer = _playersInMatch.SingleOrDefault(p => p.IsPlayerControlled);
            
            if(controlledPlayer == null)
                FinishGame(false, "You got hugged!\nBut don't fret, there could be worse ways to go.");


            GameplayManager.UpdateInPlacementSelection(true);
            StartCoroutine(SetupNextTurn());
            yield return null;
        }

    }
}
