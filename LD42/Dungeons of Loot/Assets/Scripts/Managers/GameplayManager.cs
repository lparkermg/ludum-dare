using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : ManagedObjectBehaviour
{
    private int _tilesRemoved = 0;

    private DungeonManager _dungeonManager;

    //TODO: Change to DungeonTile once implemented
    private List<GameObject> _dungeonTiles;
    
    public override void StartMe(GameObject managers)
    {
        _dungeonManager = managers.GetComponent<DungeonManager>();
        //TODO: Get all dungeon tiles into the _dungeonTiles list.
        //TODO: Setup starting intial dungeon based on randomly selected room from the manager.
    }

    public override void UpdateMe()
    {
        //TODO: Player input checking here.
    }
}
