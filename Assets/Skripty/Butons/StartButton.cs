using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	public void ChangeScene( string hrSceneName) {
		SceneManager.LoadScene(hrSceneName, LoadSceneMode.Single);
		//Application.LoadLevel(hrSceneName);
		Debug.Log (hrSceneName);
	}
}
