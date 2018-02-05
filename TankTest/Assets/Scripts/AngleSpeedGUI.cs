using UnityEngine;
using System.Collections;

public class AngleSpeedGUI : MonoBehaviour {
	
	public float angle = 45.0f, speed = 50.0f;
	private float sliderLeftpos, sliderToppos;
	#pragma warning disable 0414
	bool flag = false;
	Rect orgScreenRect;
	public Transform player = null;
	Quaternion orgRotation;
	
	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();
	
	
	void Start()
	{
		//Initializations...
		player = GameObject.Find(GuiManager.playerName+"/Barrel").transform;
		orgRotation = player.rotation;
		sliderLeftpos = transform.position.x + 100.0f ;
		sliderToppos = Screen.height - 40.0f;
		orgScreenRect = new Rect(sliderLeftpos, sliderToppos, 100.0f, 30.0f);
		
		if(player != null)
			player.eulerAngles = new Vector3(0,0,angle) ;	
		
		//Write an algorithm to dynamically populate this combo box.....		
		comboBoxList = new GUIContent[5];
		comboBoxList[0] = new GUIContent("SingleShotBullet");
		comboBoxList[1] = new GUIContent("ThreeShots");
		comboBoxList[2] = new GUIContent("ClusterBombBullet");
		comboBoxList[3] = new GUIContent("Missile");
		comboBoxList[4] = new GUIContent("JetMarker");
		
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
	}
	
	void OnGUI()
	{
		flag = LabelSlider (orgScreenRect);
		
		//For combo box
		
		int selectedItemIndex = comboBoxControl.GetSelectedItemIndex();
		selectedItemIndex = comboBoxControl.List( 
		new Rect(Screen.width - 150, Screen.height - 50, 100, 20), comboBoxList[selectedItemIndex].text, comboBoxList, listStyle );
		//GUI.Label( new Rect(20, 140, 400, 21), 
		  //        "You picked " + comboBoxList[selectedItemIndex].text + "!" );
		
		BulletProjectile.bulletName = comboBoxList[selectedItemIndex].text; 
		
	}
	
	bool LabelSlider (Rect screenRect) {
		//For Angle:
		GUI.Label (screenRect, "Angle");
		
		// Push the Slider to the end of the Label
		screenRect.x += (screenRect.width-50.0f); 
		screenRect.y += 5.0f;
		
		angle = GUI.HorizontalSlider (screenRect, angle, 0.0f, 180.0f);
		
		//Change Barrel angle
		if(player != null)
			player.rotation = Quaternion.Lerp(player.rotation,Quaternion.Euler(new Vector3(0,0,angle)),0.1f);
		//	player.eulerAngles = new Vector3(0,0,angle) ;
		
		
		//For Speed:
		screenRect = orgScreenRect;
		screenRect.y += 20.0f;
		GUI.Label (screenRect, "Speed");
		
		// Push the Slider to the end of the Label
		screenRect.x += (screenRect.width-50.0f); 
		screenRect.y += 5.0f;
		
		speed = GUI.HorizontalSlider (screenRect, speed, 0.0f, 100.0f);
		
		return true;
	}
	
}
