using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


	public Texture backgroundTexture;	
	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0f,0f,Screen.width,Screen.height),backgroundTexture);
	}	
	// Update is called once per frame
	void Update () {
	
	}
}
