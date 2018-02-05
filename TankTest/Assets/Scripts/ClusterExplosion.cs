using UnityEngine;
using System.Collections;

public class ClusterExplosion : MonoBehaviour {

	private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnParticleCollision(GameObject other)
	{
		Debug.Log(other.name +" and "+other.tag);
		int safeLength = particleSystem.safeCollisionEventSize;
		if (collisionEvents.Length < safeLength)
			collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
		
		int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
		int i = 0;
		while (i < numCollisionEvents) {
			if (other.rigidbody2D) {
				Debug.Log(other.name +" and "+other.tag);
				/*
				Vector3 pos = collisionEvents[i].intersection;
				Vector3 force = collisionEvents[i].velocity * 10;
				other.rigidbody.AddForce(force);
				*/
			}
			i++;
		}
		
	}		
}
