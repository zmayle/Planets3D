using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	public GameObject playerExplosion;

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


	// When the player's ship makes contact with the planet's surface, the player's ship is destroyed,
	// the explosion effect animation is played with sound, and the player gets a game over.
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			Destroy (other.gameObject);
			if (gameController.gameState == GameController.State.playing) {
				gameController.gameOver ("crash"); // initiates a gameOver when the player crashes
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			}
		}
	}
}