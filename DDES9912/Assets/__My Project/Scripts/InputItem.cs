using System;
using TMPro;
using UnityEngine;

public class InputItem : MonoBehaviour
{
    //1-5λ��
    public int index;

    //0-9��Ӧ����ֵ
    public int value;

    public TextMeshPro valueText;

    private void Start()
    {
        index = int.Parse(transform.parent.parent.name);
        value = int.Parse(transform.parent.name);
    }

    public void ClickMe()
    {
        CalculatorManager.Instance.ClickInputItem(this);
        valueText.color = Color.green;
    }

    public void BackColor()
    {
        valueText.color = Color.white;
    }

}
