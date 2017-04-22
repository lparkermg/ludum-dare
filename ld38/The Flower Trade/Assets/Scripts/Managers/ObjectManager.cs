using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<PlantTypeSprites> PlantSprites;
    public List<Plant> SpawnablePlants;

    public AudioClip PickupPlantSfx;
    public AudioClip PlaceSeedSfx;
    public AudioClip PlantGrowSfx;
    public AudioClip PrepSfx;
    public AudioClip PlantCompleteSfx;

    public int InitalSpawnableAmount = 10;

    void Start()
    {
        GenerateSpawnablePlants();
    }

    private void GenerateSpawnablePlants()
    {
        SpawnablePlants = new List<Plant>();

        for (int i = 0; i < InitalSpawnableAmount; i++)
        {
            var plant = ScriptableObject.CreateInstance<Plant>();
            var type = Random.Range(0, 6);
            var rarity = Random.Range(0, 2);
            plant.Initialize((PlantType)type,(PlantRarity)rarity);

            SpawnablePlants.Add(plant);
        }
    }
}

[System.Serializable]
public struct PlantTypeSprites
{
    public PlantType Type;
    public Sprite Leaf;
    public Sprite Stem;
    public Sprite Flower;
}
