using TarodevController;
using UnityEngine;

public class NinjaIdleState : NinjaBaseState
{
    public NinjaIdleState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine) { }

    public override void EnterState()
    {
        SetAnimation(Player.AnimationType.Idle);
    }

    public override void FrameUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input from player controller
        Vector2 input = _playerController._frameInput.Move;

        // Check if there is horizontal input
        if (input.x != 0 && _playerController._grounded)
        {
            // Change to run state
            _stateMachine.ChangeState(_player.RunState);
        }

        // Check for jump input
        if (_playerController._frameInput.JumpDown)
        {
            // Change to jump state
            _stateMachine.ChangeState(_player.JumpState);
        }

        // Check for attack input
        if (Input.GetMouseButtonDown(0))
        {
            // Change to attack state
            _stateMachine.ChangeState(_player.AttackState);
        }

        // Check for throw input
        if (Input.GetMouseButtonDown(1))
        {
            // Change to throw state
            _stateMachine.ChangeState(_player.ThrowState);
        }
    }

    public override void SetAnimation(Player.AnimationType animationType)
    {
        // set anim bool isIdle to true
        _player.SetAnimation(animationType);
    }
}