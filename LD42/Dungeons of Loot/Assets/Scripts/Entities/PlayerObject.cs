using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerObject : ManagedObjectBehaviour
{
    private Player _player;
    private Rigidbody2D _rb;

    [SerializeField] private float _speedMultiplier = 5;

    public override void StartMe(GameObject managers)
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = ReInput.players.GetPlayer(0);
    }

    public override void UpdateMe()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        var move = _player.GetAxis2D("LeftRight", "UpDown");

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
}
