using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerObject : ManagedObjectBehaviour
{
    private Player _player;
    private Rigidbody2D _rb;

    private bool _canPickupLoot;
    private DungeonTile _currentTile = null;
    private List<Loot> _inventory;
    private float _currentPitch = 1.0f;

    [SerializeField] private AudioClip _footstepClip;
    private AudioSource _playerAudio;

    [SerializeField] private float _speedMultiplier = 5;
    private ParticleSystem _movementParticles;

    [SerializeField] private Text _inventoryText;

    public override void StartMe(GameObject managers)
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementParticles = GetComponent<ParticleSystem>();
        _playerAudio = GetComponent<AudioSource>();
        _player = ReInput.players.GetPlayer(0);
        _inventory = new List<Loot>();
    }

    public override void UpdateMe()
    {
        CheckInput();
    }

    private void PlaySound()
    {
        if (!_playerAudio.isPlaying)
        {
            _playerAudio.PlayOneShot(_footstepClip);
        }
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
            if (_movementParticles.isPlaying)
            {
                _movementParticles.Clear();
                _movementParticles.Stop();
            }
        }
        else
        {
            PlaySound();
            _rb.AddForce(move * _speedMultiplier,ForceMode2D.Impulse);
            if(!_movementParticles.isPlaying)
                _movementParticles.Play();
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

        UpdatePickupState(false);
        _inventoryText.text = $"Inventory Value: {_inventory.Select(i => i.Value).Sum()} Gold Coins";
    }
}
