using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerInputAxis;
    public float movementSpeed;
    public PlayerInteractionManager.FacingDirection facingDirection;
    

    [SerializeField]
    private PlayerAnimationController playerAnimationController;

    private Vector3 defaultScale;

    
    private void Awake()
    {
        if (!playerAnimationController)
        {
            playerAnimationController = GetComponent<PlayerAnimationController>();
        }
        
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
            Debug.Log("wtf");
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
        switch (playerInputAxis.normalized)
        {
            case Vector2 vec2 when vec2 == Vector2.up:
                facingDirection = PlayerInteractionManager.FacingDirection.Up;
                break;
            case Vector2 vec2 when vec2 == Vector2.down:
                facingDirection = PlayerInteractionManager.FacingDirection.Down;
                break;
            case Vector2 vec2 when vec2 == Vector2.right:
                facingDirection = PlayerInteractionManager.FacingDirection.Right;
                break;
            case Vector2 vec2 when vec2 == Vector2.left:
                facingDirection = PlayerInteractionManager.FacingDirection.Left;
                break;
        }
    }
}
