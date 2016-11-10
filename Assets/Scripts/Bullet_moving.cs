using UnityEngine;
using System.Collections;

public class Balle : MonoBehaviour { 

    [HideInInspector]
	public float m_speed; 
	public float timerDestruct = 30;

	void Update () 
	{
		timerDestruct -= Time.deltaTime;
		if (timerDestruct <= 0)
		{
			Destroy (gameObject);
		}
		transform.Translate (transform.forward * m_speed * Time.deltaTime * 50,Space.World);	
	}

	void OnCollisionEnter(Collision other)
	{
		Destroy (gameObject);
	}
}
