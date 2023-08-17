using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    [SerializeField] private Slider gauge;
        
    public float MinValue
    {
        get => gauge.minValue;
        set => gauge.minValue = value;
    }

    public float MaxValue
    {
        get => gauge.maxValue;
        set => gauge.maxValue = value;
    }
}