using TarodevController;
using UnityEngine;

public class NinjaJumpState : NinjaBaseState
{
    private bool _isJumping = false;

    public NinjaJumpState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Jump State");
        _isJumping = true;
        SetAnimation(Player.AnimationType.Jump);
    }

    public override void FrameUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input from player controller
        Vector2 input = _playerController._frameInput.Move;

        // Check for attack input from player controller
        if (Input.GetMouseButtonDown(0))
        {
            // Change to attack state
            _stateMachine.ChangeState(_player.AttackState);
        }

        // Check for throw input from player controller
        if (Input.GetMouseButtonDown(1))
        {
            // Change to throw state
            _stateMachine.ChangeState(_player.ThrowState);
        }

        // check for character gliding input
        if (_playerController._isGliding)
        {
            // Change to glide state
            _stateMachine.ChangeState(_player.GlideState);
        }

        _isJumping = false;
        // Check if character is grounded
        if (_playerController._grounded && input.x == 0)
        {
            // Change to idle state
            _stateMachine.ChangeState(_player.IdleState);
        }

        // check if character is grounded and there is horizontal input
        if (_playerController._grounded && input.x != 0)
        {
            // Change to run state
            _stateMachine.ChangeState(_player.RunState);
        }
    }

    public override void SetAnimation(Player.AnimationType animationType)
    {
        Debug.Log("Set Animation: " + animationType);
        // set anim bool isJump to true
        _player.SetAnimation(animationType);
    }
}