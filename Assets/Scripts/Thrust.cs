using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour {

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			ps.Play();
		}
	}
}
