using UnityEngine;
using System.Collections;

public class ScoreSync : MonoBehaviour {
	public static int score;
	public static int collision;
    public static int failCollision;
    public static int scoreMultiplier = 1;
}

public class ScoreCounter : MonoBehaviour {
	void Start () {
		InvokeRepeating("addScore", 0f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject scoreObj = GameObject.Find("ScoreCounter");
		scoreObj.GetComponent<UnityEngine.UI.Text> ().text = ScoreSync.score.ToString ();

		if (ScoreSync.collision > 0) {
			ScoreSync.collision--;
			onCollision ();
		}
        if (ScoreSync.failCollision > 0)
        {
            ScoreSync.failCollision--;
            onFailCollision();
        }
    }

    public static void saveScore() {
        int max = PlayerPrefs.GetInt("maxScore");
        if (max < ScoreSync.score)
        {
            PlayerPrefs.SetInt("maxScore", ScoreSync.score);
        }
    }

	private void addScore() {
		ScoreSync.score = ScoreSync.score + ScoreSync.scoreMultiplier;
	}

	public void onCollision() {
		ScoreSync.score = ScoreSync.score + (ScoreSync.scoreMultiplier * 10);
	}
    public void onFailCollision()
    {
        Debug.Log("Score down");
        ScoreSync.score = ScoreSync.score - (ScoreSync.scoreMultiplier * 50);
        if (ScoreSync.score < 0)
        {
            Debug.Log("END END END");
            GameSync.endGame = true;
        }
    }

}
