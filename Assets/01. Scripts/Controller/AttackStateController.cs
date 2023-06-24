using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateController : MonoBehaviour
{
    public delegate void OnEnterAttackState();
    public delegate void OnExitAttackState();

    public OnEnterAttackState enterAttackStateController;
    public OnExitAttackState exitAttackStateController;
    
    public bool IsInAttackState { get; private set; }

    private void Start()
    {
        enterAttackStateController = EnterAttackState;
        exitAttackStateController = ExitAttackState;
    }

    public void OnStateOfAttackState()
    {
        IsInAttackState = true;
        enterAttackStateController?.Invoke();
    }

    public void OnEndOfAttackState()
    {
        IsInAttackState = false;
        exitAttackStateController?.Invoke();
    }
    
    private void EnterAttackState()
    {
        
    }

    private void ExitAttackState()
    {
        
    }
}
