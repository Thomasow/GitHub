using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	private Vector3 velocity;
	private bool jumping = false;		//Wether you just jumped or not
	private bool superjump = false;		//Superjump upgrade
	
	
	private bool goggles = false;		//Wether goggles are on/off
	private float energy = 100;			//Amount of power left in goggles
	public Texture2D texture;			//Energy bar texture
	
	// Use this for initialization
	void Start () {
		RenderSettings.fogDensity = 0.3f;
		rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		velocity = rigidbody.velocity;
		Movement();
		rigidbody.velocity = velocity;
		
		//Goggle battery charge
		if (goggles){
			RenderSettings.fogDensity = 0.1f;
			if (energy>=0)
				energy-=Time.deltaTime*5.0f;
			else
				goggles = false;		
		}else{
			RenderSettings.fogDensity = 0.3f;	
		}
	}
	
	private void Movement()
	{
		//Limit horizontal movement
		if (Physics.Raycast(transform.position-Vector3.up*0.4f, Vector3.right, .50f) || 
		    Physics.Raycast(transform.position+Vector3.up*0.4f, Vector3.right, .50f) ) {
			velocity.x = 0.0f;
		}else{
			velocity.x = 5.0f;
		}
		
		//Handle (variable) jumping
		if(Input.GetMouseButtonDown(0) && (Input.mousePosition.y>150) )
		{
			if(OnGround())
			{
				if (superjump){ //Superjump powerup
					velocity.y = 6.0f;
					superjump = false;
				}else
					velocity.y = 4.0f;
				jumping = true;
				
			}
		}
		
		//Handle gravity
		if (jumping == true && Input.GetMouseButton(0)){
			if (velocity.y < 0)
				jumping = false;
			velocity.y -= 4.9f * Time.deltaTime; //gravity
		}else
			velocity.y -= 9.8f * Time.deltaTime; //gravity
	}
	
	//Handle triggers
    private void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "PickupJump"){
			superjump = true;
        	Destroy(other.gameObject);
		}
    }
	
	//Wether player is onground 
	private bool OnGround()
	{
		if (Physics.Raycast(transform.position, -Vector3.up, 1.0f))
		{
			return true;
		}
		else return false;
			
	}
	
	//Handle GUI
	private void OnGUI()
	{
		//Toggle goggles button
		if (!goggles){
			if (GUI.Button(new Rect(0,Screen.height-150,150,150),"Turn on Goggles")){
				if (energy>0)
					goggles=!goggles;
			}
		}else{
			if (GUI.Button(new Rect(0,Screen.height-150,150,150),"Turn off Goggles"))
				goggles=!goggles;
		}
		
		//Other button (does nothing)
		GUI.Button (new Rect(Screen.width-150,Screen.height-150,150,150),"other button");
		
		//Draw energy bar
		GUI.DrawTexture(new Rect(150,Screen.height-150+50,(Screen.width-300)*energy/100.0f,50),texture);
	}
}
