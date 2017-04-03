using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SocketIO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class sesIdObject
{
	public string sesId { get; set; }
}

public class ServerConnect : MonoBehaviour {

	private SocketIOComponent socket;
	public string sesId = null;

	public Image myImage;
	public Button FBtn;
	private string url;


	// stáhne skóre a pokud je facebook zobrazí ikonu
	void Start () {
        StartCoroutine(downloadScore());

		string FbImg = PlayerPrefs.GetString ("FbImg");
		if (FbImg.Length > 0) {
            Sprite sprite = TextureStore.ReadTextureFromPlayerPrefs ("FbImg");
			applyTexture (sprite);
		} else {
            string FbId = PlayerPrefs.GetString ("FbId");
			if (FbId.Length > 0) {
				this.url = "https://graph.facebook.com/" + FbId + "/picture?width=150&height=150";
				StartCoroutine (changeButton());
			}
		}
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		FBtn = GetComponent<Button>();
    }

    // obsluha socket.io klienta
	public void loadData() {
        socket.Connect ();
		socket.On("sesId", sesIdMethod);
		socket.On("data", dataUse);
    }

    //příjem socketu s ID otevře browser
	public void sesIdMethod(SocketIOEvent e) {
		JSONObject send = new JSONObject (JSONObject.Type.OBJECT);
		send.AddField ("room", e.data.GetField("sesId").str);
		socket.Emit ("joinRoom", send);
        Application.OpenURL ("http://healthy-run.tomashavlak.cz/fb?room=" + e.data.GetField("sesId").str);
	}

    //příjem dat ze serveru
	public void dataUse(SocketIOEvent e) {
		this.url = "https://graph.facebook.com/" + e.data.GetField("id").str + "/picture?width=150&height=150";
		string uID = e.data.GetField ("id").str;
		PlayerPrefs.SetString("FbId", uID);
		StartCoroutine (changeButton());
	}

	private IEnumerator changeButton() {
		WWW www = new WWW(url);
		yield return www;

		Sprite sprite = new Sprite ();

		sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f), 1f);
		TextureStore.WriteTextureToPlayerPrefs ("FbImg", sprite);
		applyTexture (sprite);
	}

    private IEnumerator downloadScore()
    {
        string fbId = PlayerPrefs.GetString("FbId");
        if (fbId.Length > 1)
        {
            string url = "http://healthy-run.tomashavlak.eu/getScore";
            WWWForm data = new WWWForm();
            data.AddField("id", fbId);
            WWW www = new WWW(url, data);
            yield return www;
            scoreObj score = JsonUtility.FromJson<scoreObj>(www.data.ToString());
            if (score.score > PlayerPrefs.GetInt("maxScore"))
            {
                PlayerPrefs.SetInt("maxScore", score.score);
                GameObject.Find("bestScore").GetComponent<UnityEngine.UI.Text>().text = "Best: " + score.score.ToString();
            }
        }
        
    }

    // odešle skóre na server pokud je připojen facebook profil
    public static IEnumerator saveScore()
    {
        string fbId = PlayerPrefs.GetString("FbId");
        if (fbId.Length > 1)
        {
            string url = "http://healthy-run.tomashavlak.eu/saveScore";
            WWWForm data = new WWWForm();
            data.AddField("id", fbId);
            data.AddField("score", PlayerPrefs.GetInt("maxScore"));
            WWW www = new WWW(url, data);
            yield return www;
        }

    }

    // stažení wiki a její parsování pomocí pluginu JSONObject
    public static IEnumerator getWiki(Action doLast = null)
    {
        string wikiJson = PlayerPrefs.GetString("wiki");
        int day = new DateTime().Day;

        if (wikiJson.Length < 1 || PlayerPrefs.GetInt("day") < day)
        {
            string fbId = PlayerPrefs.GetString("FbId");
            string url = "http://healthy-run.tomashavlak.eu/getWiki";
            WWWForm data = new WWWForm();
            data.AddField("id", fbId);
            WWW www = new WWW(url, data);
            yield return www;
            wikiJson = www.text;
            PlayerPrefs.SetString("wiki", wikiJson);
            PlayerPrefs.SetInt("day", day);
        }

        JSONObject obj = new JSONObject(wikiJson);
        foreach (JSONObject banner in obj.list)
        {
            WikiArr arr = new WikiArr();
            arr.id = banner.GetField("id").str;
            arr.name = Regex.Unescape(banner.GetField("name").str);
            arr.text = Regex.Unescape(banner.GetField("text").str);
            arr.font_size = int.Parse(banner.GetField("font_size").str);
            arr.img = Regex.Unescape(banner.GetField("img").str);
            Scroller.wiki.Add(arr);
        }
        if (doLast != null)
        {
            Scroller.preparingWikiArr = false;
            doLast();
        }
    }

    // stažení báze bannerů
    public static IEnumerator getKnowBase(Action doLast = null)
    {
        string knowJson = PlayerPrefs.GetString("knowJson");
        int day = new DateTime().Day;

        if (knowJson.Length < 1 || PlayerPrefs.GetInt("day") < day)
        {
            string fbId = PlayerPrefs.GetString("FbId");
            string url = "http://healthy-run.tomashavlak.eu/getYouKnowThat";
            WWWForm data = new WWWForm();
            data.AddField("id", fbId);
            WWW www = new WWW(url, data);
            yield return www;
            knowJson = www.text;
            PlayerPrefs.SetString("knowJson", knowJson);
            PlayerPrefs.SetInt("day", day);
        }

        JSONObject obj = new JSONObject(knowJson);
        foreach (JSONObject banner in obj.list)
        {
            BannerArr arr = new BannerArr();
            arr.id = banner.GetField("id").str;
            arr.name = Regex.Unescape(banner.GetField("name").str);
            arr.text = Regex.Unescape(banner.GetField("text").str);
            Banner.banners.Add(arr);
        }
        
        //Banner.banners = bannerArr;
        if (doLast != null)
        {
            Scroller.preparingWikiArr = false;
            doLast();
        }
    }

    // aplikace textury
    private void applyTexture(Sprite sprite) {
		GameObject btn = GameObject.Find ("FBtn");
		btn.GetComponent<SpriteRenderer> ().sprite = sprite;
		btn.GetComponent<Button> ().interactable = false;
	}

}

class scoreObj
{
    public int score;
}
