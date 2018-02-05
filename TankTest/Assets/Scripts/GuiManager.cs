using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {
	
	Rect windowRect = new Rect(120, 10, 400, 100);
	bool flag = false, cFlag = false;
	public static float terrainSize;
	public static string playerName = "Your Name";
	public int screenHeight,screenWidth;

	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();
	
	void Start()
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		playerName = PlayerPrefs.GetString("PlayerName","Your Name");
	    comboBoxList = new GUIContent[3];
	    comboBoxList[0] = new GUIContent("Small");
	    comboBoxList[1] = new GUIContent("Normal");
	    comboBoxList[2] = new GUIContent("Big");
	    	
	    listStyle.normal.textColor = Color.white; 
	    listStyle.onHover.background =
	    listStyle.hover.background = new Texture2D(2, 2);
	    listStyle.padding.left =
	    listStyle.padding.right =
	    listStyle.padding.top =
	    listStyle.padding.bottom = 4;
		//Debug.Log(Screen.currentResolution.width+"   "+Screen.currentResolution.height+"\n"+Screen.width+"   "+Screen.height);
	}
		
	void OnGUI()
	{
		GUI.Box(new Rect(screenWidth*0.25f,10,100f,90f), "Loader Menu");
		
		
		if(GUI.Button(new Rect(20,40,80,20),"Single Player"))
		{
			NetworkManager.offline = true;
			Application.LoadLevel("scene1");
		}
		
		if(GUI.Button(new Rect(20,70,80,20),"Multi Player"))
		{
			NetworkManager.offline = false;
			flag = true;
		}
		
		playerName = GUI.TextField(new Rect(20,100,100,25),playerName);

		if (flag) 
		{
			windowRect = GUI.Window(0, windowRect, mPlayerOptions, "My Window");
		}
		
		int selectedItemIndex = comboBoxControl.GetSelectedItemIndex();
		     selectedItemIndex = comboBoxControl.List( 
		     new Rect(20, 170, 100, 20), comboBoxList[selectedItemIndex].text, comboBoxList, listStyle );
		     GUI.Label( new Rect(20, 140, 400, 21), 
			"You picked " + comboBoxList[selectedItemIndex].text + "!" );
		
		switch(selectedItemIndex)
		{
			case 0:
				terrainSize = 0.4f;
			break;
			case 1:
				terrainSize = 0.4f;
			break;
			case 2:
				terrainSize = 0.4f;
			break;
		}		
		
	}

	void mPlayerOptions(int windowID)
	{
		//The left-value in rect is relative to window's left.
		if (GUI.Button (new Rect (10, 20, 100, 20), "Join Random Room")) 
		{
			NetworkManager.randRoom = true;
			Application.LoadLevel("scene1");
		}
		if (GUI.Button (new Rect (10, 50, 100, 20), "Create Room")) 
		{
			NetworkManager.cRoom = true;
			cFlag = true;
		}
		if (GUI.Button (new Rect (10, 80, 100, 20), "Join Room")) 
		{
			NetworkManager.jRoom = true;
			cFlag = true;
		}
		if(cFlag)
		{
			NetworkManager.roomName = GUI.TextField(new Rect(120,50,50,50),NetworkManager.roomName);
			if (GUI.Button (new Rect (180, 50, 50, 20), "Ok"))
				Application.LoadLevel("scene1");
		}
	}
	
	void OnDestroy()
	{
		PlayerPrefs.SetString("PlayerName",playerName);
	}
}