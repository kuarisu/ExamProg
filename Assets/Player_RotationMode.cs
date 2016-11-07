﻿using UnityEngine;
using System.Collections;

public class Player_RotationMode : MonoBehaviour {

    Player_Bonce m_PlayerBonce;
    bool m_canRotate;
    public int m_speed;


    void Start()
    {
        m_PlayerBonce = GetComponent<Player_Bonce>();
        m_canRotate = false;


    }

	void Update ()
    {
        Debug.Log(m_canRotate);

        if (m_PlayerBonce.m_isMoveable == true && Input.GetKeyDown(KeyCode.Mouse0) && m_PlayerBonce.m_canStopAgain == true)
        {
            m_canRotate = true;
            m_PlayerBonce.m_isMoveable = false;
            StartCoroutine("Rotation");
            m_PlayerBonce.m_canStopAgain = false;

        }
        else if (m_PlayerBonce.m_isMoveable == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_canRotate = false;
            m_PlayerBonce.PushedByPlayer();
            StopCoroutine("Rotation");
        }

    }

    IEnumerator Rotation()
    {
        Debug.Log("ds<fhksdfhjskdf");

        while (m_canRotate == true)
        {
            Debug.Log("hello");
            transform.Rotate(m_speed * Time.deltaTime, 0,0);
            yield return new WaitForEndOfFrame();
        }

    }
}