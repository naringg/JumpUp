using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    
    public float maxValue; //�ִ�ġ

    public float startValue; //���۽��� ��

    public float curValue; //���簪

    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}