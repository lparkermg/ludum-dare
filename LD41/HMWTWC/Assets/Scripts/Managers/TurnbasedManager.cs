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


        // Update is called once per frame
        void Update()
        {
            if (_inPlacementSelection && GameplayManager.InGame)
                TimeCheck();
        }

        public bool CanSelectTile()
        {
            return _inPlacementSelection;
        }

        public void StartGame(List<Player> players)
        {
            GameplayManager.UpdateInGame(true);
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

        private IEnumerator TakeTurn()
        {
            foreach (var player in _playersInMatch)
            {
                if (player.SelectedTileForNextTurn != null && !player.BeenHugged)
                {
                    player.Move();
                    yield return null;
                }
            }

            var playersOutOfGame = _playersInMatch.Where(p => p.BeenHugged).ToList();
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
