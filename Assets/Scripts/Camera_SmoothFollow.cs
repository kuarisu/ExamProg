using UnityEngine;
using System.Collections;

public class Camera_SmoothFollow : MonoBehaviour {


    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float m_distance = 10.0f;

    void Start()
    {
        target = Manager_GameManager.Instance.m_Avatar.transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);

        // Set the height of the camera
        transform.position = new Vector3(m_distance, transform.position.y, transform.position.z);

    }
}
