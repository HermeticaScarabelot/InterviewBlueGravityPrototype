using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerInputAxis;
    public float movementSpeed;
    public PlayerInteractionManager.FacingDirection direction;

    [SerializeField] private PlayerAnimationController playerAnimationController;

    private Vector3 defaultScale;

    
    private void Awake()
    {
        if (!playerAnimationController)
        {
            playerAnimationController = GetComponent<PlayerAnimationController>();
        }
        
        //set defaultScale, useful for Flipping
        defaultScale = transform.localScale;
    }

    void Update()
    {
        Move(Time.deltaTime);
    }

    void Move(float deltaTime)
    {
        playerInputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (playerInputAxis != Vector2.zero)
        {
            Vector2 dirNormalized = playerInputAxis.normalized;
            Vector2 step = dirNormalized * (deltaTime * movementSpeed);
            transform.Translate(step, Space.World);
            
            UpdatePlayerDirection();
            FlipSprite();
            UpdateMovementAnimationState(PlayerAnimationController.PlayerState.Running);
        }
        else
        {
            UpdateMovementAnimationState(PlayerAnimationController.PlayerState.Idle);
        }
        

    }
    
    void UpdateMovementAnimationState(PlayerAnimationController.PlayerState newState)
    {
        if (playerAnimationController.inSpecialPlayerState)
        {
            return;
        }
        
        playerAnimationController.activePlayerState = newState;
    }

    void FlipSprite()
    {
        if (playerInputAxis.x > 0 && transform.localScale.x < 0) //Moving right while facing Left
        {
            transform.localScale = defaultScale;
        } else if (playerInputAxis.x < 0 && transform.localScale.x > 0) //Moving left while facing Right
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y,defaultScale.z);
        }
    }

    void UpdatePlayerDirection()
    {
        //Convert the player Input towards a vector2. Useful for when getting close to a npc to get the last Pressed Direction
        //This way you can still draw a raycast towards the last direction you where looking even if you don't rotate anything
        switch (playerInputAxis.normalized)
        {
            case Vector2 vec2 when vec2 == Vector2.up:
                direction = PlayerInteractionManager.FacingDirection.Up;
                break;
            case Vector2 vec2 when vec2 == Vector2.down:
                direction = PlayerInteractionManager.FacingDirection.Down;
                break;
            case Vector2 vec2 when vec2 == Vector2.right:
                direction = PlayerInteractionManager.FacingDirection.Right;
                break;
            case Vector2 vec2 when vec2 == Vector2.left:
                direction = PlayerInteractionManager.FacingDirection.Left;
                break;
        }
    }
}
