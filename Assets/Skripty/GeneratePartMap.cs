using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratePartMap : MonoBehaviour {

    public GameObject tutorial;
    public List<GameObject> partA;
    public List<GameObject> partB;
    private int partsNum = 2;
    

    // Use this for initialization
    void Start()
    {
        generateNewMap();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "MAP")
        {
            generateNewMap();
        }
    }

    void generateNewMap()
    {
        if (PlayerPrefs.GetString("Skip") == "TRUE")
        {
            System.Random rn = new System.Random();
            int parts = (int)Mathf.Round(ScoreSync.score / 1000) + 1;
            if (parts > partsNum)
            {
                parts = partsNum;
            }
            int part = 0;
            switch (rn.Next(1, parts + 1))
            {
                case 1:
                    part = rn.Next(0, partA.Count);
                    Instantiate(partA[part]);
                    break;
                case 2:
                    part = rn.Next(0, partB.Count);
                    Instantiate(partB[part]);
                    break;
            }
        }
        else
        {
            PlayerPrefs.SetString("Skip", "TRUE");
            Instantiate(tutorial);
        }
    }
}
