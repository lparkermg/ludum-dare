using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public string Name;
    public Sprite FaceImage;
    public string Description;
}
