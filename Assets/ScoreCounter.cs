using UnityEngine;
using System.Collections;

public class ScoreSync : MonoBehaviour {
	public static int score;
	public static int collision;
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
	}

	private void addScore() {
		//ScoreSync.score = ScoreSync.score + ScoreSync.scoreMultiplier;

	}

	public void onCollision() {
		ScoreSync.score = ScoreSync.score + (ScoreSync.scoreMultiplier * 10);
	}

}
