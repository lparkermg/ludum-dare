using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : ManagedObjectBehaviour
{
    private int _tilesRemoved = 0;

    private DungeonManager _dungeonManager;
    private VisualManager _visualManager;
    private GameManager _gameManager;

    private DungeonTheme _currentTheme;

    [SerializeField] private List<DungeonTile> _dungeonTiles;
    
    public override void StartMe(GameObject managers)
    {
        _dungeonManager = managers.GetComponent<DungeonManager>();
        _visualManager = managers.GetComponent<VisualManager>();
        _gameManager = managers.GetComponent<GameManager>();

        _currentTheme = _visualManager.SelectDungeonTheme();
        UpdateRoom(_dungeonManager.GetDungeonRoom(),true);
    }

    public override void UpdateMe()
    {
        
    }

    public void UpdateRoom(DungeonRoomData data, bool isInitialSpawn = false)
    {
        var tiles = data.LayoutToTileTypes();
        //TODO: Add Loot generation.
        if (isInitialSpawn)
        {
            _dungeonTiles = new List<DungeonTile>();
        }

        bool isDoor;
        Vector2 playerSpawn = Vector2.zero;
        List<ManagedObjectBehaviour> newBehaviours = new List<ManagedObjectBehaviour>();
        for (var x = 0; x < data.XSize; x++)
        {
            for (var y = 0; y < data.YSize; y++)
            {
                if (isInitialSpawn)
                {
                    GameObject go = GameObject.Instantiate(_dungeonManager.GetTilePrefab(), new Vector3(x, y),
                        _dungeonManager.GetTilePrefab().transform.rotation) as GameObject;
                    var behaviour = go.GetComponent<ManagedObjectBehaviour>();
                    newBehaviours.Add(behaviour);
                    behaviour.StartMe(gameObject);
                    var tile = go.GetComponent<DungeonTile>();
                    tile.TileX = x;
                    tile.TileY = y;
                    _dungeonTiles.Add(tile);
                }

                isDoor = tiles[x, y] == TileType.Door;
                if (isDoor)
                {
                    if (x == 0)
                    {
                        playerSpawn = new Vector2(data.XSize - 2, y);
                    }
                    else if (x == data.XSize - 1)
                    {
                        playerSpawn = new Vector2(1,y);
                    }
                    else if (y == 0)
                    {
                        playerSpawn = new Vector2(x,data.YSize -2);
                    }
                    else if (y == data.YSize - 1)
                    {
                        playerSpawn = new Vector2(x, 1);
                    }
                }
                _dungeonTiles.First(t => t.TileX == x && t.TileY == y).SetupTile(GetBase(tiles[x,y]),tiles[x,y] == TileType.Wall,false,false,isDoor,playerSpawn);
            }
        }
        _gameManager.AddObjects(newBehaviours);
    }

    private Sprite GetBase(TileType tileType)
    {
        switch (tileType)
        {
            case (TileType.Wall):
                return _currentTheme.Wall;
            case (TileType.Floor):
                return _currentTheme.Floor;
            case (TileType.Door):
                return _currentTheme.Door;
            default:
                return _currentTheme.Wall;
        }
    }
}
