﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("bestScore").GetComponent<UnityEngine.UI.Text>().text = "Best: " + PlayerPrefs.GetInt("maxScore");
        ScoreSync.score = 0;

        SoundController.InitSettings();
        ServerConnect.getWiki();

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
