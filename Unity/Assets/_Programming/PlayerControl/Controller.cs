using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	private Vector3 velocity;
	private bool superjump = false;
	//private bool jumping;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log(OnGround());
		velocity = rigidbody.velocity;
		Movement();
		rigidbody.velocity = velocity;
	}
	
	private void Movement()
	{
			
		if (Physics.Raycast(transform.position-Vector3.up*0.4f, Vector3.right, .50f) || 
		    Physics.Raycast(transform.position+Vector3.up*0.4f, Vector3.right, .50f) ) {
			velocity.x = 0.0f;
		}else{
			velocity.x = 5.0f;
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			if(OnGround())
			{
				if (superjump){
					velocity.y = 9.0f;
					superjump = false;
				}else
					velocity.y = 6.0f;
			}
		}
		
		//velocity *= 0.8f;
	}
	
    private void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "PickupJump"){
			superjump = true;
		}
        Destroy(other.gameObject);
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
