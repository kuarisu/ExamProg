using UnityEngine;
using System.Collections;

public class Deplacement01 : MonoBehaviour {

	public Rigidbody myRigidbody;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		myRigidbody.velocity = new Vector3 (10.0f,myRigidbody.velocity.y,0.0f);
	}

}
