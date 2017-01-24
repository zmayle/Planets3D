using UnityEngine;
using System.Collections;

public class BoundaryKeeper : MonoBehaviour {

	public GameObject lostSound;

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

	// Alerts the GameController object for this scene that the player has flown out of bounds
	// and destroys the player object (no explosion)
	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			Instantiate (lostSound, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
			gameController.gameOver ("lost");
		}
	}
}
