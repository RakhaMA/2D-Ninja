using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public abstract class NinjaBaseState
{
    protected Player _player;
    protected PlayerController _playerController;
    protected NinjaStateMachine _stateMachine;

    protected NinjaBaseState(Player player, PlayerController playerController, NinjaStateMachine stateMachine)
    {
        _player = player;
        _playerController = playerController;
        _stateMachine = stateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public virtual void PhysicsUpdate() {}
    public virtual void SetAnimation(Player.AnimationType animationType) 
    {
        _player.SetAnimation(animationType);
    }
    public virtual void AnimationTriggerEvent(Player.AnimationTriggerType triggerType) {}
}
