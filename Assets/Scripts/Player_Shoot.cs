using UnityEngine;
using System.Collections;

public class Player_Shoot : MonoBehaviour {

    public GameObject m_prefabBullet;

    public float m_speedBullet;

    bool m_canShoot = true;
    public float m_timer;

    void Start()
    {
        m_canShoot = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && m_canShoot == true && GetComponent<Player_NbBullet>().m_NbBullets > 0)
        {
            StartCoroutine(Shooting());
            m_canShoot = false;       
        }
    }

    IEnumerator Shooting()
    {
        GetComponent<Player_NbBullet>().SubstractBullet();
        GameObject _clone;
                _clone = Instantiate(m_prefabBullet, this.transform.position, Quaternion.identity) as GameObject;
                _clone.transform.LookAt(transform.position + transform.forward);
                _clone.GetComponent<Bullet_moving>().m_speed = m_speedBullet / 10;
        yield return new WaitForSeconds(m_timer);
        m_canShoot = true;
    }

}
