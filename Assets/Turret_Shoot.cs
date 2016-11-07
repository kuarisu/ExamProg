using UnityEngine;
using System.Collections;

public class Turret_Shoot : MonoBehaviour
{

    public GameObject m_prefabBullet;

    public float m_distance;
    [HideInInspector]
    public float m_timer;
    public float m_pace;
    public float m_speedBullet;

    public bool m_targetingAvatar = false;
    public GameObject m_targetAvatar;


    void Start()
    {
        m_targetAvatar = Manager_GameManager.Instance.m_Avatar;
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, m_targetAvatar.transform.position) < m_distance)
        {

            if (m_targetingAvatar == true)
            {
                transform.LookAt(new Vector3(0, m_targetAvatar.transform.position.y, m_targetAvatar.transform.position.z));
            }

            m_timer += Time.deltaTime;

            if (m_timer > m_pace)
            {
                m_timer = 0;


                GameObject _clone;
                _clone = Instantiate(m_prefabBullet, this.transform.position, Quaternion.identity) as GameObject;
                _clone.transform.LookAt(transform.position + transform.forward);
                _clone.GetComponent<Balle>().vitesse = m_speedBullet;
            }
        }
    }
}
