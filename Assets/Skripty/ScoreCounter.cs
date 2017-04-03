using UnityEngine;
using System.Collections;

public class ScoreSync : MonoBehaviour {
	public static int score;
	public static int collision;
    public static int failCollision;
    public static int scoreMultiplier = 1;
}

public class ScoreCounter : MonoBehaviour {
    // nastaví časovač přidávání skóre
    void Start () {
		InvokeRepeating("addScore", 0f, 0.2f);
	}
	
	// aktualuzuje skóre
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

    //uloží skóre pokud je vyšší, než nejvyšší skóre
    public static void saveScore() {
        int max = PlayerPrefs.GetInt("maxScore");
        if (max < ScoreSync.score)
        {
            PlayerPrefs.SetInt("maxScore", ScoreSync.score);
        }
    }

    //přidá skóre za čas
	private void addScore() {
		ScoreSync.score = ScoreSync.score + ScoreSync.scoreMultiplier;
	}

    // přidá skóre za srážku
	public void onCollision() {
		ScoreSync.score = ScoreSync.score + (ScoreSync.scoreMultiplier * 10);
	}

    // ubere skóre popřípadě uklončí hru
    public void onFailCollision()
    {
        ScoreSync.score = ScoreSync.score - (ScoreSync.scoreMultiplier * 50);
        if (ScoreSync.score < 0)
        {
            GameSync.endGame = true;
        }
    }

}
