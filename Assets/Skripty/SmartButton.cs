using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmartButton : MonoBehaviour {
    public bool isSettings;
    public GameObject text;

    bool paused = true;
	void Start () {
		paused = true;
        //pokud se jedná o nastavení ovládá btn
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

    // mění scénu
	public void ChangeScene( string hrSceneName) {
		SceneManager.LoadScene(hrSceneName, LoadSceneMode.Single);
	}

    // přidá scénu
	public void AddScene(string hrSceneName) {
		SceneManager.LoadScene(hrSceneName, LoadSceneMode.Additive);
	}

    // odebere scénu
	public void UnloadScene(string hrSceneName){
		SceneManager.UnloadScene (hrSceneName);
	}

    // pozastaví hru
	public void PauseScene( string onPauseScene = null) {
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

    // spustí hru
	public void UnpauseScene(string hrPause) {
		if (hrPause == "true") {
			Time.timeScale = 1;
		}
	}

    // vyčistí fb data
	public void ClearFbData() {
		PlayerPrefs.DeleteKey ("FbId");
		PlayerPrefs.DeleteKey ("FbImg");
	}

    // vynuluje skóre
    public void ClearScore()
    {
        PlayerPrefs.DeleteKey("maxScore");
        StartCoroutine (ServerConnect.saveScore());
    }

    // ovládání zvuku
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
