using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Player
    {
        public string FirstName;
        public string LastName;
        public Gender Gender;
        public Player CurrentTarget;
        public bool BeenHugged;
        public Tile CurrentTile;
        public Tile SelectedTileForNextTurn;
        public GameObject PlayerObject;

        public Player(string firstName, string lastName, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            CurrentTarget = null;
            BeenHugged = false;
            CurrentTile = null;
            SelectedTileForNextTurn = null;
            PlayerObject = null;
        }

        public void Move()
        {
            if (PlayerObject == null)
            {
                Debug.LogError("GameObject hasn't been attached to the player yet.");
                return;
            }

            CurrentTile = SelectedTileForNextTurn;
            // TODO: Move the player to current tile and run checks.
        }
    }

    public enum Gender
    {
        Male,
        Female,
        MtF,
        FtM,
        Other
    }
}
