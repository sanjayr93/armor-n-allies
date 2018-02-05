using UnityEngine;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour {

	//This code also includes Networking the player's movements....
	
	Vector3 realPosition = Vector3.zero;
	Quaternion barrelRotation = Quaternion.identity;
	Transform Barrel;
	
	// Use this for initialization
	void Start () {
			Barrel = GameObject.Find(gameObject.name+"/Barrel").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(photonView.isMine)
		{
			//Do nothing ! The joystick and anglespeedGUI will take care of movements
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position,realPosition, 0.1f);
			Barrel.rotation = Quaternion.Lerp(Barrel.rotation,barrelRotation,0.1f);
		}
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			//This is us.. We have to send our position over the network...
			//Debug.Log("writing.....");
			stream.SendNext(transform.position);
			stream.SendNext(Barrel.rotation);
		}
		else
		{
			//Other players' data ...
			realPosition = (Vector3) stream.ReceiveNext();
			barrelRotation = (Quaternion) stream.ReceiveNext();
		}
	
	}
	
	public void hitTaken(float damagePoints)
	{
		//Destroy(gameObject);
		Debug.Log("I'm Hitttt !!" + gameObject.name);
	} 
}
