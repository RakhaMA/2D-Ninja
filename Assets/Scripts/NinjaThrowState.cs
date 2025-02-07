using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class NinjaThrowState : NinjaBaseState
{
    private float _throwCooldown = 0.1f;
    private float _throwCooldownTimer;

    public NinjaThrowState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine){}

    public override void EnterState()
    {
        _throwCooldownTimer = _throwCooldown;

        // check if character is jumping
        if (!_playerController._grounded)
        {
            // Trigger Jump Throw
            Debug.Log("Jump Throw");
            JumpThrow();
        }
        else if (_playerController._grounded)
        {
            // Trigger Ground Throw
            GroundThrow();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _throwCooldownTimer -= Time.deltaTime;
        if (_throwCooldownTimer <= 0 && _playerController._grounded)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetAnimation(Player.AnimationType animationType)
    {
        base.SetAnimation(animationType);
    }

    private void JumpThrow()
    {
        // Jump Throw logic
        SetAnimation(Player.AnimationType.JumpThrow);
    }

    private void GroundThrow()
    {
        // Ground Throw logic
        SetAnimation(Player.AnimationType.Throw);
    }
}
