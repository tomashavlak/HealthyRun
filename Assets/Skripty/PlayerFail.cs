using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFail : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("KOLIZE FAIL");
        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "RED")
        {
            ScoreSync.failCollision++;
        }
    }
}
