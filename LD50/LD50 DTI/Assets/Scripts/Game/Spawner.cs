using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform SpawnedParent;

    public GameObject CRTPrefab;
    public GameObject NodePrefab;

    public int NodesToSpawn = 5;
    public int CRTsToSpawn = 10;
    public float MaxCRTHeight = 50f;

    public List<Transform> SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (NodesToSpawn + CRTsToSpawn > SpawnPoints.Count)
        {
            throw new ArgumentException("Nodes and CRTs cannot be more than the amount of spawn points.");
        }

        // Node Spawn
        for(var i = 0; i < NodesToSpawn; i++)
        {
            Spawn(NodePrefab, SpawnedParent, 0.0f, false);
        }

        // CRT Spawn
        for(var i = 0; i < CRTsToSpawn; i++)
        {
            Spawn(CRTPrefab, SpawnedParent, Random.Range(0.0f, MaxCRTHeight), true);
        }
    }

    private void Spawn(GameObject baseObject, Transform parent, float height, bool randomRotation)
    {
        var spawnIndex = Random.Range(0, SpawnPoints.Count - 1);
        var spawn = SpawnPoints[spawnIndex].position;
        var newObject = GameObject.Instantiate(baseObject, parent);
        var newTransform = newObject.GetComponent<Transform>();

        var rotation = newTransform.rotation;

        if (randomRotation)
        {
            rotation = Quaternion.Euler(Random.Range(0, 359.99f), Random.Range(0f, 359.99f), Random.Range(0f, 359.99f));
        }

        newTransform.SetPositionAndRotation(new Vector3(spawn.x, height, spawn.z), rotation);

        SpawnPoints.RemoveAt(spawnIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
