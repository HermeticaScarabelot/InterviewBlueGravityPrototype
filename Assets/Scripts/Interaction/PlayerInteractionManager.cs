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

    [SerializeField] private float rayLength = 1; //Raycast Length

    private PlayerMovement playerMovement;
    private LayerMask interactionLayerMask; //Only cast on Interactable Layer
    private Camera cam;

    private void Awake()
    {
        // Initialize references and settings when the object is created
        cam = Camera.main;
        interactionLayerMask = LayerMask.GetMask("Interactable");
        if (!playerMovement)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position,(Vector3)GetRayDirection() + transform.position);
        CastInteractionRay();
    }

    void CastInteractionRay()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 rayDirection = GetRayDirection();
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rayDirection, rayLength, interactionLayerMask);

            if (hits.Length <= 0)
            {
                return;
            }
            
            if (hits[0].collider)
            {
                Interactable[] interactables = hits[0].collider.GetComponents<Interactable>();
                CallInteractions(interactables);
            }
        }
        
        // Interaction using right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            var direction = mousePos - transform.position;
            direction.Normalize();
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, rayLength, interactionLayerMask);

            if (hits.Length <= 0)
            {
                return;
            }
            
            if (hits[0].collider)
            {
                Interactable[] interactables = hits[0].collider.GetComponents<Interactable>();
                CallInteractions(interactables);
            }
        }
    }
    
    // Determine the ray direction based on player input and facing direction
    Vector2 GetRayDirection()
    {
        Vector2 rayDirection = playerMovement.playerInputAxis;
        if (rayDirection == Vector2.zero) //If player is not moving, get the current Direction
        {
            switch (playerMovement.direction)
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
    
    // Call the "Interact" method on interactable objects
    void CallInteractions(Interactable[] interactables)
    {
        foreach (var interactable in interactables)
        {
            if (interactable.isActiveAndEnabled)
            {
                interactable.Interact();

            }
        }
    }

}
