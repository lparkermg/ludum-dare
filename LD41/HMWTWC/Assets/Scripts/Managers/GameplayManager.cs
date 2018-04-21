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

        private static bool _inGame;

        public static void UpdateInGame(bool inGame)
        {
            _inGame = inGame;
        }

        private static Names _names;

        public static void Initialise(Names names)
        {
            _names = names;
        }

        // TODO: Add more gameplay related parts here.
        public static List<Player> GeneratePlayers(int amount)
        {
            var players = new List<Player>();

            for (var i = 0; i < amount; i++)
            {
                var firstName = _names.FirstNames[Random.Range(0, _names.FirstNames.Count - 1)];
                var lastName = _names.LastNames[Random.Range(0, _names.LastNames.Count - 1)];

                players.Add(new Player(firstName, lastName));
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
