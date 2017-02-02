using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class AddScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
		SceneManager.LoadScene("MenuGame01", LoadSceneMode.Additive);
	}
}
