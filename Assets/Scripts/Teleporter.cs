using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public GameObject sister; // the location that this portal teleports to
	public GameObject teleportSound; // sound object for the teleport

	private bool ready; // true iff this portal is ready to teleport

	// Use this for initialization
	void Start () {
		ready = true;
	}

	// If p= true, activates teleportation capabilities
	// If p= false, deactivates teleportation capabilities
	public void setReady (bool p) {
		this.ready = p;
	}

	// If this portal is ready for teleportation, then when the player enters this portal..
	//	1) deactivates the teleportation of the sister portal
	//	2) teleports the player to the sister portal
	void OnTriggerEnter (Collider other) {
		// only teleports the player if ready for teleportation
		if (ready) {
			if (other.tag == "Player") {
				// deactivates the sister portal first so that we don't have an endless cycle of teleports
				Teleporter wormhole = sister.GetComponent<Teleporter> ();
				wormhole.setReady (false);
				// instantiates the teleportSound object
				Instantiate (teleportSound, other.transform.position, other.transform.rotation);
				// teleports the player to the sister location
				other.transform.position = sister.transform.position;
			}
		}
	}

	// Reactivates teleportation capabilites when the player leaves this portal
	void OnTriggerExit (Collider other) {
		this.setReady (true);
	}
}