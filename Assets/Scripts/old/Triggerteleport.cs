using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Triggerteleport : MonoBehaviour {

	//Ces variables permetent de stocker des references a des gameObject dans l'inspector
	public Transform target;
	public Image fondu; 
	public GameObject Avatar; 

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Cette fonction est appelée automatiquement par unity lorsqu'un rigidbody entre dans un trigger
	//Un trigger est un collider dont la case isTrigger est cochée
	void OnTriggerEnter(Collider other)
	{
		//Ici on s'assure que c'est bien l'avatar qui rentre dans le trigger
		if(other.gameObject.name == "Avatar")
		{

			//Une foi sur de cela, on lance une coroutine de fondu pour masquer la teleportation
			StartCoroutine("PerformTeleport");
		}

	}

	//Une coroutine est une fonction pouvant etre mise en pause
	//Elle est caracterisée par son IEnumerator a la place du classique void
	IEnumerator PerformTeleport()
	{
		//On commence par appeler une fonction sur l'avatar pour ignorer les input du joueur
		Avatar.SendMessage ("PauseMouvement");

		//puis on utilise une boucle while(expretion){instruction;} 
		//tant que l'expretion est vraie, on continu a effectuer l'instruction en boucle
		//ATTENTION : si l'instruction ne rend pas l'expretion fausse au bout d'un moment la boucle est infinie et le programme plante
		while(fondu.color.a < 1)
		{
			//ici on augmente progressivement le parametre alpha de la couleur du fondu
			fondu.color = new 	Color (fondu.color.r, fondu.color.g, fondu.color.b, fondu.color.a + 0.1f);
			//et on met la fonction en pause pour environ une frame entre chaque augmentations
			yield return new WaitForSeconds (0.02f);
		}
		//une foi l'augmentation terminée, on fixe la valeur de l'alpha a 1 pour s'assurer de son opacité
		fondu.color = new Color (fondu.color.r, fondu.color.g, fondu.color.b, 1.0f);

		//Une foi le fondu opaque, on teleporte l'avatar vers sa destination
		Avatar.transform.position = target.transform.position;
		//puis on met a jour le chekpoint
		MouvementSimple.m_LastCheckpoint = target.transform.position;

		//On réutilise ensuite un while avec pause pour re-rendre trensparent le fondu
		while(fondu.color.a > 0)
		{
			fondu.color = new Color (fondu.color.r, fondu.color.g, fondu.color.b, fondu.color.a - 0.1f);
			yield return new WaitForSeconds (0.02f);
		}

		fondu.color = new Color (fondu.color.r, fondu.color.g, fondu.color.b, 0.0f);

		//enfin, on redonne ses controles au joueur par un appel de fonction;
		Avatar.SendMessage ("UnPauseMouvement");
	}
}
