using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : ManagedObjectBehaviour
{
    [SerializeField] private DungeonRoomData[] _dungeonRooms;
    [SerializeField] private Loot[] _availableLoot;

    [SerializeField] private GameObject _tilePrefab;

    public override void StartMe(GameObject managers){}

    public override void UpdateMe(){}

    public DungeonRoomData GetDungeonRoom()
    {
        //TODO: Get a random dungeon room
        return _dungeonRooms[Random.Range(0,_dungeonRooms.Length)];
    }

    public GameObject GetTilePrefab()
    {
        return _tilePrefab;
    }

    public Loot GetLoot()
    {
        return _availableLoot[Random.Range(0,_availableLoot.Length)];
    }
}
