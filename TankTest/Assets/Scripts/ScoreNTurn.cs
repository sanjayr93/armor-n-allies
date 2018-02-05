using UnityEngine;
using System.Collections;

public class ScoreNTurn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(NetworkManager.onlinePlayers[TurnManager.currplayer].Equals(GuiManager.playerName))
			guiText.text = "Your Turn..";
		else
			guiText.text = NetworkManager.onlinePlayers[TurnManager.currplayer]+" Turn..";	
	}
}
