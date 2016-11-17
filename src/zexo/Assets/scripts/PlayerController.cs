using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin,xMax,zMin,zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		var movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		var rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = movement * speed;

		// prevent ship from leaving area of game
		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x,boundary.xMin,boundary.xMax),
			0.0f, 
			Mathf.Clamp(rigidbody.position.z,boundary.zMin,boundary.zMax)
		);

		// rotate the ship
		rigidbody.rotation = Quaternion.Euler(0.0f,0.0f,rigidbody.velocity.x * -tilt);

		// rotate ship based on position
		//var transform = GetComponent<Transform> ();
		//transform.rotation = new Quaternion(0.0f,0.0f,rigidbody.position.x * .15f,1f);
	}
}
