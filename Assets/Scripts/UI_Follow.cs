using UnityEngine;
using System.Collections;

public class UI_Follow : MonoBehaviour {

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float m_distance = 0.1f;

    void Start()
    {
        target = Manager_GameManager.Instance.m_Avatar.transform;
    }

    void LateUpdate()
    {
        transform.position = target.position;

        // Set the height of the camera
        transform.position = new Vector3(m_distance, transform.position.y, transform.position.z);

    }
}
