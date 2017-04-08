using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	private PlayerStatistics ps;

	public int level;

	// Use this for initialization
	void Start () {
		ps = GameObject.Find ("PlayerStatistics").GetComponent<PlayerStatistics> ();
		if (ps != null) {
			if (ps.bestScores [level - 1] != 0) {
				Text bestText = this.GetComponent<Text> ();
				bestText.text = "Best: " + ps.bestScores [level - 1];
			}
		}
	}
}