using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    [SerializeField] private Slider gauge;
    private Canvas _canvas;
        
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

    public float Value
    {
        get => gauge.value;
        set => gauge.value = value;
    }
}