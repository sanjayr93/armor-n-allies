using UnityEngine;
using System.Collections;

public class JetMarker : MonoBehaviour {

	public GameObject bomb;
	GameObject jet;
	//public Transform player;
	//CameraFollowBullet cam;
	public Transform jetEndpoint;
	public Transform jetSpawn;
	public float smoothing = 1f ;
	bool first = true, hasHit = false;
	
	// Use this for initialization

	void Start () {
		jet = GameObject.Find("Jet");
		//cam = Camera.main.GetComponent<CameraFollowBullet>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		//Instantiate(jet,jetSpawn.position,Quaternion.identity);
		if(!hasHit)
		{
			Debug.Log("Inside");
		//StartCoroutine(moveJet(jetEndpoint));
		StartCoroutine("moveJet",jetEndpoint);
		Invoke("DestroyObjects",3f);
		hasHit = !hasHit;
		}	
	}
	
	IEnumerator moveJet (Transform jetEnd)
	{
		//Debug.Log(Vector3.Distance(jet.transform.position, jetEnd.position));
		while(Vector3.Distance(jet.transform.position, jetEnd.position) > 0.05f)
		{
			//Debug.Log("Inside");
			jet.transform.position = Vector3.Lerp(jet.transform.position, jetEnd.position, smoothing * Time.deltaTime);
			
			if(jet.transform.position.x.AlmostEquals(transform.position.x, 0.3f) && first)
			{
				Instantiate(bomb,jet.transform.position,Quaternion.Euler(new Vector3(0,0,-90)));
				first = !first;
			}
			
			yield return null;
		}
	}
	
		void DestroyObjects()
		{
			//cam.player = player;
			GameObject.FindObjectOfType<TurnManager>().requestToSwitch();
			StopCoroutine("moveJet");
			jet.transform.position = jetSpawn.position;
			Destroy(gameObject,1f);
		
		}	
}
