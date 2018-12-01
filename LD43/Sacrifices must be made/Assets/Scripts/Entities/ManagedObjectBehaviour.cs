using UnityEngine;

public abstract class ManagedObjectBehaviour : MonoBehaviour
{
    public abstract void StartMe(GameObject managers);
    public abstract void UpdateMe();
}