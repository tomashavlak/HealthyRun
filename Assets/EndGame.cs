using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
	void Start () {
        if (GameSync.endGame)
        {
            GameObject.Find("End").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 280);
            GameObject.Find("Score").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -280);
            GameObject.Find("Score").GetComponent<Text>().text = "Skóre: " + PlayerPrefs.GetInt("maxScore").ToString();
            GameSync.endGame = false;
        }
	}
}
