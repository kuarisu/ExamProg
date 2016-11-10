using UnityEngine;
using System.Collections;

public class Turret_Shoot : MonoBehaviour
{

    public GameObject m_prefabBullet;

    public float m_distance;
    //[HideInInspector]
    public float m_timer;
    public float m_pace;
    public float m_speedBullet;

    public bool m_targetingAvatar = false;
    public GameObject m_targetAvatar;

    bool m_canTarget;
     bool m_isCoroutineStarted = false;
    Vector3 m_previousRotation;


    void Start()
    {
        Invoke("Delay", 0.1f);
    }


    void Delay()
    {
        m_targetAvatar = Manager_GameManager.Instance.m_Avatar;
    }

    void Update()
    {
        if (m_targetingAvatar == true && m_targetAvatar != null)
        {
            if (Vector3.Distance(transform.position, m_targetAvatar.transform.position) < m_distance && !m_isCoroutineStarted)
            {
                m_previousRotation = transform.eulerAngles;
                m_canTarget = true;
                GetComponent<Turret_Move>().m_moving = false;
                StartCoroutine("Targeting");
            }
            else
            {
                m_canTarget = false;
                GetComponent<Turret_Move>().m_moving = true;
                GetComponent<Turret_Move>().StartMovement();

            }
        }


    }

    IEnumerator Targeting()
    {
        m_isCoroutineStarted = true;
            if (m_canTarget == true && m_targetAvatar.transform.position.y >= transform.position.y)
            {
                transform.LookAt(new Vector3(0, m_targetAvatar.transform.position.y, m_targetAvatar.transform.position.z));
                GetComponent<Turret_Move>().m_moving = false;
                m_timer += Time.deltaTime;

                if (m_timer > m_pace)
                {
                    m_timer = 0;


                    GameObject _clone;
                    _clone = Instantiate(m_prefabBullet, this.transform.position, Quaternion.identity) as GameObject;
                    _clone.transform.LookAt(transform.position + transform.forward);
                    _clone.GetComponent<Bullet_moving>().m_speed = m_speedBullet / 10;
                }

            yield return new WaitForEndOfFrame();
            }
        transform.eulerAngles = m_previousRotation;
        m_isCoroutineStarted = false;
    }
}
