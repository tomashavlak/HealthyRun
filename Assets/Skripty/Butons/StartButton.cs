using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	bool paused = true;
	void Start () {
		paused = true;
	}
	public void ChangeScene( string hrSceneName) {
		SceneManager.LoadScene(hrSceneName, LoadSceneMode.Single);
		Debug.Log (hrSceneName);
	}

	public void AddScene(string hrSceneName) {
		SceneManager.LoadScene(hrSceneName, LoadSceneMode.Additive);
		Debug.Log (hrSceneName);
	}
	public void UnloadScene(string hrSceneName){
		SceneManager.UnloadScene (hrSceneName);
	}
	public void PauseScene() {
		Debug.Log (paused);
		paused = !paused;

		if (paused) {
			Time.timeScale = 1;
		} else if (!paused) {
			Time.timeScale = 0;
		}
	}

	public void UnpauseScene(string hrPause) {
		if (hrPause == "true") {
			Time.timeScale = 1;
			Debug.Log ("unpaused");
		}
	}
}
