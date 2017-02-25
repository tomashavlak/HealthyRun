using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSync : MonoBehaviour {

    public static bool endGame = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameSync.endGame == true)
        {
            ScoreCounter.saveScore();
        }
	}
}
