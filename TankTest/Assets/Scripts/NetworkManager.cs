using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

	SpawnManager[] spawnPoints;
	GameObject gO;
		
	public static string[] onlinePlayers;
	
	public static bool offline = false,
					
					//CHANGE TO FALSE FINALLY..........For Testing, set to True
						randRoom = false,
					
						cRoom = false,
						jRoom = false;
	public static string roomName = "Default";
	public static int noOfPlayers = 2; //Set this from User Input and this must be the exact number of players.
	int index = 0;
	bool flag = false;
	int spawnTurn = 1; // for players other than masterclient...
	// Use this for initialization
	void Start () {
		onlinePlayers = new string[noOfPlayers];
		
		spawnPoints = GameObject.FindObjectsOfType<SpawnManager>();
		Connect ();
	}

	void Connect()
	{
		if(offline)
		{
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		}
		else
			PhotonNetwork.ConnectUsingSettings ("TankTest v1.0");
	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label(GuiManager.playerName);
	}

	void OnJoinedLobby()
	{
		//Debug.Log ("OnJoinedLobby");
		PhotonNetwork.player.name = PlayerPrefs.GetString("PlayerName");
		if (randRoom) 
		{
			PhotonNetwork.JoinRandomRoom ();
			randRoom = false;
		} 
		else if (cRoom) 
		{
			PhotonNetwork.CreateRoom (roomName, true, true, 2);
			cRoom = false;
		} 
		else if (jRoom) 
		{
			PhotonNetwork.JoinRoom (roomName);
			jRoom = false;
		}
		Debug.Log ("OnJoinedLobby");
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom ("Room"+Random.value);
	}

	void OnPhotonCreateRoomFailed()
	{
		Debug.Log ("OnPhotonCreateRoomFailed");
		PhotonNetwork.CreateRoom ("Room"+Random.value);
	}
	
	void OnPhotonJoinRoomFailed()
	{
		Debug.Log("OnPhotonJoinRoomFailed");
	}
	
	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
		GuiManager.playerName += PhotonNetwork.player.ID;

		if(!offline)
		{
			if(PhotonNetwork.isMasterClient)
			{	
				StartCoroutine(checkPlayers());
			}
			else
			{
				//check if its our turn using invokerepeating.... If yes cancelinvoke and playerspawn()
				StartCoroutine(isMyTurn());
			}		
		}	
	}
	
	IEnumerator checkPlayers()
	{
		while(PhotonNetwork.room.playerCount < 2)
		{
			yield return new WaitForSeconds(2f);
		}	
		
		Debug.Log("Calling playerSpawn");
		playerSpawn ();
		
	}
	
	IEnumerator isMyTurn()
	{
		while(spawnTurn != PhotonNetwork.player.ID)
		{
			yield return new WaitForSeconds(2f);
		}	
		
		Debug.Log("Calling playerSpawn");
		playerSpawn ();
		
	}
	
	void playerSpawn()
	{
		
		if(spawnPoints == null)
		{
			Debug.LogError("spawnPoints == null");
			return;
		}
		
		//foreach(PhotonPlayer myPlayer in PhotonNetwork.playerList)
		//{
			//foreach(SpawnManager spawnPoint in spawnPoints)
			//{
			//Debug.Log(myPlayer.name);
			tryAgain:
			int spawnIndex = Random.Range(0,spawnPoints.Length);
			SpawnManager spawnPoint = spawnPoints[spawnIndex]; 
				if(!spawnPoint.isSpawnPointTaken)
				{
					//Debug.Log("Spawning.....");
					gO = PhotonNetwork.Instantiate("Player",spawnPoint.transform.position,spawnPoint.transform.rotation,0);
					spawnPoint.setSpawnPointTaken(true);
					photonView.RPC( "assignName" , PhotonTargets.All,PhotonNetwork.player,spawnIndex);
					
				}
				else
				{
					goto tryAgain;
				}
			//}
		//}
		
		photonView.RPC("setNextTurn",PhotonTargets.All);
		
		//Enabling the script
		if(PhotonNetwork.isMasterClient)
		{
			Invoke("initializerMasterRPC",PhotonNetwork.room.playerCount * 1.5f);	
		}
	}
	
	[RPC]
	void setNextTurn()
	{
		spawnTurn++;
	}
	
	void initializerMasterRPC()
	{		
		photonView.RPC( "initializeScripts" , PhotonTargets.All);
	}
	
	[RPC]
	void assignName(PhotonPlayer player, int spawnIndex)
	{
		spawnPoints[spawnIndex].setSpawnPointTaken(true);
		//if(gO != null)
		//	gO.name = player.name + player.ID;
		//else
			GameObject.Find("Player(Clone)").name = player.name + player.ID;
			
		onlinePlayers[index++] = player.name + player.ID;	
	}
	
	[RPC]
	void initializeScripts()
	{
		//The scripts here are global scripts... They are available when the SCENE is created.. 
		//Check if scripts in Instantiated objects can be enabled here ...
		//I think we can bcuz we are calling this function everytime a player spawns...
		GameObject.FindObjectOfType<JoyStickController>().enabled = true;
		GameObject.FindObjectOfType<AngleSpeedGUI>().enabled = true;

		GetComponent<TurnManager>().enabled = true; //Since it's present in the same gameobject
		GetComponent<BulletProjectile>().enabled = true;
		GameObject.FindObjectOfType<ScoreNTurn>().enabled = true;
	}
	
	void OnDestroy()
	{
		//Called when the scene is destroyed

	}
	
}
