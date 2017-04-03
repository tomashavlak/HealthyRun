using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Vector2 velocity = new Vector2(0, -2);

    // nastaví objektu opakování na aktualizaci rychlosti pádu každých x ms
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        InvokeRepeating("updateSpeed", 0f, 1f);
    }

    //nastaví objektu rychlost pádu a aktualizuje rychlost pádu výchozího
    private void updateSpeed() {
        GetComponent<Rigidbody2D>().velocity = GameSync.gameSpeed;
        GameSync.gameSpeed.y = GameSync.gameSpeed.y - 0.001f;
    }
}
