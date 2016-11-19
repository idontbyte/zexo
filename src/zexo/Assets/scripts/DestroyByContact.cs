using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	void OnTriggerEnter(Collider other) {
		// ignore collision with boundary
		if (other.tag == "Boundary")
			return;

		// explosion
		Instantiate(explosion, transform.position, transform.rotation);

		if (other.tag == "Player")
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

		// destroy both objects
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
