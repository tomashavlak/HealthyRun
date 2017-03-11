using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scroller : MonoBehaviour {

    public RectTransform panel;
    public RectTransform center;
    public RectTransform tpl;

    public static bool dragging = false;

    private List<RectTransform> panels = new List<RectTransform>();
    private List<float> distance = new List<float>();
    private int bttnDistance;
    private int minButtonNum;
    private float minDistance = 0;

    void Start()
    {
        //Debug.Log(center.transform.position.x);
        for (int i = 0; i < 3; i++)
        {
            RectTransform panelClone = Instantiate(tpl);
            panelClone.GetComponent<Transform>().transform.parent = GameObject.Find("ScrollPanel").transform;
            panelClone.GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 0);
            panelClone.GetComponent<Transform>().transform.localPosition = new Vector2(i * 720, 0);
            panels.Add(panelClone);
            
            distance.Add(0);

        }

        
        
        bttnDistance = (int)Mathf.Abs(panels[0].GetComponent<RectTransform>().anchoredPosition.x - panels[1].GetComponent<RectTransform>().anchoredPosition.x);


    }

    void Update()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            //Debug.Log(panels[i].GetComponent<Transform>().transform.position.x);
            distance[i] =(Mathf.Abs(center.transform.position.x - panels[i].transform.position.x));
            if (minDistance > distance[i] || i == 0)
            {
                minDistance = distance[i];
                minButtonNum = i;
            }
        }

        if (!dragging)
        {
            CenterPanel(minButtonNum * -bttnDistance);
        }
    }

    void CenterPanel(int newPos)
    {
        float slideToCenter = Mathf.Lerp(panel.anchoredPosition.x, newPos, Time.deltaTime * 15f);
        Vector2 slide = new Vector2(slideToCenter, panel.anchoredPosition.y);
        panel.anchoredPosition = slide;
    }

    public void StartStopDragging()
    {
        dragging = !dragging;
    }
}
