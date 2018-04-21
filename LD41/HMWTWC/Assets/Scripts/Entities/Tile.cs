using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

// TODO: Make the actual tiles.
public class Tile : MonoBehaviour
{ 
    private Vector2 _tileLocation;
    [SerializeField]
    private Transform _playerStandOne;
    [SerializeField]
    private Transform _playerStandTwo;
    private bool _isPlayerOnTile;
    private Player _playerOnStandOne;
    private Player _playerOnStandTwo;

    private int _amountTillSunk = 5;

    public void Initialise(int x, int y, float multiplier)
    {
        _tileLocation = new Vector2(x * multiplier,y * multiplier);
    }

    public bool PlacePlayer(Player player)
    {
        if (_playerOnStandOne != null && _playerOnStandTwo != null)
            return false;

        if (_playerOnStandOne != null)
        {
            // TODO: Logic on if the hug is instant or if you wait a turn
            _playerOnStandTwo = player;
            // TODO: Update player game object.
        }
        else
        {
            _playerOnStandOne = player;
            // TODO: Update player game object.
        }

        if (_playerOnStandOne != null && _playerOnStandTwo != null)
        {
            _playerOnStandOne.BeenHugged = true;
        }

        return true;
    }

    public bool SinkTile()
    {
        _amountTillSunk--;

        if (_amountTillSunk <= 0)
            return true;

        transform.position = new Vector3(_tileLocation.x, _amountTillSunk / 10f,_tileLocation.y);

        return false;
    }
}
