using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public static class GameManager
    {
        public static float DamageMultiplier { get; private set; }

        public static int MaxWaves { get; private set; }

        public static Element Slot1Element { get; private set; }

        public static Element Slot2Element { get; private set; }

        public static Element? Slot3Element { get; private set; }

        public static bool Slot3Unlocked { get; private set; }

        public static Element? Slot4Element { get; private set; }

        public static bool Slot4Unlocked { get; private set; }

        public static Element? Slot5Element { get; private set; }

        public static bool Slot5Unlocked { get; private set; }

        public static Element[] EnemyElements { get; private set; }

        public static string EndMessage { get; private set; }

        public static string SubMessage { get; private set; }

        public static void Initialize(float multiplier)
        {
            DamageMultiplier = multiplier;
        }

        public static void SetupGame(int waves, Element slot1, Element slot2, Element? slot3, Element? slot4, Element? slot5, Element[] enemyElements)
        {
            MaxWaves = waves;
            Slot1Element = slot1;
            Slot2Element = slot2;
            Slot3Element = slot3;
            Slot4Element = slot4;
            Slot5Element = slot5;
            EnemyElements = enemyElements;
        }

        public static void UpdateEndMessage(string endMessage, string subMessage)
        {
            EndMessage = endMessage;
            SubMessage = subMessage;
        }

        public static void UnlockSlot(int slot)
        {
            switch(slot)
            {
                case 3:
                    Slot3Unlocked = true;
                    break;
                case 4:
                    Slot4Unlocked = true;
                    break;
                case 5:
                    Slot5Unlocked = true;
                    break;
                default:
                    break;
            }

            var newLockedSlots = "";

            if (!Slot3Unlocked)
            {
                newLockedSlots += "3,";
            }

            if (!Slot4Unlocked)
            {
                newLockedSlots += "4,";
            }

            if (!Slot5Unlocked)
            {
                newLockedSlots += "5";
            }

            PlayerPrefs.SetString("LockedSlots", newLockedSlots);
        }
    }
}
