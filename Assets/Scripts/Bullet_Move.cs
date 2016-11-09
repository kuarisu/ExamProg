using UnityEngine;
using System.Collections;

public class Bullet_Move : MonoBehaviour {

    [HideInInspector]
    public float vitesse;

    public float timerDestruct = 15;

    void Update()
    {

        timerDestruct -= Time.deltaTime;

        if (timerDestruct <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(transform.forward * vitesse * Time.deltaTime * 50, Space.World);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
