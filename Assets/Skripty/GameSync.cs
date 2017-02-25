using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSync : MonoBehaviour {

    public static bool endGame = false;
    public static Vector2 gameSpeed = new Vector2(0 ,-2);
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
