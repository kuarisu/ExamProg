using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_Death : MonoBehaviour {

    public Image m_fading;

    public void DeathStart()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        GetComponent<Player_Bonce>().m_isMoveable = false;
        Manager_GameManager.Instance.m_playerPaused = true;

        while (m_fading.color.a < 1)
        {
            m_fading.color = new Color(m_fading.color.r, m_fading.color.g, m_fading.color.b, m_fading.color.a + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
        m_fading.color = new Color(m_fading.color.r, m_fading.color.g, m_fading.color.b, 1.0f);

        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
