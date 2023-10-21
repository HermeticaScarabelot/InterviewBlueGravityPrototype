using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{  
    public enum FacingDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    [SerializeField]
    private float rayLength = 1;

    private PlayerMovement playerMovement;
    private LayerMask interactionLayerMask;


    private void Awake()
    {
        interactionLayerMask = LayerMask.GetMask("Interactable");
        if (!playerMovement)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        Debug.DrawLine(transform.position,(Vector3)GetRayDirection() + transform.position);
        CastInteractionRay();
    }

    void CastInteractionRay()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 rayDirection = GetRayDirection();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayLength, interactionLayerMask);
            
            if (hit.collider)
            {
                hit.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    Vector2 GetRayDirection()
    {
        Vector2 rayDirection = playerMovement.playerInputAxis;
        if (rayDirection == Vector2.zero) //If player is not moving, get the current Direction
        {
            switch (playerMovement.facingDirection)
            {
                case FacingDirection.Up:
                    rayDirection = Vector2.up;
                    break;
                case FacingDirection.Down:
                    rayDirection = Vector2.down;
                    break;
                case FacingDirection.Right:
                    rayDirection = Vector2.right;
                    break;
                case FacingDirection.Left:
                    rayDirection = Vector2.left;
                    break;
            }
        }

        return rayDirection;
    }

}
