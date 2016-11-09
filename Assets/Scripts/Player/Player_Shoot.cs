using UnityEngine;
using System.Collections;

public class Player_Shoot : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetComponent<Player_NbBullet>().SubstractBullet();
            Debug.Log("hihihihihih");
        }
    }

}
