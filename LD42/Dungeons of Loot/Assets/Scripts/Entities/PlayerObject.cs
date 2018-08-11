using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObject : ManagedObjectBehaviour
{
    private Player _player;
    private Rigidbody2D _rb;

    private bool _canPickupLoot;
    private DungeonTile _currentTile = null;
    private List<Loot> _inventory;

    [SerializeField] private float _speedMultiplier = 5;

    public override void StartMe(GameObject managers)
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = ReInput.players.GetPlayer(0);
        _inventory = new List<Loot>();
    }

    public override void UpdateMe()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        var move = _player.GetAxis2D("LeftRight", "UpDown");

        if (_canPickupLoot && _player.GetButtonDown("Select") && _currentTile != null)
        {
            PickupLoot(_currentTile.PickupLoot());
        }

        if (_player.GetButtonDown("Quit"))
        {
            PlayerPrefs.SetInt("GoldFromRun", _inventory.Select(i => i.Value).Sum());
            SceneManager.LoadScene(2);
        }

        if (move.x == 0.0f && move.y == 0.0f)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.AddForce(move * _speedMultiplier,ForceMode2D.Impulse);
        }
    }

    public void PlayerEnteredDoor(Vector2 newPlayerPos)
    {
        _rb.MovePosition(newPlayerPos);
        _rb.velocity = Vector2.zero;
    }

    public void UpdatePickupState(bool canPickUp, DungeonTile tile = null)
    {
        _currentTile = tile;
        _canPickupLoot = canPickUp;
    }

    private void PickupLoot(Loot loot)
    {
        _inventory.Add(loot);

        if (_inventory.Count >= 11)
        {
            _inventory.RemoveAt(0);
        }
        Debug.Log(_inventory.Count);
    }
}
