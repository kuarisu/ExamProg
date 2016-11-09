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

    public void LostPoint()
    {
        if(m_LifePointList.Count != 0 && m_IndexLifePoint < 5)
        {
            m_LifePointList[m_IndexLifePoint].gameObject.SetActive(false);
            m_IndexLifePoint++;
        }
        else { }
            //GetComponent<Player_Death>().Death()
    }
	
}
