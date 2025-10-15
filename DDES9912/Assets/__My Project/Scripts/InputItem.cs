using System;
using UnityEngine;

public class InputItem : MonoBehaviour
{
    //1-5位置
    public int index;

    //0-9对应的数值
    public int value;

    private void Start()
    {
        index = int.Parse(transform.parent.parent.name);
        value = int.Parse(transform.parent.name);
    }

    public void ClickMe()
    {
        CalculatorManager.Instance.ClickInputItem(this);
    }

}
