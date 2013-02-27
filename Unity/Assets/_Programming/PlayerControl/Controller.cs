using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	private Vector3 velocity;
	//private bool jumping;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(OnGround());
		velocity = rigidbody.velocity;
		Movement();
		rigidbody.velocity = velocity;
	}
	
	private void Movement()
	{
		velocity.x = 5.0f;
		
		if(Input.GetKey(KeyCode.Space))
		{
			if(OnGround())
			{
				velocity.y = 6.0f;
			}
		}
		
		//velocity *= 0.8f;
	}
	
	private bool OnGround()
	{
		if (Physics.Raycast(transform.position, -Vector3.up, .50f))
		{
			return true;
		}
		else return false;
			
	}
}
