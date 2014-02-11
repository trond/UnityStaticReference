using UnityEngine;
using System.Collections;

public class SceneLogic : MonoBehaviour {

	Sprite[] sprites = null;

	void Update()
	{
		sprites = Resources.FindObjectsOfTypeAll(typeof(Sprite)) as Sprite[];
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, Screen.height - 40, 100, 20), "Change scene"))
		{
			Application.LoadLevel((Application.loadedLevel + 1) % Application.levelCount);
		}

		GUI.Label(new Rect(20, Screen.height - 60, 200, 20), "Manager instanced: " + (Manager.Instance != null));

		if (sprites != null)
		{
			GUI.Label(new Rect(Screen.width - 150, 30, 200, 20), "Sprites in memory: " + sprites.Length);

			for (int i = 0; i < sprites.Length; ++i)
			{
				GUI.DrawTexture(new Rect(Screen.width - 150, 64 * (i + 1) + 30, 64, 64), sprites[i].texture);
				GUI.Label(new Rect(Screen.width - 150, 64 * (i + 1) + 30, 200, 20), sprites[i].name);
			}
		}
	}
}
