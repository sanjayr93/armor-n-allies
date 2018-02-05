using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public bool isSpawnPointTaken = false;
	
	public void setSpawnPointTaken(bool flag)
	{
		isSpawnPointTaken = flag;
	}
	
	void OnDestroy()
	{
		//Called when the scene is destroyed
		isSpawnPointTaken = false;
	}
}
