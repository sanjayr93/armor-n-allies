using UnityEngine;
using System.Collections;

public class MissileTrackNExplosion : MonoBehaviour {

	//public Transform player ;
	//CameraFollowBullet cam;
	public ParticleSystem explosion;
	float bulletRadius = 5f,
	bulletForce = 30f,
	damagePoints = 5f,
	smoothing = 1f;
	bool hasHit = false;
	
	// Use this for initialization
	void Start () {
		//cam = Camera.main.GetComponent<CameraFollowBullet>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(!col.name.Equals(NetworkManager.onlinePlayers[TurnManager.currplayer]) && !col.name.Equals ("BattleGround"))
		{
			//Debug.Log("Inside");
			rigidbody2D.velocity = Vector2.zero;
			transform.Rotate(Vector3.forward,Vector3.Angle(transform.position,col.gameObject.transform.position));	
			StartCoroutine(moveMissile(col.gameObject.transform));
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log("OnCollisionEnter2D" + col.collider.name);
		if((col.collider.name.Equals ("BattleGround") || col.collider.CompareTag("Player")) && !hasHit)
		{
			hasHit = !hasHit;
			OnExplode();
			Invoke("DestroyObjects",1f);
		}	
	}
	
	IEnumerator moveMissile (Transform target)
	{
		//Debug.Log(Vector3.Distance(jet.transform.position, jetEnd.position));
		while(Vector3.Distance(transform.position, target.position) > 0.1f)
		{
			//Debug.Log("Inside");
			transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime );
						
			yield return null;
		}
	}
	
	void DestroyObjects()
	{
		//cam.player = player;
		GameObject.FindObjectOfType<TurnManager>().requestToSwitch();
		Destroy(gameObject,1f);
	}
	
	public void OnExplode()
	{
		Instantiate(explosion,transform.position,Quaternion.identity);
		
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, bulletRadius,1 << LayerMask.NameToLayer("Character"));
		
		foreach(Collider2D c in col)
		{
			//Debug.Log("OnExplode "+c.tag+" and "+c.name);
			if(c.CompareTag ("Player"))
			{	
				c.gameObject.GetComponent<PlayerController>().hitTaken(damagePoints);
			}
		}		
	}
	
	
}
