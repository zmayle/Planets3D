using UnityEngine;
using System.Collections;

public class Singularity : MonoBehaviour {

	public GameObject blackHoleSound;

	private GameController gameController;


	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}


	// When the player's ship is inside the black hole, the player's ship is destroyed and the player gets a game over.
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			Instantiate (blackHoleSound, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
			gameController.gameOver ("singularity"); // initiates a gameOver when the player crashes
		}
	}
}
