using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour {

	private ParticleSystem ps;
	private AudioSource clip;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
		clip = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			ps.Play();
			if (! clip.isPlaying) clip.Play ();
		}
	}
}
