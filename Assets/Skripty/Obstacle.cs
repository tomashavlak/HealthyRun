using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Vector2 velocity = new Vector2(0, -2);

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
