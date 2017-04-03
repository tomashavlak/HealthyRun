using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFail : MonoBehaviour {

    // hlídač propuštěných objektů s tagem red
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "RED")
        {
            ScoreSync.failCollision++;
        }
    }
}
