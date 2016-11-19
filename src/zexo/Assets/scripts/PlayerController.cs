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

	public GameObject shot;
	public Transform shotSpawn;

	public float fireDelta = 0.5F;

	private float nextFire = 0.5F;
	private GameObject newProjectile;
	private float myTime = 0.0F;

	void Update() {
		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Jump") && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			newProjectile = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		var movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		var rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = movement * (speed + Mathf.Abs(rigidbody.velocity.x) * 0.5f);

		// prevent ship from leaving area of game
		rigidbody.position = new Vector3 (
			Mathf.Clamp(rigidbody.position.x,boundary.xMin,boundary.xMax),
			0.0f, 
			Mathf.Clamp(rigidbody.position.z,boundary.zMin,boundary.zMax)
		);

		// rotate the ship
		rigidbody.rotation = Quaternion.Euler(0.0f,0.0f,rigidbody.velocity.x * - tilt);
	}
}
