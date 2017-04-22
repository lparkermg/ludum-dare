using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="LD38/Plant")]
public class Plant : PlantTemplate {

    public override PlantType Type
    {
        get { return _type; }
    }
    private PlantType _type;

    public override PlantRarity Rarity
    {
        get { return _rarity; }
    }
    private PlantRarity _rarity;

    public override PlantStage Stage
    {
        get{ return _stage; }
    }
    private PlantStage _stage;

    public override Color FlowerColour
    {
        get { return _flowerColour; }
    }
    private Color _flowerColour;

    public override Color StemColour
    {
        get { return _stemColour; }
    }
    private Color _stemColour;

    public override Color LeafColour
    {
        get { return _leafColour; }
    }
    private Color _leafColour;

    private float _timeCurrentStage; //TODO: Add calculation based on Rarity + Type.
    private float _currentTime;

    public override void Initialize(PlantType type, PlantRarity rarity, Color? flowerColour = null, Color? stemColour = null,Color? leafColour = null)
    {
        _type = type;
        _rarity = rarity;
        _flowerColour = flowerColour.HasValue ? flowerColour.Value : GenerateColour();
        _stemColour = stemColour.HasValue ? stemColour.Value : GenerateColour();
        _leafColour = leafColour.HasValue ? leafColour.Value : GenerateColour();

        _stage = PlantStage.Seed;
    }

    public override void Grow(bool isLight, Action nextGrowStage, Action complete)
    {
        //TODO: if there's time implement lightening/darkening of the plant colour based on isLight.
        //We don't need to grow anymore.
        if (_stage == PlantStage.Flower) return;

        if (_currentTime >= _timeCurrentStage)
        {
            IncrementPlantStage();
            nextGrowStage.Invoke();
            if (_stage == PlantStage.Flower)
            {
                complete.Invoke();
                return;
            }

            CalculateTime();
            _currentTime = 0.0f;
        }
        else
        {
            //TODO: Change to GameManager DeltaTime when implemented.
            _currentTime += Time.deltaTime;
        }
    }

    private void IncrementPlantStage()
    {
        var stageInt = (int) _stage;
        stageInt++;
        _stage = (PlantStage)stageInt;
    }

    private void CalculateTime()
    {
        var time = 0.0f;

        switch (_rarity)
        {
            case PlantRarity.VeryCommon:
                time = 1.0f * 30.0f;
                break;
            case PlantRarity.Common:
                time = 2.0f * 30.0f;
                break;
            case PlantRarity.Rare:
                time = 4.0f * 30.0f;
                break;
            case PlantRarity.VeryRare:
                time = 8.0f * 30.0f;
                break;
            case PlantRarity.Legendary:
                time = 12.0f * 30.0f;
                break;
            default:
                Debug.LogError("Null or wrong Rarity type.");
                break;
        }

        switch (_type)
        {
            case PlantType.Type0:
            case PlantType.Type1:
            case PlantType.Type2:
            case PlantType.Type3:
            case PlantType.Type4:
            case PlantType.Type5:
                time += 1.0f * 15.0f;
                break;
            default:
                Debug.LogError("Null or wrong Plant Type.");
                break;
        }

        _timeCurrentStage = time;
    }

    private Color GenerateColour()
    {
        var r = Random.Range(0.05f, 0.9f);
        var g = Random.Range(0.05f, 0.9f);
        var b = Random.Range(0.05f, 0.9f);

        return new Color(r,g,b);
    }
}
