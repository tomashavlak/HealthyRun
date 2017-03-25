// Author: OsmanSenol
// URL: http://stackoverflow.com/questions/38816412/how-to-save-a-sprite-to-a-playerpref/41069642#41069642

using UnityEngine;
using System.Collections;

public class TextureStore
{

	public static void WriteTextureToPlayerPrefs (string tag, Sprite sprite)
	{
		Texture2D tex = sprite.texture;
		PlayerPrefs.SetInt (tag + "-width", (int)tex.width);
		PlayerPrefs.SetInt (tag + "-height", (int)tex.height);

        byte[] texByte = tex.EncodeToPNG ();
		string base64Tex = System.Convert.ToBase64String (texByte);

		PlayerPrefs.SetString (tag, base64Tex);
		PlayerPrefs.Save ();
	}

	public static Sprite ReadTextureFromPlayerPrefs (string tag)
	{
		// load string from playerpref
		string base64Tex = PlayerPrefs.GetString (tag, null);
		int textureWidth = PlayerPrefs.GetInt (tag + "-width");
		int textureHeight = PlayerPrefs.GetInt (tag + "-height");

        if (!string.IsNullOrEmpty (base64Tex)) {
			// convert it to byte array
			byte[] texByte = System.Convert.FromBase64String (base64Tex);
			Texture2D tex = new Texture2D ((int)textureWidth, (int)textureHeight);

			//load texture from byte array
			if (tex.LoadImage (texByte)) {
				Sprite sprite = Sprite.Create(tex, new Rect(0, 0, (float)textureWidth, (float)textureHeight), new Vector2(0.5f, 0.5f), 1.0f);
                return sprite;
			}
		}
        return null;
	}
}