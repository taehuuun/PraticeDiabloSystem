public abstract class State<T>
{
    protected StateMachine<T> stateMachine;
    protected T context;

    public State()
    {
        
    }

    internal void SetStateMachineAndContext(StateMachine<T> stateMachine, T context)
    {
        this.stateMachine = stateMachine;
        this.context = context;
        OnInitialized();
    }
    public virtual void OnInitialized() { }
    public virtual void OnEnter() { }
    public abstract void Update(float deltaTime);
    public virtual void OnExit() { }
}