using UnityEngine;
using System.Collections;

public class JoyStickController : MonoBehaviour {

	public GameObject player ;
	public Transform joyTrans = null;
	public float playerSpeed = 50f , //player move speed 
	maxJoyDelta = 0.05f; //max dist the joy can move expressed in % of the screen width
	private Vector3 oJoyPos, //original joystick pos
	joyDelta; // new joystick pos
	 
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find(GuiManager.playerName);
		joyTrans = this.transform; // REFERENCE to current joysticks transform
		oJoyPos = joyTrans.position;
	}

	void OnMouseUp()
	{
		joyTrans.position = oJoyPos;
	}
	
	void OnMouseDrag()
	{
		if(NetworkManager.onlinePlayers[TurnManager.currplayer].Equals(GuiManager.playerName))
			joyTrans.position = MoveJoystick();
	}
	
	Vector3 MoveJoystick()
	{
		float x = (Input.mousePosition.x - (guiTexture.pixelInset.width/2)) / Screen.width //,
		 /*y = (Input.mousePosition.y - (guiTexture.pixelInset.height/2)) / Screen.height*/ ;
		Vector3 position = new Vector3(Mathf.Clamp(x , oJoyPos.x - maxJoyDelta , oJoyPos.x + maxJoyDelta ),oJoyPos.y,0);
		
		player.rigidbody2D.velocity = new Vector2((position.x - oJoyPos.x) * playerSpeed , 0);
		//Debug.Log(player.rigidbody2D.velocity.magnitude);
		
		return position;	
	}
}
