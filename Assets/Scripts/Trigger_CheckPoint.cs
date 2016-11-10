using UnityEngine;
using System.Collections;

public class Trigger_CheckPoint : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveCheckPoint();
        }

    }

    void SaveCheckPoint()
    {
        Manager_GameManager.Instance.m_lastCheckpoint = transform.position;

    }
}
