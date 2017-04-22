using System;
using UnityEngine;
using Enums;

//Base SO to handle growing and typing of the plant.
public abstract class PlantTemplate : ScriptableObject
{
    public string Name; //Potentially generated on the fly based on Type + Rarity and something.
    public abstract PlantType Type { get; } //Determines the Sprites used
    public abstract PlantRarity Rarity { get; } //Grab growth time based on rarity.
    public abstract PlantStage Stage { get; } //Readonly Stage of growth.
    public abstract Color FlowerColour { get; } //Determined by growth variables.
    public abstract Color StemColour { get; } //Determined by growth variables.
    public abstract Color LeafColour { get; } //Determined by growth variables.

    public abstract void Initialize(PlantType type, PlantRarity rarity,Color? flowerColour = null, Color? stemColour = null,Color? leafColour = null);
    //Maybe expand on this to if theres time to take in some ground details.
    public abstract void Grow(bool isLight,Action nextGrowStage, Action complete);
}
