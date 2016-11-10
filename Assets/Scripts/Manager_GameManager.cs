using UnityEngine;
using System.Collections;

public class Manager_GameManager : MonoBehaviour {

    public static Manager_GameManager Instance;

    [SerializeField]
    private GameObject m_Player;

    [HideInInspector]
    public GameObject m_Avatar;

    public Vector3 m_lastCheckpoint;
   
    [HideInInspector]
    public bool m_playerPaused;

    private void Awake()
    {
        m_Avatar = m_Player;

        if (Manager_GameManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Manager_GameManager.Instance = this;
        DontDestroyOnLoad(this.gameObject);
        m_playerPaused = false;
    }
}
