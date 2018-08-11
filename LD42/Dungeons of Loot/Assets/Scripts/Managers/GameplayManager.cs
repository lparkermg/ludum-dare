using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : ManagedObjectBehaviour
{
    [SerializeField]private int _tilesRemoved;

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

        if (isInitialSpawn)
        {
            _dungeonTiles = new List<DungeonTile>();
        }
        else
        {
            _tilesRemoved = _tilesRemoved + 1;
        }

        for (var i = 0; i < _tilesRemoved; i++)
        {
            var x = Random.Range(0, data.XSize);
            var y = Random.Range(0, data.YSize);

            if (tiles[x, y] != TileType.Empty)
            {
                tiles[x, y] = TileType.Empty;
            }
            else
            {
                i--;
            }
        }

        bool isDoor;
        Vector2 playerSpawn = Vector2.zero;
        List<ManagedObjectBehaviour> newBehaviours = new List<ManagedObjectBehaviour>();
        for (var x = 0; x < data.XSize; x++)
        {
            for (var y = 0; y < data.YSize; y++)
            {
                Sprite lootSprite = tiles[x, y] == TileType.FloorWithLoot ? _currentTheme.LootClosed : null;
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
                _dungeonTiles.First(t => t.TileX == x && t.TileY == y).SetupTile(GetBase(tiles[x,y]),tiles[x,y] == TileType.Wall,tiles[x, y] == TileType.FloorWithLoot, tiles[x, y] == TileType.Empty, isDoor,playerSpawn, lootSprite);
            }
        }
        _gameManager.AddObjects(newBehaviours);
    }

    private Sprite GetBase(TileType tileType)
    {
        switch (tileType)
        {
            case (TileType.Wall):
                return _currentTheme.Wall[Random.Range(0, _currentTheme.Wall.Length - 1)];
            case (TileType.Floor):
            case (TileType.FloorWithLoot):
                return _currentTheme.Floor[Random.Range(0, _currentTheme.Floor.Length - 1)];
            case (TileType.Door):
                return _currentTheme.Door;
            case (TileType.Empty):
                return _currentTheme.EmptySpace;
            default:
                return _currentTheme.Wall[Random.Range(0, _currentTheme.Wall.Length - 1)];
        }
    }

    public Sprite GetLootBox(bool isOpen)
    {
        return isOpen ? _currentTheme.LootOpen : _currentTheme.LootClosed;
    }
}
