using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiki : MonoBehaviour {
    public RectTransform tpl;
    public List<RectTransform> pannels;


    public void Start()
    {
        return;
        Debug.Log(tpl);
        for (int i = 0; i < 3; i++)
        {
            pannels.Add(tpl);
            RectTransform panel = Instantiate(pannels[i]);
            panel.transform.parent = GameObject.Find("ScrollPanel").transform;
            panel.transform.localScale = new Vector3(1f, 1f, 0);
            panel.transform.localPosition = new Vector2(i * 720, -1075);

        }
    }
    
}
