using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	private float pointer_x;
	ScoreCounter score;

    // přidá na začátku hry layout a spustí edukační banner
	void Start ()
	{
        Time.timeScale = 0;
        SceneManager.LoadScene("GameLayout", LoadSceneMode.Additive);
        SceneManager.LoadScene("EduBanner", LoadSceneMode.Additive);
    }
	
    // nastavuje a překresluje hráčovu pozici na mapě
	void Update ()
	{
		if (Camera.main.ScreenToWorldPoint (Input.mousePosition).y < 4) {
			if (Input.touchCount > 0) {
				pointer_x = Camera.main.ScreenToWorldPoint (Input.touches [0].position).x;
			} else {
				pointer_x = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
			}
			transform.position = new Vector3 (pointer_x, float.Parse ("-4"), 0);
		}
	}

    // hlídá kolizi s objekty a vyhodnocuje akci
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "RED") {
			Destroy (coll.gameObject,0.1f);
            coll.gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", (float)(0.2 + Mathf.Sin(Time.time) * 0.2));
			ScoreSync.collision++;
		}
        if (coll.gameObject.tag == "WHITE")
        {
            GameSync.endGame = true;
        }
	}
}
