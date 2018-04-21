using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // TODO: Add more gameplay related parts here.
        public static List<Player> GeneratePlayers(int amount)
        {
            // TODO: Generate the players themselves.

            // TODO: Setup their initial targets.

            return new List<Player>();
        }
    }
}
