using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<PlantTypeSprites> PlantSprites;
    public List<Plant> SpawnablePlants;
    private List<Plant> _shopSeeds;

    public AudioClip PickupPlantSfx;
    public AudioClip PlaceSeedSfx;
    public AudioClip PlantGrowSfx;
    public AudioClip PrepSfx;
    public AudioClip PlantCompleteSfx;

    public int SpawnableSeedPool = 100;
    public int ShopSeedPool = 100;

    void Start()
    {
        GenerateSpawnablePlants();
        GenerateShopSeeds();
    }

    private void GenerateSpawnablePlants()
    {
        SpawnablePlants = new List<Plant>();

        for (int i = 0; i < SpawnableSeedPool; i++)
        {
            var plant = ScriptableObject.CreateInstance<Plant>();
            var type = Random.Range(0, 4);
            var rarity = Random.Range(0, 2);
            plant.Initialize((PlantType)type,(PlantRarity)rarity);

            SpawnablePlants.Add(plant);
        }
    }

    private void GenerateShopSeeds()
    {
        _shopSeeds = new List<Plant>();

        for (int i = 0; i < ShopSeedPool; i++)
        {
            var plant = ScriptableObject.CreateInstance<Plant>();
            var type = Random.Range(2, 6);
            var rarity = Random.Range(2, 4);
            plant.Initialize((PlantType)type,(PlantRarity)rarity);
            _shopSeeds.Add(plant);
        }
    }

    public Plant GetRandomShopSeed()
    {
        return _shopSeeds[Random.Range(0, _shopSeeds.Count)];
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
