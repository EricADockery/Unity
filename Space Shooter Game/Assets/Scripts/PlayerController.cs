using UnityEngine;
using System.Collections;

[System.Serializable] //will add the object to the unity toolset: This is complex and will be covered later in tutorials maybe?
public class Boundry{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour { //: indicates inherits from MonoBehaviour
	public float speed; // by making it a public variable you can edit it inside unity. 
	public Boundry boundry; // reference the Boundry values
	public float tilt; //allows the bank/tilt of the ship

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Update(){ // This allows constant update not waiting on a Fixed Update time frame.
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate(){ //called every movement update
		//Input.GetAxis only returns 0 or 1
		float moveHorizontal = Input.GetAxis ("Horizontal");//grabs horizontal moves from player

		float moveVertical = Input.GetAxis ("Vertical"); //grabs vertical moves from the player

		Vector3 movement = new Vector3( moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed; //controls movement
		//keeps the ship inside the view field
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundry.xMin,boundry.xMax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundry.zMin, boundry.zMax)
		); 

		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt); //negative tilt will make the object go the correct direction.

	}
}
