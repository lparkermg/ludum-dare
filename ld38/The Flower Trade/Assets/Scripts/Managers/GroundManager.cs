using System.Collections;
using System.Collections.Generic;
using Enums;
using NUnit.Framework;
using UnityEngine;

//This is going to handle the following:
//- Running the plant growth logic.
//- Showing plant visuals.
//- Showing ground visuals.
//- Player interaction.
public class GroundManager : MonoBehaviour
{
    //Plant Details
    public Plant Plant { get; private set; }
    private PlantManager _plantManager;
    private bool _collected = false;

    //Ground Visuals
    private Renderer _renderer;

    public Material PrepableMaterial;
    public Material PreppedMaterial;
    public Material PlantedMaterial;
    public Material CollectableMaterial;

    //Ground Details
    private LandStage _landStage;

    private float _prepResetTime = 60.0f;
    private float _currentTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<Renderer>();
	    _plantManager = GetComponent<PlantManager>();
	    _landStage = LandStage.Prepable; //TODO: Change when loading stuff is complete.
	}
	
	// Update is called once per frame
	void Update ()
	{
	    RunChecks();
	}

    private void RunChecks()
    {
        CheckPrep();
        if (Plant != null && _landStage != LandStage.Collectable))
        {
            GrowPlant();
        }
    }

    #region Plant Specific Commants
    public Plant CollectPlant()
    {
        if (Plant.Stage != PlantStage.Flower)
            return null;

        return Plant;
    }

    public void RemovePlant()
    {
        if (Plant != null && _collected)
            Plant = null;
    }

    public void PlaceSeed(Plant plant)
    {
        if(_landStage == LandStage.Prepped)
        Plant = plant;
        _renderer.material = PlantedMaterial;
    }

    private void GrowPlant()
    {
        Plant.Grow(true, () =>
        {
            _plantManager.UpdatePlantVisuals(Plant);
        }, SetCollectable);
    }
    #endregion

    #region Ground Specific Commands
    private void ResetGround()
    {
        _landStage = LandStage.Prepable;
        _renderer.material = PrepableMaterial;
    }

    public void PrepGroud()
    {
        if (_landStage == LandStage.Prepable)
        {
            _landStage = LandStage.Prepped;
            _renderer.material = PreppedMaterial;
        }
    }

    private void SetCollectable()
    {
        _landStage = LandStage.Collectable;
        _renderer.material = CollectableMaterial;
    }

    private void CheckPrep()
    {
        if (_landStage == LandStage.Prepped)
        {
            if (_currentTime >= _prepResetTime)
                ResetGround();

            _currentTime = Time.deltaTime;
        }
        else
        {
            _currentTime = 0.0f;
        }
    }
    #endregion
}
