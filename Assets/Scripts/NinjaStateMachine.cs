using UnityEngine;

public class NinjaStateMachine
{
    public NinjaBaseState _currentState { get; set; }

    public void Initialize(NinjaBaseState startingState)
    {
        _currentState = startingState;
        _currentState.EnterState();
    }

    public void ChangeState(NinjaBaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    // public void Update()
    // {
    //     _currentState?.FrameUpdate();
    // }

    // public void PhysicsUpdate()
    // {
    //     _currentState?.PhysicsUpdate();
    // }
}