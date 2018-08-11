using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon of Loot/Dungeon Theme")]
public class DungeonTheme : ScriptableObject
{
    public string Name;
    public Sprite[] Floor;
    public Sprite EmptySpace;
    public Sprite LootClosed;
    public Sprite LootOpen;
    public Sprite[] Wall;
    public Sprite Door;
}
