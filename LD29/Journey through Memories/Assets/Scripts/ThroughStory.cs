using UnityEngine;
using System.Collections;

public class ThroughStory : MonoBehaviour {

	public GameObject storyPart1;
	public GameObject storyPart2;
	public GameObject storyPart3;
	public GameObject storyPart4;
	public GameObject storyPart5;

	public int whichPartAt = 0;

	public int nextScene = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true){
			ChangeSprite ();
		}
	}

	void ChangeSprite(){
		switch(whichPartAt){
		case(0):
			Destroy (storyPart1);
			break;
		case(1):
			Destroy (storyPart2);
			break;
		case(2):
			Destroy (storyPart3);
			break;
		case(3):
			Destroy (storyPart4);
			break;
		case(4):
			Destroy (storyPart5);
			Application.LoadLevel (nextScene);
			break;
		}
		whichPartAt = whichPartAt +1;
	}
}
