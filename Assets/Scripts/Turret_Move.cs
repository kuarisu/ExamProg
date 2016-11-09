using UnityEngine;
using System.Collections;

public class Turret_Move : MonoBehaviour {

    [SerializeField]
    int m_Speed;
    //[HideInInspector]
    public bool m_moving = true;
    public bool m_canMove;
    public int m_distanceRayCast;
    public RaycastHit m_Hit;
    float m_heightTurret;
    Rigidbody m_Rb;
    Vector3 m_direction = Vector3.forward;

    void Start () {

        m_Rb = GetComponent<Rigidbody>();

        m_heightTurret = transform.localScale.y / 2 + 0.5f;
        StartMovement();

    }

    public void StartMovement()
    {
        if (m_moving && m_canMove)
            StartCoroutine("Moving");
    }

    

	IEnumerator Moving()
    {
        while (m_moving == true)
        {
            Vector3 _rayCastDirection = - Vector3.up.normalized;
            Vector3 _rayCastOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z + m_distanceRayCast);
            Ray groundingRay = new Ray(_rayCastOrigin, _rayCastDirection);

            if (Physics.Raycast(groundingRay, out m_Hit, m_heightTurret))
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
            }

            yield return new WaitForEndOfFrame();
        }
        m_Rb.velocity = Vector3.zero; 
    }
}
