using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	//Called as an Animation Event
	void DestroyAnimatorObject()
	{
		Destroy(gameObject);
	}
}
