using UnityEngine;
using System.Collections;

public class TurnManager : Photon.MonoBehaviour {

	private static int currentPlayer = 1;
	public int previousPlayer;
	
	public static int currplayer
	{
		get
		{
			return currentPlayer; 
		}
	}
	
	public GameObject[] player1Objects;
	public GameObject[] player2Objects;	
	
	public static Transform cPlayer;
	//CameraFollowBullet camFollowScript;
	// Use this for initialization
	
	void Start () {
		//check if this reference is needed.....
		//camFollowScript = Camera.main.GetComponent<CameraFollowBullet>();
				
		if(PhotonNetwork.isMasterClient)
			photonView.RPC("SwitchPlayer",PhotonTargets.All);
				
	}
	
	void nextPlayer()
	{
		previousPlayer = currentPlayer;
		currentPlayer = previousPlayer + 1;
		if(currentPlayer > 1)
		{
			currentPlayer = 0;
		}
		
	}
	
	[RPC]
	public void SwitchPlayer()
	{
		CameraFollowBullet camFollowScript = Camera.main.GetComponent<CameraFollowBullet>();
		nextPlayer();
		switchCamera(camFollowScript);
		//Check if the below line is an overhead......
		camFollowScript.enabled = true;
		
	}
	
	void switchCamera(CameraFollowBullet camFollowScript)
	{
		// This was the problematic statement. A slight overhead here but I think we ll not call this like Update()
		cPlayer = GameObject.Find(NetworkManager.onlinePlayers[currentPlayer]).transform;
		camFollowScript.player = cPlayer;
	}
	
	public void requestToSwitch()
	{
		if(PhotonNetwork.isMasterClient)
			photonView.RPC("SwitchPlayer",PhotonTargets.All);
	}
/*	
	public void ReallySwitchPlayer()
	{
		if(currentPlayer == 1)
		{
			foreach(GameObject obj in player1Objects)
			{
				obj.SetActive(true);
			}
			player1Camera.SetActive(true);
			foreach(GameObject obj in player2Objects)
			{
				obj.SetActive(false);
			}
			player2Camera.SetActive(false);
		}
		if(currentPlayer == 2)
		{
			foreach(GameObject obj in player1Objects)
			{
				obj.SetActive(false);
			}
			player1Camera.SetActive(false);
			foreach(GameObject obj in player2Objects)
			{
				obj.SetActive(true);
			}
			player2Camera.SetActive(true);
		}
		if(currentPlayer == 0)
		{
			foreach(GameObject obj in player1Objects)
			{
				obj.SetActive(false);
			}
			foreach(GameObject obj in player2Objects)
			{
				obj.SetActive(false);
			}
		}
	}
*/	
}
