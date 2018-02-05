using UnityEngine;
using System.Collections;

public class DelSystem : MonoBehaviour {
	
	public string sortingLayerName;		// The name of the sorting layer the particles should be set to.
	
	
	void Start ()
	{
		// Set the sorting layer of the particle system.
		particleSystem.renderer.sortingLayerName = sortingLayerName;
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, particleSystem.duration);
	}
}
