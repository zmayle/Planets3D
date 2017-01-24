using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	private Vector3 rotator;

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		rotator= new Vector3(0.0f, 1.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotator * rotationSpeed * Time.deltaTime);
	}
}
