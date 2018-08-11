using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : ManagedObjectBehaviour
{
    private DungeonRoomData[] _dungeonRooms;

    public override void StartMe(GameObject managers){}

    public override void UpdateMe(){}

    public DungeonRoomData GetDungeonRoom()
    {
        //TODO: Get a random dungeon room
        return _dungeonRooms[0];
    }
}
