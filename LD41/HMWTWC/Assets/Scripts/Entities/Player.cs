using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public class Player
    {
        public Guid PlayerId;
        public bool IsPlayerControlled;
        public string FirstName;
        public string LastName;
        public Player CurrentTarget;
        public bool BeenHugged;
        public Tile CurrentTile;
        public Tile SelectedTileForNextTurn;
        public GameObject PlayerObject;

        public Player(string firstName, string lastName, bool isPlayerControlled)
        {
            PlayerId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            CurrentTarget = null;
            BeenHugged = false;
            CurrentTile = null;
            SelectedTileForNextTurn = null;
            PlayerObject = null;
            IsPlayerControlled = isPlayerControlled;
           
        }

        public void PlayerDebugCheck()
        {
            var targetString = "Nope";
            if (CurrentTarget != null)
            {
                targetString = CurrentTarget.PlayerObject != null ? "Yep, with a GO..." : "Yep, but with no GO";
            }

            Debug.Log("Name: " + FirstName + " " + LastName + ", Has Target: " + targetString + ", Has CurrentTile: " + (CurrentTile != null) + ", Selected A Tile? " + (SelectedTileForNextTurn != null));
        }

        public void Move()
        {
            
            if (PlayerObject == null)
            {
                Debug.LogError("GameObject hasn't been attached to the player yet.");
                return;
            }

            var oldTile = CurrentTile;

            CurrentTile = SelectedTileForNextTurn;

            if (!CurrentTile.PlacePlayer(this))
                CurrentTile = oldTile;
            else
                oldTile.TryRemovePlayer(this);
        }

        public void SpawnPlayerObject()
        {
            PlayerObject = GameObject.Instantiate(PlayerObject, Vector3.zero, PlayerObject.transform.rotation) as GameObject;
            CurrentTile.PlacePlayer(this);
        }

        public Vector2 GetCurrentPosition()
        {
            return CurrentTile.TileLocationInGame();
        }

        public void SelectNextTile(Tile tile)
        {
            SelectedTileForNextTurn = tile;
        }

    }
}
