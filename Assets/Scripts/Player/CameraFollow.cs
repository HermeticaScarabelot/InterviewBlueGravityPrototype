using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 5.0f; 

    private void LateUpdate()
    {
        if (target == null)
        {
            return; 
        }
     
        Vector3 desiredPosition = target.position;
        desiredPosition.z = -10f;

        // Use Lerp to smoothly move the camera towards the player
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
