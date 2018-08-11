using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : ManagedObjectBehaviour
{

    public int TileX;
    public int TileY;

    [SerializeField] private SpriteRenderer _tileBase;
    [SerializeField] private SpriteRenderer _lootVisuals;

    [SerializeField] private BoxCollider2D _collider;

    private bool _isEmptySpace;
    private bool _canPickupLoot;
    private bool _hasLoot;
    private bool _isDoor;
    private bool _alreadyCollided;
    private Vector2 _playerSpawn;

    private GameplayManager _gameplayManager;
    private DungeonManager _dungeonManager;

    public void SetupTile(Sprite tileBase, bool colliderEnabled, bool hasLoot, bool isEmptySpace, bool isDoor, Vector2 spawnLocation, Sprite lootBox = null)
    {
        _tileBase.sprite = tileBase;
        _isEmptySpace = false;
        _canPickupLoot = false;
        _isDoor = false;
        _hasLoot = false;
        _collider.enabled = false;
        _collider.isTrigger = false;
        _lootVisuals.sprite = null;

        if (isEmptySpace)
        {
            _collider.enabled = true;
            _collider.isTrigger = true;
            _isEmptySpace = true;
        }
        else if (isDoor)
        {
            _collider.enabled = true;
            _collider.isTrigger = true;
            _isDoor = true;
            _playerSpawn = spawnLocation;
        }
        else
        {
            _collider.enabled = colliderEnabled;
        }


        if (hasLoot)
        {
            _lootVisuals.sprite = lootBox;
            _collider.enabled = true;
            _collider.isTrigger = true;
            _hasLoot = true;
        }
    }

    public override void StartMe(GameObject managers)
    {
        _gameplayManager = managers.GetComponent<GameplayManager>();
        _dungeonManager = managers.GetComponent<DungeonManager>();
    }

    public override void UpdateMe()
    {
        //TODO: Any trap bits here.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_alreadyCollided)
        {
            _alreadyCollided = true;
            if (_isEmptySpace)
            {
                //TODO: Death is here.
                Debug.Log("DEATH!!");
                return;
            }

            if (_isDoor)
            {
                Debug.Log("Collided...");
                other.GetComponent<PlayerObject>().PlayerEnteredDoor(_playerSpawn);
                _gameplayManager.UpdateRoom(_dungeonManager.GetDungeonRoom());

            }

            if (_hasLoot)
            {
                Debug.Log("Can pick up loot.");
                _canPickupLoot = true;
                other.GetComponent<PlayerObject>().UpdatePickupState(_canPickupLoot,this);
            }

            _canPickupLoot = true;
            //TODO: Loot UI here.
            //TODO: 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_hasLoot)
            {
                other.GetComponent<PlayerObject>().UpdatePickupState(false);
            }
        }
        _canPickupLoot = false;
        _alreadyCollided = false;
    }

    public Loot PickupLoot()
    {
        _lootVisuals.sprite = _gameplayManager.GetLootBox(true);
        return _dungeonManager.GetLoot();
    }
}
