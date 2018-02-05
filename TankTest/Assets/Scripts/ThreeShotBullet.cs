using UnityEngine;
using System.Collections;

public class ThreeShotBullet : MonoBehaviour {
	
	//CameraFollowBullet cam;
	//public Transform player;
	public Rigidbody2D bullet;
	
	delegate void ClickAction();
	event ClickAction OnClicked;
	
	bool hasHit = false;
	//public ParticleSystem explosion;
	// Use this for initialization
	void Start () {
	//	cam = Camera.main.GetComponent<CameraFollowBullet>();
		OnClicked+=Explode;
		OnClicked+=Explode;
		OnClicked+=Explode;
		
		StartCoroutine(Timer());
		//Invoke("OnClicked",1f);
	}
	
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(1f);
		
		OnClicked();
		
	}
	void Explode()
	{
		//Instantiate(explosion,transform.position,Quaternion.identity);
		//Destroy(gameObject);
		//cam.player = player;
		Rigidbody2D bulletInstance = Instantiate(bullet,transform.position,Quaternion.identity) as Rigidbody2D;
		
		bulletInstance.rigidbody2D.velocity = new Vector2(Random.Range (rigidbody2D.velocity.x-4f,rigidbody2D.velocity.x+4f),Random.Range (rigidbody2D.velocity.y-2f,rigidbody2D.velocity.y+2f));
		bulletInstance.rigidbody2D.angularVelocity = rigidbody2D.angularVelocity;
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{	
		if(!hasHit)
		{
		hasHit = true;
		GameObject.FindObjectOfType<TurnManager>().requestToSwitch();
		
		Destroy(gameObject,2f);
		}
	//	cam.player = player;
	}
	
	void Update () 
	{
	}
}
