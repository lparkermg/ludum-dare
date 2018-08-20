using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon of Loot/Loot Item")]
public class Loot : ScriptableObject
{
    public string Name;
    public int Value;
    public Sprite Icon;

}
