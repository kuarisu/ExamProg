using UnityEngine;
using System.Collections;

public class TriggerMessage : MonoBehaviour {

	//Le but de triggerMessage est de fournir un outil de LD pouvant appeler des fonction sur des gameObject ciblé

	//Ces deux variable contiennent le nom des fonctions a appeler et doivent etre assigné dans l'ispector
	public string messageOnEnter;
	public string messageOnExit;

	//Cette variable continet une reference au gameObject qui dois ressevoir le message et doit etre assigné dans l'inspector
	public GameObject targetMessage; 

	//Cette variable permet de definir ce trigger comme etant un collectible et de le detruire une foi activé
	public bool selfDestruct = false;

	// Use this for initialization
	void Start () {
	
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
			//puis on s'assure que le message a envoyer n'est pas vide
			if(messageOnEnter != "" && messageOnEnter != " ")
			{
				//enfin on envois le message au gameObject contenu dans targetMessage
				targetMessage.SendMessage (messageOnEnter);

				//Si le trigger est un collectible, on le detruit apré avoir envoyé le message
				if (selfDestruct == true)
				{
					Destroy (gameObject);
				}
			}
		}
	}

	//Pour permetre d'envoyer un message lorsqu'on sort du trigger on utilise cette fonction
	//idem
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "Avatar")
		{
			if(messageOnExit != "" && messageOnExit != " ")
			{
				targetMessage.SendMessage (messageOnExit);

				if (selfDestruct == true)
				{
					Destroy (gameObject);
				}
			}
		}
	}

}
