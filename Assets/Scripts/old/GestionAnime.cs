using UnityEngine;
using System.Collections;

public class GestionAnime : MonoBehaviour { 

	//Ce script permet de ressevoir les message envoyé par un triggerMessage pour lancer des animation

	//Cette variable contien une reference au composant animator de ce gameObject (voir start)
	public Animator myAnimator;

	// Use this for initialization
	void Start () 
	{
		//ici on assigne l'animator de ce gameObject a la variable myAnimator
		myAnimator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Cette fonction est appelée par un triggerMessage et permet de modifier un parametre dans l'animator de ce gameObject
	//Pour que cela fonctionne, il faut, au sein de l'animator, declarer 2 etat d'animation
	//les relier par des transition et metre comme condition de transition le changement du parametre "Ouverture"
	void PlayAnim1()
	{
		//myAnimator.Play ("Ouverture");
		myAnimator.SetBool("Ouverture",true);
	}

	//idem
	void PlayAnim2()
	{
		//myAnimator.Play ("Fermeture");
		myAnimator.SetBool("Ouverture",false);
	}
}
