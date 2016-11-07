using UnityEngine;
using System.Collections;

public class Balle : MonoBehaviour { 

	//Cette variable sert a modifier la vitesse de deplacement de la balle
	public float vitesse; 

	//cette variable permet de definir le temps de vie de la balle
	public float timerDestruct = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//On fait progressivment diminuer le temps de vie de la balle
		timerDestruct -= Time.deltaTime;
		//puis on la detruit lorsqu'il ateint zero
		if (timerDestruct <= 0)
		{
			Destroy (gameObject);
		}

		//ici on fait effectuer une translation a la balle
		//la vitesse est modifiée par le variable vitesse
		//on multiplie par Time.deltaTime * 50 pour rendre ce deplacement independant du framerate
		//On ajoute Space.World pour eviter que la rotation de la balle n'afecte sa trajectoire
		transform.Translate (transform.forward * vitesse * Time.deltaTime * 50,Space.World);	
	}

	//Cette fonction est appelée automatiquement par unity lors d'une collision avec un autre collider
	//notez que la balle et la tourelle doivent etre placé sur 2 layer de collision qui ne collident pas entre eux
	//voir : edit projectSetting Physics et edit ProjectSetting tag & layers
	void OnCollisionEnter(Collision other)
	{
		//Si le collider touché ets celui de l'avatar, on appel la fonction Death dessu
		if (other.gameObject.name == "Avatar")
		{
			other.gameObject.SendMessage ("Death");
		}

		//Quel que soit le collider touché, on detruit la balle
		Destroy (gameObject);
	}
}
