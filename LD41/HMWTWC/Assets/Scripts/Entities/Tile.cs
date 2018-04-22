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

    public void RemovePlayer(PlayerDTO playerDto)
    {
        if (_playerDtoOnStandOne != null && playerDto.PlayerId == _playerDtoOnStandOne.PlayerId)
        {
            _playerDtoOnStandOne = null;
        }

        if (_playerDtoOnStandTwo != null && playerDto.PlayerId == _playerDtoOnStandTwo.PlayerId)
        {
            _playerDtoOnStandTwo = null;
        }
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
            _playerDtoOnStandTwo = playerDto;
            playerDto.PlayerObject.transform.position = _playerStandTwo.position;
        }
        else
        {
            _playerDtoOnStandOne = playerDto;
            playerDto.PlayerObject.transform.position = _playerStandOne.position;
        }

        if (_playerDtoOnStandOne != null && _playerDtoOnStandTwo != null)
        {
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
