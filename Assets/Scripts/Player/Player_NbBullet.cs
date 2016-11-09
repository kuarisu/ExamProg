using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_NbBullet : MonoBehaviour {

    
    public int m_NbBullets = 3;
    public Text m_TextNbBullets;

    public void AddBullet()
    {
        m_NbBullets++;
        m_TextNbBullets.text = m_NbBullets.ToString();
    }

    public void SubstractBullet()
    {
        if(m_NbBullets > 0)
        {
            m_NbBullets--;
            m_TextNbBullets.text = m_NbBullets.ToString();
        }
    }

}
