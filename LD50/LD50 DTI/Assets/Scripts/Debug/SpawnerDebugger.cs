using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDebugger : MonoBehaviour
{
    public float SphereRadius = 1.0f;

    public float SphereMaxSpawnAreaRadius = 5.0f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SphereMaxSpawnAreaRadius);
    }
}
