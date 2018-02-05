using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {

	//ATOM BOMB
	public GameObject explosion;
	float bulletRadius = 25f,damagePoints = 80f;
	// Use this for initialization
	
	void OnTriggerEnter2D(Collider2D co)
	{
		//Instantiate explosion effect
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
		Destroy(gameObject);
		
	}
}
