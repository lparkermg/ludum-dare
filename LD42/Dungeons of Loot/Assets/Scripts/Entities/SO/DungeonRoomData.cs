using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon of Loot/Dungeon Room Data")]
public class DungeonRoomData : ScriptableObject
{
    public Texture2D Layout;
    public int MaxLoot;
    private int _xSize = 32;
    private int _ySize = 24;

    public TileType[,] LayoutToTileTypes()
    {
        var tiles = new TileType[_xSize, _ySize];

        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                tiles[x, y] = ColorToTileType(Layout.GetPixel(x, y));
            }
        }

        return tiles;
    }

    private TileType ColorToTileType(Color c)
    {
        if (c.Equals(Color.white))
        {
            return TileType.Floor;
        }

        return TileType.Wall;
    }
}