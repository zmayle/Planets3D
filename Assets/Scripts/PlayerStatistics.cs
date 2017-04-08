using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStatistics : MonoBehaviour {

	public static PlayerStatistics instance = null;
	public int[] bestScores;  //Entry i contains the best score for level i+1. A score of zero means the level hasn't yet been won.
	public int numberLevels = 0;  // the number of levels in the game

	void Awake () {
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a PlayerStatistics.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Initialize the bestScores array
		bestScores = new int[numberLevels];  //This sets all scores to zero
	}
}
