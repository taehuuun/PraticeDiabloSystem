using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    #if UNITY_EDITOR
    [Multiline] public string description;
    #endif

    public GameObject effect;
    public LayerMask targetMask;
    
    public int animationIdx;
    public int priority;
    public int damage = 10;
    public float range = 3f;

    [SerializeField] protected float coolTime;
    protected float calCoolTime = 0f;
    
    private void Start()
    {
        calCoolTime = coolTime;
    }

    private void Update()
    {
        if (calCoolTime < coolTime)
        {
            calCoolTime += Time.deltaTime;
        }
    }

    public abstract void ExecuteAttack(GameObject target = null, Transform startPoint = null);
}
