using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using SO;
using UnityEngine;
using UnityEngine.Experimental.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public static class GameplayManager
    {
        public static float DeltaTime
        {
            get { return Time.deltaTime; }
        }

        public static bool InGame
        {
            get { return _inGame; }
        }

        private static bool _inGame = false;

        public static bool LevelGenerated
        {
            get { return _levelGenerated; }
        }

        private static bool _levelGenerated = false;

        public static bool InPlacementSelection
        {
            get { return _inPlacementSelection; }
        }

        private static bool _inPlacementSelection = false;

        private static Names _names;

        // TODO: Change to a list once more have been made.
        private static GameObject _playerTemplates;

        public static void UpdateInGame(bool inGame)
        {
            _inGame = inGame;
        }

        public static void UpdateLevelGenerated(bool generated)
        {
            _levelGenerated = generated;
        }

        public static void UpdateInPlacementSelection(bool inSelection)
        {
            _inPlacementSelection = inSelection;
        }

        

        public static void Initialise(Names names, List<GameObject> playerTemplates)
        {
            _names = names;
            _playerTemplates = playerTemplates[0];
        }

        public static void StartGame()
        {
            var managers = GameObject.FindGameObjectWithTag("Managers");
            var players = GeneratePlayers(50);
            managers.GetComponent<LevelManager>().InitialiseLevel(50,50,2.0f);
            managers.GetComponent<TurnbasedManager>().StartGame(players);
        }

        // TODO: Add more gameplay related parts here.
        public static List<PlayerDTO> GeneratePlayers(int amount)
        {
            var players = new List<PlayerDTO>();

            for (var i = 0; i < amount; i++)
            {
                
                var firstName = _names.FirstNames[Random.Range(0, _names.FirstNames.Count - 1)];
                var lastName = _names.LastNames[Random.Range(0, _names.LastNames.Count - 1)];
                var player = new PlayerDTO(firstName, lastName, false);

                if (i == 0)
                    player.IsPlayerControlled = true;

                player.PlayerObject = _playerTemplates;
                players.Add(player);
            }

            for (var p = 0; p < players.Count - 1; p++)
            {
                var index = Random.Range(0, players.Count - 1);

                if (index == p)
                {
                    p--;
                    continue;
                }

                players[p].CurrentTarget = players[index];
            }

            return players;
        }
    }
}
