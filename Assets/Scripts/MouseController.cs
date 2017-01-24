using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	private Rigidbody rb;
	private Camera camera;
	private float mouseDistance; // the distance between the mouse and the object in screen space

	public float speed;			// scaling factor for the ship's speed
	public float maxThrust;		// the maximum amount of thrust the ship can put out


	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		camera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		// set mouseDistance
		Vector3 mousePos= new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
		Vector3 playerPos= camera.WorldToScreenPoint(transform.position);
		Vector3 diff= mousePos - playerPos;
		mouseDistance= diff.magnitude;
	}

	// Every frame, the ship rotates to face the mouse, and thrust is added to the ship in the forward direction -- UNLESS
	// the ship is currently kinematic. If the ship is kinematic, nothing happens.
	void FixedUpdate() {
		if (!rb.isKinematic) {
			rotate ();
			thrust ();
		}
	}

	// sets the ship's rotation so that it points at the mouse position
	// also keeps track of the distance between the mouse and the object
	private void rotate() {
		// get mouse's position
		Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
		// get the mouse's angle relative to this object
		Vector3 playerPos = camera.WorldToScreenPoint (transform.position);
		Vector3 diff = mousePos - playerPos;
		mouseDistance = diff.magnitude;
		float angle = Vector3.Angle (new Vector3 (0, 1, 0), diff);
		if (diff.x < 0)
			angle = -angle;
		// set this object's y rotation to that angle
		transform.rotation = Quaternion.Euler (0, angle, 0);
	}

	// adds thrust to the ship in the direction the ship is facing when the player presses the left mouse key.
	// thrust is proportional to the object's "speed" and to the distance between the mouse and the object.
	// thrust cannot be greater than the max allowed thrust
	private void thrust() {
		if (Input.GetButton ("Fire1")) { 
			float thrustPower = 0.01f * mouseDistance * speed;
			thrustPower = Mathf.Min (thrustPower, maxThrust);
			//Debug.Log ("ThrustPower: " + thrustPower);
			Vector3 thrust = thrustPower * transform.forward;
			rb.AddForce (thrust);
		}
	}
}
