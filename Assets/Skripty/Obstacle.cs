using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Vector2 velocity = new Vector2(0, -2);

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        InvokeRepeating("updateSpeed", 0f, 1f);
    }

    private void updateSpeed() {
        GetComponent<Rigidbody2D>().velocity = GameSync.gameSpeed;
        GameSync.gameSpeed.y = GameSync.gameSpeed.y - 0.0003f;
    }
}
