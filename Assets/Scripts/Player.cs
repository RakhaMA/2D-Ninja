using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field : SerializeField] public float MaxHealth { get; set; } = 100f;
    [field : SerializeField] public float CurrentHealth { get; set; }
    [HideInInspector] public PlayerController playerController;

#region  State Machine Variables
    public NinjaStateMachine StateMachine { get; set; }
    public NinjaIdleState IdleState { get; set; }
    public NinjaRunState RunState { get; set; }
    public NinjaJumpState JumpState { get; set; }
    public NinjaAttackState AttackState { get; set; }
    public NinjaThrowState ThrowState { get; set; }
    public NinjaGlideState GlideState { get; set; }
    
#endregion

    private Animator _animator;
    public SpriteRenderer _spriteRenderer { get; set; }
    public bool isFacingRight { get; set; } = true;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        StateMachine = new NinjaStateMachine();
        IdleState = new NinjaIdleState(this, playerController, StateMachine);
        RunState = new NinjaRunState(this, playerController, StateMachine);
        JumpState = new NinjaJumpState(this, playerController, StateMachine);
        AttackState = new NinjaAttackState(this, playerController, StateMachine);
        ThrowState = new NinjaThrowState(this, playerController, StateMachine);
        GlideState = new NinjaGlideState(this, playerController, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine._currentState.FrameUpdate();
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        StateMachine._currentState.PhysicsUpdate();
    }

#region  health/damage function

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

#endregion


#region  Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine._currentState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        ThrowKunai,
        Hurt,
        PlayFootstepSound
    }

    public void SetAnimation(AnimationType animationType)
    {
        Debug.Log("Set Animation: " + animationType);
        // Reset all animation states to false
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isRun", false);
        _animator.SetBool("isJump", false);
        _animator.SetBool("isHurt", false);
        _animator.SetBool("isGlide", false);
        _animator.ResetTrigger("Attack");
        _animator.ResetTrigger("JumpAttack");
        _animator.ResetTrigger("Throw");
        _animator.ResetTrigger("JumpThrow");

        // Set the specified animation state to true
        switch (animationType)
        {
            case AnimationType.Idle:
                _animator.SetBool("isIdle", true);
                break;
            case AnimationType.Run:
                _animator.SetBool("isRun", true);
                break;
            case AnimationType.Jump:
                _animator.SetBool("isJump", true);
                break;
            case AnimationType.Hurt:
                _animator.SetBool("isHurt", true);
                break;
            case AnimationType.Glide:
                _animator.SetBool("isGlide", true);
                break;
            case AnimationType.GroundAttack:
                _animator.SetTrigger("Attack");
                break;
            case AnimationType.JumpAttack:
                _animator.SetTrigger("JumpAttack");
                break;
            case AnimationType.Throw:
                _animator.SetTrigger("Throw");
                break;
            case AnimationType.JumpThrow:
                _animator.SetTrigger("JumpThrow");
                break;

            default:
                _animator.SetBool("isIdle", true);
                break;
        }
    }

    public enum AnimationType
    {
        Idle,
        Run,
        Jump,
        Glide,
        Hurt,
        GroundAttack,
        JumpAttack,
        Throw,
        JumpThrow,
    }

    private void FlipPlayer()
    {
        Vector2 input = playerController._frameInput.Move;
        if (input.x > 0)
        {
            // Flip the gameobject, change y rotation to 0
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
        else if (input.x < 0)
        {
            // Flip the gameobject, change y rotation to 180
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
    }

    #endregion
}