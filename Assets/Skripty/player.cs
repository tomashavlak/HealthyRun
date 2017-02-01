using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    //private Rigidbody2D myRigBod = new Rigidbody2D();
    private float pointer_x;
    // Use this for initialization
    void Start () {
        //myRigBod = GetComponent<Rigidbody2D>();
         

    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touchCount > 0)
        {
            pointer_x = Camera.main.ScreenToWorldPoint(Input.touches[0].position).x;
        } else
        {
            pointer_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        transform.position = new Vector3(pointer_x, float.Parse("-4"), 0);
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("KOLIZE");
        if (coll.gameObject.tag == "RED")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        {

        }
    }

    void Die()
    {

        
    }
}
