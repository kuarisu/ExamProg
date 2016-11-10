using UnityEngine;
using System.Collections;

public class Turret_Destroy : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet_Player")
        {
            Destroy(gameObject);
        }
    }
}
