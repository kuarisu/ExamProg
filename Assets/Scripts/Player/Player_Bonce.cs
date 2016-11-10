using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Bonce : MonoBehaviour
{
    Rigidbody m_Rb;
    Vector3 m_direction;
    Vector3 m_startPos;
    [SerializeField]
    Collision m_colAvatar;
    public float m_avatarSpeed = 15;
    public GameObject m_avatarTarget;

    [HideInInspector]
    public bool m_isMoveable;

    private Vector3 m_previousPos;

    public GameObject m_targetforAvatar;

    [HideInInspector]
    public bool m_canStopAgain = true;

    public Image m_fading;

    void Start()
    {
        if (Manager_GameManager.Instance.m_Avatar == null)
            Manager_GameManager.Instance.m_Avatar = this.gameObject;

        if (Manager_GameManager.Instance.m_lastCheckpoint == Vector3.zero)
        {
            Manager_GameManager.Instance.m_lastCheckpoint = transform.position;
        }
        else
        {
            transform.position = Manager_GameManager.Instance.m_lastCheckpoint;
        }

        Manager_GameManager.Instance.m_playerPaused = true;

        m_canStopAgain = true;
        m_isMoveable = false;
        m_Rb = GetComponent<Rigidbody>();
        m_previousPos = transform.position;

        StartCoroutine(FadingIn());
        //PushedByPlayer();
    }

    IEnumerator FadingIn()
    {
        while (m_fading.color.a > 0)
        {
            m_fading.color = new Color(m_fading.color.r, m_fading.color.g, m_fading.color.b, m_fading.color.a - 0.03f);
            yield return new WaitForSeconds(0.02f);
        }
        m_fading.color = new Color(m_fading.color.r, m_fading.color.g, m_fading.color.b, 0.0f);

        Manager_GameManager.Instance.m_playerPaused = false;
    }


    public void PushedByPlayer()
    {
        m_isMoveable = true;
        m_direction = transform.forward.normalized;
        StartCoroutine(Pushed());

    }

    public void PushedByPlateform()
    {
        StopAllCoroutines();
        m_isMoveable = true;
        Vector3 _colNormale = m_colAvatar.contacts[0].normal;
        m_direction = Vector3.Reflect(m_colAvatar.contacts[0].point - m_previousPos,_colNormale).normalized;

        Quaternion rotation = Quaternion.LookRotation(m_direction);
        transform.rotation = rotation;

        m_previousPos = transform.position;
        StartCoroutine(Pushed());
        m_colAvatar = null;


    }


    private IEnumerator Pushed()
    {


        while (m_isMoveable == true)
        {
 
            m_Rb.velocity = m_direction* m_avatarSpeed;
            yield return new WaitForEndOfFrame();
        }

        m_Rb.velocity = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Plateforme")
        {
            m_canStopAgain = true;

            if (m_colAvatar == null)
            {
                m_colAvatar = col;
                PushedByPlateform();
            }
        }
    }
}
