using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
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

        // TODO: Change these to a single SO data file later.
        private static List<string> _firstNames = new List<string>() {"Test"};
        private static List<string> _lastNames = new List<string>() {"McTester"};

        // TODO: Add more gameplay related parts here.
        public static List<Player> GeneratePlayers(int amount)
        {
            var players = new List<Player>();

            for (var i = 0; i < amount; i++)
            {
                var firstName = _firstNames[Random.Range(0, _firstNames.Count - 1)];
                var lastName = _lastNames[Random.Range(0, _lastNames.Count - 1)];
                var gender = (Gender) Random.Range(0, Enum.GetValues(typeof(Gender)).Length);

                players.Add(new Player(firstName, lastName, gender));
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
