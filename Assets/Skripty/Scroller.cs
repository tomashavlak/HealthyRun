using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class WikiArr
{
    public string id { get; set; }
    public string name { get; set; }
    public int font_size { get; set; }
    public string img { get; set; }
    public string text { get; set; }
}

public class Scroller : MonoBehaviour {

    public RectTransform panel;
    public RectTransform center;
    public RectTransform tpl;

    public static bool dragging = false;
    public static bool preparingWikiArr = true;
    public static List<WikiArr> wiki = new List<WikiArr>();

    private List<RectTransform> panels = new List<RectTransform>();
    private List<float> distance = new List<float>();
    private int bttnDistance;
    private int minButtonNum;
    private float minDistance = 0;
    private bool loadingFailed = false;
    private bool centered = false;

    void Start()
    {
        dragging = false;
        preparingWikiArr = true;
        wiki.Clear();
        wiki = new List<WikiArr>();

        minButtonNum = 0;
        bttnDistance = 0;
        panels.Clear();
        panels = new List<RectTransform>();
        distance = new List<float>();
        minDistance = 0;
        loadingFailed = false;
        centered = false;

        Time.timeScale = 1;

        StartCoroutine(ServerConnect.getWiki(initWiki));
    }

    void initWiki()
    {
        try
        {
            for (int i = 0; i < wiki.Count; i++)
            {
                RectTransform panelClone = Instantiate(tpl);
                panelClone.GetComponent<Transform>().transform.SetParent(GameObject.Find("ScrollPanel").transform);
                panelClone.GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 0);
                panelClone.GetComponent<Transform>().transform.localPosition = new Vector2(i * 720, -230f);

                Transform panel = panelClone.FindChild("Panel");
                panel.FindChild("Name").GetComponent<Text>().text = wiki[i].name;
                panel.FindChild("Name").GetComponent<Text>().fontSize = wiki[i].font_size;

                string wikiImg = PlayerPrefs.GetString(wiki[i].id + "ImgWiki");
                string wikiImgId = wiki[i].id + "ImgWiki";
                if (wikiImg.Length > 0)
                {
                    Sprite sprite = TextureStore.ReadTextureFromPlayerPrefs(wikiImgId);
                    panel.FindChild("Img").GetComponent<Image>().sprite = sprite;
                }
                else
                {
                     StartCoroutine(changeImg(wiki[i].img, panel, wikiImgId));
                }

                panel.FindChild("Text").GetComponent<TextMeshProUGUI>().text = wiki[i].text;
                panels.Add(panelClone);

                distance.Add(0);

            }
            bttnDistance = (int)Mathf.Abs(panels[0].GetComponent<RectTransform>().anchoredPosition.x - panels[1].GetComponent<RectTransform>().anchoredPosition.x);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            loadingFailed = true;
            float slideToCenter = Mathf.Lerp(0f, 0, Time.deltaTime * 3f);
            Vector2 slide = new Vector2(slideToCenter, 0f);
            GameObject.Find("NoConnection").GetComponent<RectTransform>().anchoredPosition = slide;
        }
    }

    private IEnumerator changeImg(string url, Transform panelClone, string wikiImgId)
    {
        print(url);
        WWW www = new WWW(url);
        yield return www;

        Sprite sprite = new Sprite();

        sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f), 1f);
        TextureStore.WriteTextureToPlayerPrefs(wikiImgId, sprite);
        panelClone.FindChild("Img").GetComponent<Image>().sprite = sprite;
    }

    void Update()
    {
        if (! loadingFailed)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                distance[i] = (Mathf.Abs(center.transform.position.x - panels[i].transform.position.x));
                if (minDistance > distance[i] || i == 0)
                {
                    minDistance = distance[i];
                    minButtonNum = i;
                }
            }

            if (!dragging)
            {
                print("time to autoscroll");
                print(panels.Count);
                print((minButtonNum * -bttnDistance).ToString() + "=" + panel.anchoredPosition.x.ToString());
                CenterPanel(minButtonNum * -bttnDistance);
                for (int i = 0; i < panels.Count; i++)
                {
                    ScrollRect scroll = panels[i].GetComponent<ScrollRect>();
                    RectTransform panelSlider = panels[i].Find("Panel").GetComponent<RectTransform>();
                    if (i == minButtonNum)
                    {
                        scroll.enabled = enabled;
                        if (panelSlider.anchoredPosition.y < 0)
                        {
                            panelSlider.anchoredPosition = new Vector2(0, Mathf.Lerp(panelSlider.anchoredPosition.y, 0, Time.deltaTime * 5f));
                        }
                        if (panelSlider.anchoredPosition.y > 400)
                        {
                            panelSlider.anchoredPosition = new Vector2(0, Mathf.Lerp(panelSlider.anchoredPosition.y, 400, Time.deltaTime * 5f));
                        }
                    }
                    else
                    {
                        scroll.enabled = !enabled;
                        panelSlider.anchoredPosition = new Vector2(0, Mathf.Lerp(panelSlider.anchoredPosition.y, 0, Time.deltaTime * 5f));
                    }
                }
            }
        }
    }

    void CenterPanel(int newPos)
    {
        if (Mathf.Round(panel.anchoredPosition.x) != newPos)
        {
            float slideToCenter = Mathf.Lerp(panel.anchoredPosition.x, newPos, Time.deltaTime * 5f);
            Vector2 slide = new Vector2(slideToCenter, panel.anchoredPosition.y);
            panel.anchoredPosition = slide;
        }
    }

    public void StartStopDragging()
    {
        dragging = !dragging;
    }
}
