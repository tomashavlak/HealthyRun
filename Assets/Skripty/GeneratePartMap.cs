using UnityEngine;
using System.Collections;

public class GeneratePartMap : MonoBehaviour {

    public GameObject partA;
    public GameObject partB;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
    }

    void CreateObstacle()
    {
        System.Random rn = new System.Random();
        int part = rn.Next(1, 10);
        Debug.Log(part);

        switch (part)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                Instantiate(partA);
            break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                Instantiate(partB);
                break;
            default:
                break;
        }
        
    }
}
