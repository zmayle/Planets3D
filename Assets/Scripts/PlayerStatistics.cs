using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public class PlayerStatistics : MonoBehaviour {

	public static PlayerStatistics instance = null;
	public int[] bestScores;  //Entry i contains the best score for level i+1. A score of zero means the level hasn't yet been won.
	public int numberLevels = 0;  // the number of levels in the game

	void Awake () {

		//Initialize the bestScores array
		bestScores = new int[numberLevels];  //This sets all scores to zero

		//Check if instance already exists
		if (instance == null) {

			// create the new instance
			instance = this;

			//check whether a save file exists
			if (File.Exists ("Saves/save.binary")) {
				// if so, then update the bestScores 
				LoadData();
			}
		}
		//If instance already exists and it's not this:
		else if (instance != this) {

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a PlayerStatistics.
			Destroy (gameObject);    
		}
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}


	// saves the current bestScores array in a save file (creates a new file if one does not exist)
	public void SaveData()
	{
		if (!Directory.Exists("Saves"))
			Directory.CreateDirectory("Saves");

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create("Saves/save.binary");

		formatter.Serialize(saveFile, bestScores);

		saveFile.Close();
	}


	// sets bestScores to the saved instance of bestScores
	private void LoadData()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

		bestScores = (int[])formatter.Deserialize(saveFile);

		saveFile.Close();
	}

}
