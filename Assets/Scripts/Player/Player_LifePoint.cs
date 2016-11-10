using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_LifePoint : MonoBehaviour {

    public List<GameObject> m_LifePointList = new List<GameObject>();

    int m_LifePoint;
    int m_IndexLifePoint;

   

    void Start()
    {
        m_LifePoint = m_LifePointList.Count;
        m_IndexLifePoint = 0;
    }

    void Update()
    {
        if (m_IndexLifePoint >= 5)
            GetComponent<Player_Death>().DeathStart();
    }

    void LostPoint()
    {
        if(m_LifePointList.Count != 0 && m_IndexLifePoint < 5)
        {
            m_LifePointList[m_IndexLifePoint].gameObject.SetActive(false);
            m_IndexLifePoint++;
        }
            
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            LostPoint();
        }

        if (col.gameObject.tag == "Turret")
        {
            StartCoroutine(InstantDeath());
        }
    }

    IEnumerator InstantDeath()
    {
        Manager_GameManager.Instance.m_playerPaused = true;
        yield return new WaitForSeconds(0.05f);
        Manager_GameManager.Instance.m_Avatar.GetComponent<Player_Bonce>().m_avatarSpeed = 0;
        for (int i = 0; i < GetComponent<Player_LifePoint>().m_LifePoint; i++)
        {
            LostPoint();
            yield return new WaitForSeconds(0.3f);
        }

    }
                


}
