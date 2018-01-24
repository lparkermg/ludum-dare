using UnityEngine;
using System.Collections;

public class Player_Movement_2D : MonoBehaviour {


	//Variables
	public float jumpForce = 250.0f;
	public float moveSpeed = 25.0f;
	public string axisName = "Horizontal";
	public Animator anim;

	//public float forceMultiplier = 10.0f;

	public bool isGrounded = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update(){
		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis(axisName)));
	}

	void FixedUpdate(){
		//Move Right.
		if(Input.GetKey (KeyCode.D) == true || Input.GetKey (KeyCode.RightArrow) == true){
			if(Input.GetAxis (axisName) >0)
			{
				Vector3 newScale = transform.localScale;
				newScale.x = 2.0f;
				transform.localScale = newScale;
			}
			rigidbody2D.AddForce(new Vector2(+moveSpeed,0.0f));
		}

		//Move Left
		if(Input.GetKey (KeyCode.A) == true || Input.GetKey (KeyCode.LeftArrow) == true){
			if(Input.GetAxis (axisName) <0)
			{
				Vector3 newScale = transform.localScale;
				newScale.x = -2.0f;
				transform.localScale = newScale;
			}
			rigidbody2D.AddForce (new Vector2(-moveSpeed,0.0f));
		}

		//Jump
		if(Input.GetKeyDown(KeyCode.Space) == true && isGrounded == true){
			rigidbody2D.AddForce (new Vector2(0.0f,+jumpForce));
			isGrounded = false;
		}
	}
}
