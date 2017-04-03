using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// nastaví skóre a začne stahovat data ze serveru
	void Start () {
        GameObject.Find("bestScore").GetComponent<UnityEngine.UI.Text>().text = "Best: " + PlayerPrefs.GetInt("maxScore");
        ScoreSync.score = 0;

        SoundController.InitSettings();
        ServerConnect.getWiki();

        
	}
}
