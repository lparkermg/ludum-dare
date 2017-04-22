using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<PlantTypeSprites> PlantSprites;
}

[System.Serializable]
public struct PlantTypeSprites
{
    public PlantType Type;
    public Sprite Leaf;
    public Sprite Stem;
    public Sprite Flower;
}
