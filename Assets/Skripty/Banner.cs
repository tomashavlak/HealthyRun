using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BannerArr
{
    public string id { get; set; }
    public string name { get; set; }
    public string text { get; set; }
}

public class BannerObj
{
    public List<BannerArr> list { get; set; }
}

public class Banner : MonoBehaviour {
    public static List<BannerArr> banners = new List<BannerArr>();
    // Use this for initialization
    void Start () {
        StartCoroutine(ServerConnect.getKnowBase(initBanner));
    }

    public void initBanner()
    {
        print(banners);
        System.Random rn = new System.Random();
        int part = rn.Next(0, (banners.Count));

        TextMeshProUGUI name = GameObject.Find("header").GetComponent<TextMeshProUGUI>();
        name.text = banners[part].name;
        TextMeshProUGUI bText = GameObject.Find("text").GetComponent<TextMeshProUGUI>();
        bText.text = banners[part].text;
    }
}
