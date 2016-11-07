using UnityEngine;
using System.Collections;

public class Tourelle : MonoBehaviour {

	//Cette variable a assigner dans l'inspector contient le prefab de la balle
	//Le prefab doit etre dans un dossier car si le model disparait de la scene la tourelle arette de tirer
	public GameObject m_prefabBullet;

	//Ces deux variables permettent de tirer toute les cadence secondes et de decaler le premier tir de timer seconde
	public float m_timer; 
	public float m_pace; 

	//Cette variable permet de changer la votesse des balles tirée
	public float m_speedBullet; 

	//Cette variable indique si la tourelle doit viser l'avatar ou non
	public bool m_targetingAvatar = false;

	public GameObject m_targetAvatar;

	// Use this for initialization
	void Start () 
	{
		m_targetAvatar = Manager_GameManager.Instance.m_Avatar;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(transform.position,m_targetAvatar.transform.position) < 20.0f)
		{

			if(m_targetingAvatar == true)
			{
				transform.LookAt (new Vector3(0, m_targetAvatar.transform.position.y, m_targetAvatar.transform.position.z));
			}

			//On compte le temps réél en ajoutant à timer le temps entre deux frame a toute les frame
			m_timer += Time.deltaTime;

			//Quand timer est supperieur a cadence, la tourelle tire
			if (m_timer > m_pace)
			{
				//on Remet timer a zero
				m_timer = 0;

				//puis on declare une variable pour stocker la balle une foi instanciée
				GameObject _clone; 
				//on instancie la balle au centre de la tourelle
				//nottez instanciate créé des "object" il faut donc ajouter "as gameObject" a la fin
				_clone = Instantiate (m_prefabBullet, this.transform.position, Quaternion.identity) as GameObject;
				//une foi la balle instanciée, on l'oriente dans la meme direction que la tourelle
				_clone.transform.LookAt (transform.position + transform.forward);
				//puis, on accéde a sa variable de vitesse et on la modifie
				_clone.GetComponent<Balle>().vitesse = m_speedBullet;
			}
		}
	}
}
