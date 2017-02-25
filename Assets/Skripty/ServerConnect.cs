using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SocketIO;
using UnityEngine;

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


	// Use this for initialization
	void Start () {
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

	public void loadData() {
        socket.Connect ();
		socket.On("sesId", sesIdMethod);
		socket.On("data", dataUse);
    }

	public void sesIdMethod(SocketIOEvent e) {
		JSONObject send = new JSONObject (JSONObject.Type.OBJECT);
		send.AddField ("room", e.data.GetField("sesId").str);

		socket.Emit ("joinRoom", send);

		Application.OpenURL ("http://healthy-run.tomashavlak.cz/?room=" + e.data.GetField("sesId").str);
	}

	public void dataUse(SocketIOEvent e) {
		this.url = "https://graph.facebook.com/" + e.data.GetField("id").str + "/picture?width=150&height=150";

		string uID = e.data.GetField ("id").str;
		PlayerPrefs.SetString("FbId", uID);


		//this.changeButton (url);
		StartCoroutine (changeButton());
	}

	private IEnumerator changeButton() {
		WWW www = new WWW(url);
		yield return www;

		Sprite sprite = new Sprite ();

		sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f), 1.0f);
		TextureStore.WriteTextureToPlayerPrefs ("FbImg", sprite);
		applyTexture (sprite);
		}

	private void applyTexture(Sprite sprite) {
		GameObject btn = GameObject.Find ("FBtn");
		btn.GetComponent<SpriteRenderer> ().sprite = sprite;
		btn.GetComponent<Button> ().interactable = false;
	}

}
