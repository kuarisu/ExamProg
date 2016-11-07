using UnityEngine;
using System.Collections;

public class Turret_Move : MonoBehaviour {

    [SerializeField]
    int m_Speed;
    public bool m_moving = false;
    [HideInInspector]
    public bool m_canMove;
    public int m_distanceRayCast;
    public RaycastHit m_Hit;
    float m_heightTurret;
    Rigidbody m_Rb;
    Vector3 m_direction = Vector3.forward;

    void Start () {

        m_Rb = GetComponent<Rigidbody>();

        if (m_moving)
            m_canMove = true;

        m_heightTurret = transform.localScale.y / 2 + 0.5f;
        StartCoroutine(Moving());

	}

	IEnumerator Moving()
    {
        while (m_moving && m_canMove)
        {
            Vector3 _rayCastDirection = - Vector3.up.normalized;
            Vector3 _rayCastOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z + m_distanceRayCast);
            Ray groundingRay = new Ray(_rayCastOrigin, _rayCastDirection);

            if (Physics.Raycast(groundingRay, out m_Hit))
            {

                if (m_Hit.collider.tag == "Plateforme")
                {
                    m_Rb.velocity = m_direction * m_Speed;
                }
          
            }
            else
            {

                transform.eulerAngles += new Vector3(0, 180, 0);
                m_direction *= -1;
                m_distanceRayCast *= -1;
                //m_Rb.velocity = m_direction * m_Speed;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
