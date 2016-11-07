//Ici on declare les bibliotheques de fonctions
//Celle ci sont presente par defaut
using UnityEngine;
using System.Collections;
//Celle la est ajouté et conserne le load de scene
using UnityEngine.SceneManagement;

//Ici on declare la classe (automatique) le nom de la classe doit etre le meme que le nom du script
public class MouvementSimple : MonoBehaviour { 

	//Cette variable permet de stocker une reference au rigidbody attaché au gameObject (dans l'inspector)
	public Rigidbody m_Rigidbody; 

	//Ici on trouve la vitesse de base ainsi que les modificateurs des zonne de vitesse et lenteur
	public float m_Speed;
	public float m_AddSpeed;
	public float m_DecreaseSpeed;

	//current vitesse est la vitesse actuelle
	private float m_CurrentSpeed;

	//Cette variable permet de régler la puissance du saut
	public float m_JumpPower;

	//Cette variable permet de stocker les input horrizontaux et verticaux pour optimiser l'ecriture du code
	private Vector2 m_StockedInput;

	//Cette variable indique si l'avatar as les pied au sol (true) ou non (false)
	private bool m_Grounded;

	//Cette variable permet de suspendre les input du joueur pendant notament la teleportation
	private bool m_Controlable = true; 

	//Cette variable contient le nom de la scene a loader lorsque l'on touche le trigger de fin de niveau
	public string m_NextLevelName;

	//Cette variable statique permet de conserver la position du dernier checkpoint lorsqu'on reload la scene
	public static Vector3 m_LastCheckpoint;

	//Start est appelée automatiquement par unity à la fin du chargement de la scene. 
	// Use this for initialization
	void Start () 
	{
        m_Rigidbody = GetComponent<Rigidbody>();
		//Si on veux utiliser DontDestroyOnLoad (); pour faire survivre cet gameObject au changement de scene
		//c'ets ici qu'il faut l'ecrire

		//Au demarage de la scene, on met la vitesse a sa valeur de base. 
		m_CurrentSpeed = m_Speed;

		//Si aucun checkpoint n'est enregistré, on enregistre la position de depart comme 1er checkpoint
		if (m_LastCheckpoint == Vector3.zero)
		{
			m_LastCheckpoint = transform.position;
		} 
		else
		{
			//Si par contre un checkpoint est enregistré, on teleporte l'Avatar dessu
			transform.position = m_LastCheckpoint;
		}
	}
	
	// Update s'effectue a toute les frames, FixedUpdate s'effectue toute les 0.02sec
	//Utiliser un fixedUpdate permet dans certains cas de s'afranchire des variation de framerate qui peuvent ralentire ou accelere el jeu
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Ce if qui englobe tout le contenu de fixed update permet d'ignorer temporairement les input du joueur
		if(m_Controlable == true)
		{
			//ici on stoque les input horizontaux et verticaux du joueur dans une seule variable
			//On multiplie ensuite cette variable par la vitesse actuelle
			m_StockedInput = new Vector2 (0, Input.GetAxis("Horizontal")) * m_CurrentSpeed;
			//puis on utilise cette variable pour modifier velocity, la vitesse instentanée du rigidbody
			//les valeurs en x et en z sont remplacée tantdis que la valeur en y est conservée
			m_Rigidbody.velocity = new Vector3 (m_StockedInput.x,m_Rigidbody.velocity.y,m_StockedInput.y);
		
			//dans le but de limiter le saut, on enregistre si l'avatar touche le sol ou non
			//pour ce faire on utilise un raycast, un trais qui part d'un point dans une direction sur une distance
			//et qui renvois true si le trais touche un collider
			//ici le trais part du centre de l'avatar, vers le bas, sur une distance un peut plus grande que la moitié du collider
			if (Physics.Raycast (transform.position, Vector3.down, 1.01f))
			{
				m_Grounded = true;
			} 
			else
			{
				m_Grounded = false;
			}

			//Si l'avatar à les pied au sol et que le joueur apuis sur espace, l'avatar saute
			if(Input.GetKeyDown(KeyCode.Mouse0) && m_Grounded == true)
			{
				//le saut prend ici la forme d'une agmentation de la vitesse instentanée en y
				m_Rigidbody.velocity += Vector3.up*m_JumpPower;
			}
		}

	}

	//Cette fonction est appelée automatiquement par unity lors de la collision du rigidbody avec un collider
	//ici nous cherchons a determiner si le collider en question est une plateforme mobile sous les pieds de l'avatar
	void OnCollisionEnter(Collision other)
	{
		//En ayant préalablement tagué les plateforme avec le tag "Plateforme", on peut les identifier
		if(other.gameObject.tag == "Plateforme")
		{
			//Une foi que l'on est sur que le collider est une plateforme, on s'assure qu'il est sous les pied de l'avatar
			//Pour ce faire nous comparons la position de l'avatar - 90% de la moitié de sa hauteur
			//avec la position de la plateforme + 90% de la moitié de sa hauteur
			if(transform.position.y - 0.9f > other.transform.position.y + other.collider.bounds.extents.y*0.9f)
			{
				//Si la plateforme est effectivement sous les pied de l'avatar, elle devient son parent
				transform.parent = other.transform;
			}
		}
	}

	//De la meme façon, lorsqu'on quite une collision, on refait les meme verification
	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag == "Plateforme")
		{
			if (transform.position.y - 0.9f > other.transform.position.y + other.collider.bounds.extents.y * 0.9f)
			{
				//Si c'ets bien de la plateforme que l'avatar s'est éloigné, son parent redevien null.
				transform.parent = null;
			}
		}
	}

	//Cette fonction est appelée par un trigger message et sert a reloader la scene en cas de mort
	void Death()
	{
		//Remarquez que SceneManager necéssite une bibliotheque supplementaire using UnityEngine.SceneManagement
		//ici on accéde à l'index de la scene actuelement loadé pour la re-loader
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	//Cette fonction est appelée par un trigger message et sert a passer au niveau suivant
	void GoToNextLevel()
	{
		//ici le checkpoint est remis a zero avant le load
		m_LastCheckpoint = Vector3.zero;
		//nottez que nextLevelName est un string qui doit etre assigné dans l'inspector
		SceneManager.LoadScene (m_NextLevelName);
	}

	//Cette fonction est appelée par un trigger message et augmente la vitesse
	void SpeedModeOn()
	{
		m_CurrentSpeed += m_AddSpeed;
	}

	//Cette fonction est appelée par un trigger message et retablis la vitesse augmenté par la fonction precedente
	void SpeedModeOff()
	{
		m_CurrentSpeed -= m_AddSpeed;
	}
	//Cette fonction est appelée par un trigger message et diminu la vitesse
	void SlowModeOn()
	{
		m_CurrentSpeed -= m_DecreaseSpeed;
	}
	//Cette fonction est appelée par un trigger message et retablis la vitesse diminuée par la fonction precedente
	void SlowModeOff()
	{
		m_CurrentSpeed += m_DecreaseSpeed;
	}

	//Cette fonction est appelée par un trigger message et indique qu'il faut temporairement ignorer les input du joueur
	void PauseMouvement()
	{
		
		m_Controlable = false; 
		//pour s'assurer que l'Avatar s'arette instentanément, on met sont accélération instentané a zero
 		m_Rigidbody.velocity = Vector3.zero; 
	}

	//Cette fonction est appelée par un trigger message et redonne ses input au joueur
	void UnPauseMouvement()
	{
		
		m_Controlable = true; 
	}
}
