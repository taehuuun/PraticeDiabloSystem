using System;
using System.Collections.Generic;

public sealed class StateMachine<T>
{
    public State<T> CurrentState { get; private set; }
    public State<T> PrevState { get; private set; }
    public float ElapsedTimeInState { get; private set; }

    private Dictionary<Type, State<T>> _states = new Dictionary<Type, State<T>>();
    private T _context;

    public StateMachine(T context, State<T> initializeState)
    {
        _context = context;
        AddState(initializeState);

        CurrentState = initializeState;
        CurrentState.OnEnter();
    }

    public void AddState(State<T> state)
    {
        state.SetStateMachineAndContext(this, _context);
        _states.Add(state.GetType(), state);
    }
    public void Update(float deltaTime)
    {
        ElapsedTimeInState += deltaTime;
        CurrentState.Update(deltaTime);
    }
    public R ChangeState<R>() where R : State<T>
    {
        var newType = typeof(R);

        if (CurrentState.GetType() == newType)
        {
            return CurrentState as R;
        }

        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        PrevState = CurrentState;
        CurrentState = _states[newType];
        
        CurrentState.OnEnter();
        ElapsedTimeInState = 0f;

        return CurrentState as R;
    }
}