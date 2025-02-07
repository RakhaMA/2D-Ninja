using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class NinjaGlideState : NinjaBaseState
{
    public NinjaGlideState(Player player, PlayerController playerController, NinjaStateMachine stateMachine) : base(player, playerController, stateMachine){}

    public override void EnterState()
    {
        // Trigger Glide
        Debug.Log("Glide");
        Glide();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        // check if character is not gliding anymore
        if (!_playerController._isGliding && _playerController._frameInput.Move.x == 0)
        {
            // Change to idle state
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

    private void Glide()
    {
        // Glide logic
        SetAnimation(Player.AnimationType.Glide);
    }
}
