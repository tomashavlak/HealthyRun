using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratePartMap : MonoBehaviour {

    public GameObject tutorial;
    public List<GameObject> partA;
    public List<GameObject> partB;
    public List<GameObject> partC;
    public List<GameObject> partD;
    private int partsNum = 4;
    
    // po startu se vyheneruje první mapa
    void Start()
    {
        GenerateNewMap();
    }

    // při každé kolizi hlídajícího objektu se vygeneruje nová mapa ideální řešení oproti časovači
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "MAP")
        {
            GenerateNewMap();
        }
    }

    // náhodně se vygeneruje část mapy z předem připravených dílů. pokud běží metoda poprvé spustí se tutoriál
    void GenerateNewMap()
    {
        if (PlayerPrefs.GetString("Skip") == "TRUE")
        {
            System.Random rn = new System.Random();
            int parts = (int)Mathf.Round(ScoreSync.score / 10) + 1;
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
                    print("1");
                    break;
                case 2:
                    part = rn.Next(0, partB.Count);
                    Instantiate(partB[part]);
                    print("2");
                    break;
                case 3:
                    part = rn.Next(0, partC.Count);
                    Instantiate(partC[part]);
                    print("3");
                    break;
                case 4:
                    part = rn.Next(0, partD.Count);
                    Instantiate(partD[part]);
                    print("4");
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
