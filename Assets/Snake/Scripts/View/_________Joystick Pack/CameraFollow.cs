using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object that the camera will follow
    public float smoothing = 5f; // Smoothing factor for the camera movement

    private Vector3 offset; // Offset between the camera and the target object

    private void Start()
    {
        offset = new Vector3(0.330000013f, 0.699999988f, -5.88999987f);
        GetComponent<Camera>().fieldOfView = 30;
       if(target == null) target = GameObject.Find("Player").transform;
    }

    // void Start()
    // {
    //     // Calculate the initial offset at the start
    //     offset = transform.position - target.position;
    // }

    void FixedUpdate()
    {
        // Calculate the new camera position
        Vector3 targetCamPos = target.position + offset;

        // Interpolate between the camera's current position and the target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        // If you want to follow only on the X and Y axes, uncomment the line below
        // transform.position = new Vector3(transform.position.x, transform.position.y, offset.z);
    }
}