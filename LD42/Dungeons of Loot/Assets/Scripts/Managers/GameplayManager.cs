using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : ManagedObjectBehaviour
{
    private int _tilesRemoved = 0;

    private DungeonManager _dungeonManager;
    private VisualManager _visualManager;

    private DungeonTheme _currentTheme;
    //TODO: Change to DungeonTile once implemented
    [SerializeField] private List<DungeonTile> _dungeonTiles;
    
    public override void StartMe(GameObject managers)
    {
        _dungeonManager = managers.GetComponent<DungeonManager>();
        _visualManager = managers.GetComponent<VisualManager>();
        //TODO: Get all dungeon tiles into the _dungeonTiles list.
        _currentTheme = _visualManager.SelectDungeonTheme();
        //TODO: Setup starting intial dungeon based on randomly selected room from the manager.
        UpdateRoom(_dungeonManager.GetDungeonRoom(),true);
    }

    public override void UpdateMe()
    {
        //TODO: Player input checking here.
    }

    private void UpdateRoom(DungeonRoomData data, bool isInitialSpawn = false)
    {
        var tiles = data.LayoutToTileTypes();
        //TODO: Add Loot generation.
        if (isInitialSpawn)
        {
            _dungeonTiles = new List<DungeonTile>();
        }

        for (var x = 0; x < data.XSize; x++)
        {
            for (var y = 0; y < data.YSize; y++)
            {
                if (isInitialSpawn)
                {
                    GameObject go = GameObject.Instantiate(_dungeonManager.GetTilePrefab(), new Vector3(x, y),
                        _dungeonManager.GetTilePrefab().transform.rotation) as GameObject;
                    var tile = go.GetComponent<DungeonTile>();
                    tile.TileX = x;
                    tile.TileY = y;
                    _dungeonTiles.Add(tile);
                }

                _dungeonTiles.First(t => t.TileX == x && t.TileY == y).SetupTile(GetBase(tiles[x,y]),tiles[x,y] == TileType.Wall,false,false);
            }
        }
    }

    private Sprite GetBase(TileType tileType)
    {
        switch (tileType)
        {
            case (TileType.Wall):
                return _currentTheme.Wall;
            case (TileType.Floor):
                return _currentTheme.Floor;
            default:
                return _currentTheme.Wall;
        }
    }
}
