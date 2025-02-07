using UnityEngine;
using TarodevController;

public class NinjaRunState : NinjaBaseState
{


    public NinjaRunState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Run State");
        SetAnimation(Player.AnimationType.Run);
    }

    public override void FrameUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input from player controller
        Vector2 input = _playerController._frameInput.Move;

        // Check if there is no horizontal input
        if (input.x == 0)
        {
            // Change to idle state
            _stateMachine.ChangeState(_player.IdleState);
        }

        // Check if jump input from player controller
        if (_playerController._frameInput.JumpDown)
        {
            // Change to jump state
            _stateMachine.ChangeState(_player.JumpState);
        }

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
        
    }

    public override void SetAnimation(Player.AnimationType animationType)
    {
        Debug.Log("Set Animation: " + animationType);
        // set anim bool isRun to true
        _player.SetAnimation(animationType);
    }
}