using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public string Name;
    public Sprite ImageFront;
    public Sprite ImageBack;
    public int Value; //Loot value on LootCard, amount to divide by on a TrapCard 
    public abstract CardType Type { get; }
}
