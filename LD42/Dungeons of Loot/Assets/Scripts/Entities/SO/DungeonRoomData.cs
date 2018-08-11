using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon of Loot/Dungeon Room Data")]
public class DungeonRoomData : ScriptableObject
{
    public Texture2D Layout;
    public int MaxLoot;
    public int XSize => 30;
    public int YSize => 17;

    public TileType[,] LayoutToTileTypes()
    {
        var tiles = new TileType[XSize, YSize];

        for (var x = 0; x < XSize; x++)
        {
            for (var y = 0; y < YSize; y++)
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

        if (c.Equals(Color.blue))
        {
            return TileType.Door;
        }

        return TileType.Wall;
    }
}