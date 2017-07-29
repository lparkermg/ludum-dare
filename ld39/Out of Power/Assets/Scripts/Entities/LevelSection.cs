using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : ScriptableObject
{
	public LevelSections Section;
	

}

public enum LevelSections
{
	Start,
	Main,
	Finish
}