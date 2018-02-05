using UnityEngine;
using System.Collections;

public class BulletProjectile : Photon.MonoBehaviour {
	
	public float angle , vel;
	public float gravity = 9.8f;
	private float leftVal;
	Rect orgScreenRect;
	AngleSpeedGUI angSpdObj;
	GameObject goGui;
	//public Rigidbody2D bullet;
	CameraFollowBullet camFollowScript;
	Transform bulletSpawn;
	
	public static string bulletName ;
	
	// Use this for initialization
	
	void Start()
	{
		camFollowScript = Camera.main.GetComponent<CameraFollowBullet>();
		goGui = GameObject.Find("GuiInput");
		leftVal = goGui.transform.position.x ;
		orgScreenRect = new Rect(leftVal + 300.0f, Screen.height - 40.0f, 120.0f, 30.0f);
		angSpdObj = goGui.GetComponent<AngleSpeedGUI>();
	}
	
	void OnGUI()
	{
		if(GUI.Button(orgScreenRect,"FIRE"))
		{
			vel = angSpdObj.speed * 0.4f;      //GuiManager.terrainSize ;
			angle = angSpdObj.angle;
			//Debug.Log(TurnManager.currplayer + "Fire !!" + photonView.isMine + "  " + NetworkManager.onlinePlayers[TurnManager.currplayer] + "  " + GuiManager.playerName);
			
			if(NetworkManager.onlinePlayers[TurnManager.currplayer].Equals(GuiManager.playerName))
			{
				photonView.RPC("bulletCreate",PhotonTargets.All,vel,angle,bulletName);
			}	
		}	
	}
	
	[RPC]
	void bulletCreate(float velParam,float angleParam, string bulletType)
	{
		Rigidbody2D bullet = Resources.Load<Rigidbody2D>(bulletType);
			
		bulletSpawn = TurnManager.cPlayer.Find("Barrel/BulletSpawnPt").transform;
		
		bulletSpawn.audio.Play();
				
		Rigidbody2D bulletInstance = Instantiate(bullet,
		   new Vector3(bulletSpawn.position.x,bulletSpawn.position.y,10f),Quaternion.identity) as Rigidbody2D;
		
		camFollowScript.player = bulletInstance.transform;
		
		bulletInstance.rigidbody2D.velocity = new Vector2(velParam*Mathf.Cos(angleParam*Mathf.Deg2Rad),
		                                                  (velParam*Mathf.Sin(angleParam*Mathf.Deg2Rad))-(gravity*Time.deltaTime));
		bulletInstance.rigidbody2D.angularVelocity = velParam*-1;
		
	}
}

