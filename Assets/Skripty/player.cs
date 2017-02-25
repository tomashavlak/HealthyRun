using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

	private float pointer_x;
	ScoreCounter score;

	void Start ()
	{
		
	}
	
	// Update is called once per frame
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

	void OnCollisionEnter2D (Collision2D coll)
	{
		Debug.Log ("KOLIZE");
		if (coll.gameObject.tag == "RED") {
			Destroy (coll.gameObject,0.1f);
            coll.gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", (float)(0.2 + Mathf.Sin(Time.time) * 0.2));
			ScoreSync.collision++;
		}
	}
}
