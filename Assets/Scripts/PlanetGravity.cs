using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb= GetComponent<Rigidbody> ();
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			Vector3 direction = this.transform.position - other.transform.position;
			float r = direction.magnitude;

			// only add force if the distance between the objects is not zero
			if (r != 0.0f) {
				float force = rb.mass * other.attachedRigidbody.mass / r;
				other.attachedRigidbody.AddForce (force * direction);
			}
		}
	}
}
