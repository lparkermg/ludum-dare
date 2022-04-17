using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SpawnableAreasHelper
{
    [MenuItem("LD50 Helpers/Generate Spawn Areas Under Selection %&n")]
    static void GenerateSpawnAreasUnderSelection()
    {
        var prefabObject = AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Spawner.prefab");

        for (var i = 0; i < 150; i++) {
            GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(prefabObject);

            if (Selection.activeTransform != null)
            {
                prefab.transform.SetParent(Selection.activeTransform, false);
            }

            prefab.transform.localPosition = GetRandomXZPosition(); // TODO: Change to random x and z later.
            prefab.transform.localEulerAngles = Vector3.zero;
            prefab.transform.localScale = Vector3.one;
            prefab.name = $"Spawner_{i}";
        }
    }

    private static Vector3 GetRandomXZPosition()
    {
        return new Vector3(Random.Range(-500f, 500f), 0.0f, Random.Range(-500f, 500f));
    }
}
