using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : ManagedObjectBehaviour
{
    [SerializeField] private DungeonTheme[] _themes;

    public override void StartMe(GameObject managers)
    {
        
    }

    public override void UpdateMe()
    {
        
    }

    public DungeonTheme SelectDungeonTheme()
    {
        return _themes[0];
    }
}
