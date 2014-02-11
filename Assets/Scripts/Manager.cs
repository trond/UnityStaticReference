using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public static Manager Instance
	{
		get;
		private set;
	}

	bool clearInstance = false;

	[SerializeField]
	GameObject[] prefabs;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	void OnDestroy()
	{
		if (clearInstance && Instance == this)
		{
			Instance = null;
		}
	}

	void OnGUI()
	{
		clearInstance = GUI.Toggle(new Rect(20, 20, 100, 20), clearInstance, "Clear instance");
	}	
}
