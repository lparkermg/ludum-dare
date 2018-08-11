﻿using System.Collections;
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

    public void SetupTile(Sprite tileBase, bool colliderEnabled, bool hasLoot, bool isEmptySpace, Sprite lootBox = null)
    {
        _tileBase.sprite = tileBase;
        _isEmptySpace = false;
        if (isEmptySpace)
        {
            _collider.enabled = true;
            _collider.isTrigger = true;
            _isEmptySpace = true;
        }

        _collider.enabled = colliderEnabled;
        if (hasLoot)
        {
            _lootVisuals.sprite = lootBox;
            _collider.isTrigger = true;
        }
    }

    public override void StartMe(GameObject managers)
    {

    }

    public override void UpdateMe()
    {
        //TODO: Any trap bits here.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEmptySpace)
        {
            //TODO: Death is here.
            return;
        }

        _canPickupLoot = true;
        //TODO: Loot UI here.

    }

    void OnTriggerExit2D(Collider2D other)
    {
        _canPickupLoot = false;
    }
}
