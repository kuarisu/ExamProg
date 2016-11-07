using UnityEngine;
using System.Collections;

public class Trigger_CheckPoint : MonoBehaviour {


    public Transform m_target;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Manager_GameManager.Instance.m_Avatar)
        { 
            StartCoroutine("SaveCheckPoint");
        }

    }

    //Une coroutine est une fonction pouvant etre mise en pause
    //Elle est caracterisée par son IEnumerator a la place du classique void
    void SaveCheckPoint()
    {   
        //puis on met a jour le chekpoint
        MouvementSimple.m_LastCheckpoint = m_target.transform.position;

    }
}
