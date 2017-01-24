using UnityEngine;
using System.Collections;

public class WinOnContact : MonoBehaviour {

	public GameObject victorySound1;
	public GameObject victorySound2;

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
	
	// initiates a game victory when the player reaches this planet.
	// Also randomly chooses one of the victory sound effects and plays it
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			float val = Random.value;
			if (val <= 0.5f) Instantiate (victorySound1, other.transform.position, other.transform.rotation);
			else Instantiate (victorySound2, other.transform.position, other.transform.rotation);

			gameController.victory ();
		}
	}
}
