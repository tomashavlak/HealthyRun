using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSync : MonoBehaviour {

    public static bool endGame = false;
    public static bool sound = false;
    public static Vector2 gameSpeed = new Vector2(0 ,-2);
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameSync.endGame == true)
        {
            Debug.Log("KONEC V SYNC");
            ScoreCounter.saveScore();
            StartCoroutine(ServerConnect.saveScore());

            SceneManager.LoadScene("MenuPaused", LoadSceneMode.Additive);
            SceneManager.UnloadScene("GameLayout");
            Time.timeScale = 0;
            ScoreSync.score = 0;
        }
	}
}
