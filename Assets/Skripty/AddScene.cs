using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour {

	public string scene;
    //přidá scénu, která je definována v unity editoru
	void Start () {
		SceneManager.LoadScene(scene, LoadSceneMode.Additive);
	}
}
