using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Tile : MonoBehaviour
{ 
    [SerializeField]
    private Vector2 _tileLocationInGame;
    [SerializeField]
    private Transform _playerStandOne;
    [SerializeField]
    private Transform _playerStandTwo;
    private bool _isPlayerOnTile;
    private PlayerDTO _playerDtoOnStandOne;
    private PlayerDTO _playerDtoOnStandTwo;

    [SerializeField]
    private GameObject _selectableArea;

    private bool _initiallySelected;

    private int _amountTillSunk = 5;

    public bool IsTileSunk()
    {
        return _amountTillSunk <= 0;
    }

    public float XLocationInGame()
    {
        return _tileLocationInGame.x;
    }

    public float YLocationInGame()
    {
        return _tileLocationInGame.y;
    }

    public Vector2 TileLocationInGame()
    {
        return _tileLocationInGame;
    }

    public bool HasBeenSelected()
    {
        return _initiallySelected;
    }

    public void InitiallySelectTile()
    {
        _initiallySelected = true;
    }

    public void SetSelectable(bool selectable)
    {
        _selectableArea.SetActive(selectable);
    }

    public void Initialise(int x, int y, float multiplier)
    {
        _tileLocationInGame = new Vector2(x,y);
    }

    public bool TryRemovePlayer(PlayerDTO playerDto)
    {
        if (_playerDtoOnStandOne != null && playerDto.PlayerId == _playerDtoOnStandOne.PlayerId)
        {
            _playerDtoOnStandOne = null;
            return true;
        }

        if (_playerDtoOnStandTwo != null && playerDto.PlayerId == _playerDtoOnStandTwo.PlayerId)
        {
            _playerDtoOnStandTwo = null;
            return true;
        }

        return false;
    }

    public bool PlacePlayer(PlayerDTO playerDto)
    {
        if (_playerDtoOnStandOne != null && _playerDtoOnStandTwo != null)
        {
            Debug.Log("Two players on this tile (" + _tileLocationInGame.x + ", " + _tileLocationInGame.y + ")");
            return false;
        }

        if (_playerDtoOnStandOne != null)
        {
            // TODO: Logic on if the hug is instant or if you wait a turn
            _playerDtoOnStandTwo = playerDto;
            playerDto.PlayerObject.transform.position = _playerStandTwo.position;
            // TODO: Update player game object.
        }
        else
        {
            _playerDtoOnStandOne = playerDto;
            playerDto.PlayerObject.transform.position = _playerStandOne.position;
            // TODO: Update player game object.
        }

        if (_playerDtoOnStandOne != null && _playerDtoOnStandTwo != null)
        {
            Debug.Log(_playerDtoOnStandOne.FirstName + " " + _playerDtoOnStandOne.LastName + " just got hugged by " + _playerDtoOnStandTwo.FirstName + " " + _playerDtoOnStandTwo.LastName);
            _playerDtoOnStandOne.BeenHugged = true;
        }

        return true;
    }

    public bool SinkTile()
    {
        _amountTillSunk--;

        if (_amountTillSunk <= 0)
            return true;

        transform.position = new Vector3(transform.position.x, _amountTillSunk / 10f,transform.position.z);

        return false;
    }
}
