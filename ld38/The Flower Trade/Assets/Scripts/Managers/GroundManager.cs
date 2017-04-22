using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

//This is going to handle the following:
//- Running the plant growth logic.
//- Showing plant visuals.
//- Showing ground visuals.
//- Player interaction.
public class GroundManager : MonoBehaviour
{
    //Gameplay Managers
    private ObjectManager _objectManager;

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

    //Seed Spawning
    private bool _seedSpawner = false;
    private bool _containsSeed = false;
    private Plant _seed;

    private float _timeToNextSpawn = 0.0f;
    private float _currentSpawnTime = 0.0f;

    //Audio
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<Renderer>();
	    _plantManager = GetComponent<PlantManager>();
	    _audioSource = GetComponent<AudioSource>();
	    _objectManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectManager>();
	    _landStage = LandStage.Prepable; //TODO: Change when loading stuff is complete.
	    //_timeToNextSpawn = Random.Range(120.0f, 241.0f);
	    if (_landStage == LandStage.Prepable)
	        _seedSpawner = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    RunChecks();
	}

    private void RunChecks()
    {
        if(_seedSpawner)
            CheckSeedSpawning();
        CheckPrep();
        if (Plant != null && _landStage != LandStage.Collectable)
        {
            GrowPlant();
        }
    }

    public Plant TryAction(Plant plant,bool pickupSeed, Action planted)
    {
        Plant plantToReturn = null;
        if (!pickupSeed)
        {

            if (_landStage == LandStage.Prepable)
            {
                _audioSource.PlayOneShot(_objectManager.PrepSfx);
                PrepGround();
            }
            else if (_landStage == LandStage.Prepped && plant != null)
            {
                _audioSource.PlayOneShot(_objectManager.PlaceSeedSfx);
                PlaceSeed(plant);
                planted.Invoke();
            }
            else if (_landStage == LandStage.Collectable)
            {
                _audioSource.PlayOneShot(_objectManager.PickupPlantSfx);
                plantToReturn = CollectPlant();
                RemovePlant();
                _plantManager.ResetPlant();
                ResetGround();
            }
        }
        else
        {
            if (_containsSeed)
            {
                _containsSeed = false;
                plantToReturn = _seed;
            }
        }

        return plantToReturn;
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
        if (_landStage == LandStage.Prepped)
        {
            Plant = plant;
            _landStage = LandStage.Planted;
            _renderer.material = PlantedMaterial;
        }
    }

    private void GrowPlant()
    {
        Plant.Grow(true, () =>
        {
            _plantManager.UpdatePlantVisuals(Plant);
            _audioSource.PlayOneShot(_objectManager.PlantGrowSfx);
        }, SetCollectable);
    }
    #endregion

    #region Ground Specific Commands
    private void ResetGround()
    {
        _landStage = LandStage.Prepable;
        _renderer.material = PrepableMaterial;
        _seedSpawner = true;
        _seed = null;
        _containsSeed = false;
        _currentSpawnTime = 0.0f;
    }

    public void PrepGround()
    {
        if (_landStage == LandStage.Prepable)
        {
            _landStage = LandStage.Prepped;
            _renderer.material = PreppedMaterial;
            _seedSpawner = false;
            _seed = null;
            _containsSeed = false;
            _currentSpawnTime = 0.0f;
        }
    }

    private void SetCollectable()
    {
        _audioSource.PlayOneShot(_objectManager.PlantCompleteSfx);
        _landStage = LandStage.Collectable;
        _renderer.material = CollectableMaterial;
    }

    private void CheckPrep()
    {
        if (_landStage == LandStage.Prepped)
        {
            if (_currentTime >= _prepResetTime)
                ResetGround();

            _currentTime += Time.deltaTime;
        }
        else
        {
            _currentTime = 0.0f;
        }
    }

    private void CheckSeedSpawning()
    {
        if (!_containsSeed)
        {
            if (_currentSpawnTime >= _timeToNextSpawn)
            {
                _containsSeed = true;
                _seed = _objectManager.SpawnablePlants[Random.Range(0, _objectManager.SpawnablePlants.Count)];
                _currentSpawnTime = 0.0f;
                _timeToNextSpawn = Random.Range(120.0f, 241.0f);
                //TODO:display an indicator or something...
            }
            else
            {
                //TODO: Change to GameManager delta time when done.
                _currentSpawnTime += Time.deltaTime;
            }
        }
    }
    #endregion
}
