using UnityEngine;
using System.Collections;

public class bulletParticle : MonoBehaviour {

	public ParticleSystem explosion;
	float bulletRadius = 5f,
	bulletForce = 30f,
	damagePoints = 5f;
	bool hasHit = false;	
	
	
	// Use this for initialization
	void Start () {
		//cam = Camera.main.GetComponent<CameraFollowBullet>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log("Trigger "+col.tag+" and "+col.name);
		//Since it is a Trigger it always collide with the background .....
		if(col.name == "BattleGround" && !hasHit) //for use in cluster n three shots
		{
			hasHit = !hasHit;
			OnExplode();
			//Shift this to Turnmanager
			
			//Disabling for use in cluster n three shots enable after turnmanager
			//cam.player = player;
			
			Destroy(gameObject,1f);
		}	
		
	}
	
	public void OnExplode()
	{
		Instantiate(explosion,transform.position,Quaternion.identity);
		
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, bulletRadius,1 << LayerMask.NameToLayer("Character"));
		
		foreach(Collider2D c in col)
		{
			Debug.Log("OnExplode "+c.tag+" and "+c.name);
			if(c.CompareTag ("Player"))
			{	
				c.gameObject.GetComponent<PlayerController>().hitTaken(damagePoints);
			}
		}		
	}
}
