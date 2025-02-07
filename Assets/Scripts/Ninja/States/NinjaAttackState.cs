using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class NinjaAttackState : NinjaBaseState
{
    private float _attackCooldown = 0.1f;
    private float _attackCooldownTimer;

    public NinjaAttackState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine){}

    public override void EnterState()
    {
        _attackCooldownTimer = _attackCooldown;

        // check if character is jumping
        if (!_playerController._grounded)
        {
            // Trigger Jump Attack
            JumpAttack();
        }
        else if (_playerController._grounded)
        {
            // Trigger Ground Attack
            GroundAttack();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _attackCooldownTimer -= Time.deltaTime;
        if (_attackCooldownTimer <= 0 && _playerController._grounded)
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

    private void JumpAttack()
    {
        // Jump Attack logic
        SetAnimation(Player.AnimationType.JumpAttack);
    }

    private void GroundAttack()
    {
        // Ground Attack logic
        
        SetAnimation(Player.AnimationType.GroundAttack);
    }
}