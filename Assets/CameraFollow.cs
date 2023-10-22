using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform.
    public float smoothSpeed = 5.0f; // Adjust the smoothness as needed.

    private void LateUpdate()
    {
        if (target == null)
        {
            return; // Check if the target (player) is not null.
        }
        // Calculate the desired position for the camera.
        Vector3 desiredPosition = target.position;
        desiredPosition.z = -10f;

        // Use Lerp to smoothly move the camera towards the player.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
