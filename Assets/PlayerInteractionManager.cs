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

    [SerializeField] private float rayLength = 1;

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
            
            /*
            
             List<GameObject> interactablesGo = new List<GameObject>();

            foreach (var hit in hits)
            {
                interactablesGo.Add(hit.collider.gameObject);
            }
            
            foreach (var hit in interactablesGo)
            {
                Debug.Log(hit.name);
            }
            
            if (hits.Length <= 0)
            {
                return;
            }
            Destroy(hits[0].collider.gameObject);

            */

        }
    }

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
