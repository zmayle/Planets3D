﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	// begins the level whose scene index is specified by the parameter level
	public void beginLevel(int level) {
		SceneManager.LoadScene (level);
	}

}
