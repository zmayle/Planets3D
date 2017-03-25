using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private State gameState; // the game's current state (begins in the ready state)
	private string readyText; // the message shown when in the ready state
	private string victoryText; // the message shown when the player has won
	private string gameOverText; // message shown when the player has lost
	private string crashText; // message shown when the player crashes
	private string lostText; // message shown when the player leaves the screen
	private string singularityText; // message shown when the player gets sucked into a black hole
	private GameObject player; // the player game object, must be labeled with tag "Player"
	private Vector3 startPosition; // the player's starting position (set on the start of the scene)
	private bool playerFrozen; // whether or not the player game object is currently "frozen"
	private int fails; // the number of times the player has failed this level on this play through
	private AudioSource music;  // the background music audio clip for the level

	public Text gameText; // the Text object that the game should presently display on the screen
	public Text failText; // the Text object that displays the number of times the player has failed
	public GameObject playerPrefab; // the player object prefab to be instantiated when restarting the game
	public int numberLevels; // the total number of levels in the game (not including the menu)

	/** inv: During state ready, the player object is "frozen" (i.e. will not be affected by forces, will not move,
	*		and will not rotate). The ready text is also displayed, and the game waits for the player to press the
	*		spacebar to start the game. Transitions to playing state when the player presses the spacebar. 
	* 	inv: During state playing, the player object is NOT "frozen". No text is displayed by the GameController.
	* 		The game transitions to the gameOver state if the player loses (i.e. crashes, flies offscreen, or
	* 		gets sucked into a black hole) or to the victory state if the player reaches Earth.
	* 	inv: During state gameOver, the player object is non-existent (has been destroyed). The game over text is
	* 		displayed (including the text specific to the type of death). The game transitions to the ready state
	* 		when the player presses the spacebar.
	* 	inv: During state victory, the player object is "frozen". The victory text is displayed. The game loads the
	* 		next level when the player presses the spacebar.
	*/
	enum State {ready, playing, gameOver, victory};


	// Use this for initialization
	void Start () {
		//game begins in the ready state
		gameState = State.ready;
		// initialize the different messages for use later on
		readyText= "Prepare to begin your mission!\n[Press space to start]";
		victoryText = "Mission Complete!\n[Press the spacebar to proceed or 'M' to return to the menu]";
		gameOverText = "Mission Failed!\n[Press the spacebar to try again or 'M' to return to the menu]";
		crashText = "You crashed!\n";
		lostText = "You became lost in space!\n";
		singularityText = "Spacetime anomaly!\n";
		// get the player game object
		player = GameObject.FindWithTag ("Player");
		if (player != null)
		{
			startPosition = player.transform.position;
		}
		checkPlayer ();
		// put the correct text on the screen for the ready state and freeze the player
		gameText.text = readyText;
		freezePlayer ();
		fails = 0;
		// get the audio clip of the level's background music
		music = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	// During State ready, the GameController displays a message, waits for the player to press the spacebar,
	// and then begins the game and removes the message.
	// During State playing, the GameController doesn't have to do anything special.
	// During the State gameOver, the GameController displays a message, waits for the player to press the spacebar,
	// and then resets the game and switches the message.
	// During the State victory, the GameController displays a message, waits fo the player to press the spacebar, and 
	// then loads the next level.
	void Update () {
		if (gameState == State.ready) {
			if (Input.GetKeyDown ("space")) {
				beginGame ();
			}
		} else if (gameState == State.gameOver) {
			if (Input.GetKeyDown ("space")) {
				getReady ();
			} else if (Input.GetKeyDown ("m")) {
				menu ();
			}
		} else if (gameState == State.victory) {
			if (Input.GetKeyDown ("space")) {
				nextLevel();
			} else if (Input.GetKeyDown("m")) {
				menu();
			}
		}
	}

	/** If the player game object is null, throws a UnityException and prints
	 * "Cannot find 'Player' object" to the console.
	 */ 
	private void checkPlayer () {
		if (player == null)
		{
			throw new UnityException ("Cannot find 'Player' object");
		}
	}

	/** Freezes the player game object. 
	 * 	When the player is frozen, the player is kinematic and will not rotate or move.
	 */ 
	private void freezePlayer () {
		player.GetComponent<Rigidbody> ().isKinematic = true;
		playerFrozen = true;
	}

	/** Unfreezes the player game object.
	 */ 
	private void unfreezePlayer () {
		player.GetComponent<Rigidbody> ().isKinematic = false;
		playerFrozen = false;
	}

	// Starts the game by switching the game's state to ready and clearing the text on the screen.
	// Also unfreezes the player.
	// Precondition: can only call this method if the game is currently in the ready state
	private void beginGame () {
		gameState = State.playing;
		gameText.text = "";
		unfreezePlayer ();
	}

	// Puts the game in the ready state, resets the game, and switches the text on the screen to the
	// ready message.
	// Precondition: the game is currently in the gameOver state
	private void getReady () {
		gameState = State.ready;
		resetGame ();
		gameText.text = readyText;
	}

	// Moves on to the next level (if currently on the last level, it returns to the menu)
	private void nextLevel () {
		int sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		if (sceneIndex == numberLevels) menu ();
		else SceneManager.LoadScene (sceneIndex + 1);
	}

	// Loads the menu
	private void menu () {
		SceneManager.LoadScene (0);
	}

	// handles the game when the player reaches Earth (this is a victory)
	// Switches the gameState to victory and switches the gameText to the victory message.
	// Also freezes the player and stops the background music.
	public void victory () {
		gameState = State.victory;
		gameText.text = victoryText;
		freezePlayer ();
		music.Stop ();
	}

	// handles the game when the player crashes or flies off the screen
	// also updates how many times the player has failed
	// parameter loser denotes the way the player lost:
	//		"crash": the player crashed into a planet
	//		"lost": the player became lost in space
	//		"singularity": the player fell into a black hole
	// Precondition: string loser must be "crash", "lost", or "singularity"
	public void gameOver (string loser) {
		gameState = State.gameOver;
		fails += 1;
		failText.text = "Times Failed: " + fails;
		switch (loser) {
			case "crash":
				gameText.text = crashText + gameOverText;
				break;
			case "lost":
				gameText.text = lostText + gameOverText;
				break;
			case "singularity":
				gameText.text = singularityText + gameOverText;
				break;
			default:
				throw new UnityException ("The game did not end with a crash, getting lost, or a singularity");
		}
	}

	// Resets the current level to how it was at the beginning
	// Instantiates a player object at the start position, recognizes that object as the player, and freezes the player.
	private void resetGame () {
		Instantiate (playerPrefab, startPosition, Quaternion.identity);
		player = GameObject.FindWithTag ("Player");
		checkPlayer ();
		freezePlayer ();
	}
}
