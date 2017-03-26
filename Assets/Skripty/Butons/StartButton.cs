using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    public bool isSettings;
    public GameObject text;

    bool paused = true;
	void Start () {
		paused = true;
        if (isSettings)
        {
            if (GameSync.sound)
            {
                text.GetComponent<Text>().text = "Vypnout zvuk";
            }
            else
            {
                text.GetComponent<Text>().text = "Zapnout zvuk";
            }
        }
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
	public void PauseScene( string onPauseScene = null) {
		
		Debug.Log (paused);
        Debug.Log(Time.timeScale);
		paused = !paused;

		if (paused) {
			Time.timeScale = 1;
            if (onPauseScene != null)
            {
                SceneManager.UnloadScene(onPauseScene);
            }
			
		} else if (!paused) {
			Time.timeScale = 0;
            if (onPauseScene != null)
            {
                SceneManager.LoadScene(onPauseScene, LoadSceneMode.Additive);
            }
		}
	}

	public void UnpauseScene(string hrPause) {
		if (hrPause == "true") {
			Time.timeScale = 1;
			Debug.Log ("unpaused");
		}
	}

	public void ClearFbData() {
		PlayerPrefs.DeleteKey ("FbId");
		PlayerPrefs.DeleteKey ("FbImg");
	}
    public void ClearScore()
    {
        PlayerPrefs.DeleteKey("maxScore");
        StartCoroutine (ServerConnect.saveScore());
    }

    public void SoundBtn(GameObject text)
    {
        if (GameSync.sound)
        {
            PlayerPrefs.SetString("sound", "OFF");
            GameSync.sound = false;
            text.GetComponent<Text>().text = "Zapnout zvuk";

        } else
        {
            PlayerPrefs.SetString("sound", "ON");
            GameSync.sound = true;
            text.GetComponent<Text>().text = "Vypnout zvuk";
        }
    }
}
