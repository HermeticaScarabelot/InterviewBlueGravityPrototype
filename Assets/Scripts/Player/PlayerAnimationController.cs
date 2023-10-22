using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public enum PlayerState
    {
        Idle,
        Running,
        Attacking
    }

    public PlayerState activePlayerState;
    public bool inSpecialPlayerState;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private AnimationClip runAnimation;
    [SerializeField] private AnimationClip attackAnimation;

    [SerializeField] private PlayerMovement playerMovement;
    
    private void Awake()
    {
        if (!playerMovement)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        UpdateIdleRunAnimation();

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine("PlayAttack");
        }
    }

    IEnumerator PlayAttack()
    {
        if (activePlayerState == PlayerState.Attacking || inSpecialPlayerState)
        {
            yield break;
        }
        activePlayerState = PlayerState.Attacking;
        inSpecialPlayerState = true;
        playerAnimator.Play(attackAnimation.name);
        yield return new WaitForSeconds(attackAnimation.length);
        inSpecialPlayerState = false;
    }


    void UpdateIdleRunAnimation()
    {
        if (inSpecialPlayerState)
        {
            return;
        }
        
        switch (activePlayerState)
        {
            case PlayerState.Idle:
                playerAnimator.Play(idleAnimation.name);
                break;
            case PlayerState.Running:
                playerAnimator.Play(runAnimation.name);
                break;
        }
    }
}
