using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class CalculatorManager : MonoBehaviour
{
    public static CalculatorManager Instance;

    //输入的5位
    public int[] inputs = new int[5];
    public int[] delta = new int[10] { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };

    //乘数的5位，也是转的圈数
    public int[] mults = new int[5];
    public TextMeshPro[] multTexts;

    public Transform[] inputRoots;
    public InputItem[] zeroItems;

    public Transform rotateRoot;

    public int inputValue = 0;
    public int multValue = 0;
    public int resultValue = 0;

    public Transform moveRoot;
    public Vector3 moveBasePos;
    public Vector3 moveDelta;

    public int multMoveIndex = 0;


    public TextMeshPro[] resultTexts;

    private Dictionary<int, InputItem> lastInputValues = new Dictionary<int, InputItem>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetCalculator();
    }

    public void ClickInputItem(InputItem inputItem)
    {
        if (!lastInputValues.ContainsKey(inputItem.index))
        {
            lastInputValues.Add(inputItem.index, inputItem);
        }
        else
        {
            lastInputValues[inputItem.index].BackColor();
            lastInputValues[inputItem.index] = inputItem;
        }

        inputRoots[inputItem.index].DOLocalRotate(inputItem.value * 10 * Vector3.left, 0.03f * inputItem.value);
        inputs[inputItem.index] = inputItem.value;

        UpdateInputValue();
    }

    private void UpdateInputValue()
    {
        inputValue = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            inputValue += delta[i] * inputs[i];
        }
    }

    public void AddRotate()
    {
        rotateRoot.DOLocalRotate(Vector3.left * 180, 0.25f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
        {
            UpdateMult();
            UpdateResult();
            rotateRoot.DOLocalRotate(Vector3.left * 360, 0.25f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
            {
                rotateRoot.localEulerAngles = Vector3.zero;
            });
        });
    }

    private void UpdateMult()
    {
        multValue += delta[multMoveIndex];

        for (int i = 0; i < multTexts.Length; i++)
        {
            multTexts[i].text = $"{(multValue % delta[i + 1]) / delta[i]}";
        }
    }

    public void MultRightMove()
    {
        multMoveIndex++;
        if (multMoveIndex > 4)
            multMoveIndex = 4;
        moveRoot.DOLocalMove(moveBasePos + multMoveIndex * moveDelta, 0.25f);
    }

    public void MultLeftMove()
    {
        multMoveIndex--;
        if (multMoveIndex < 0)
            multMoveIndex = 0;
        moveRoot.DOLocalMove(moveBasePos + multMoveIndex * moveDelta, 0.25f);
    }

    public void UpdateResult()
    {
        resultValue += inputValue * delta[multMoveIndex];
        for (int i = 0; i < resultTexts.Length; i++)
        {
            if (i < resultTexts.Length - 1)
                resultTexts[i].text = $"{(resultValue % delta[i + 1]) / delta[i]}";
            else
                resultTexts[i].text = $"{resultValue / delta[i]}";
        }
    }

    public void ResetCalculator()
    {
        inputValue = 0;
        foreach (var item in zeroItems)
        {
            item.ClickMe();
        }

        multMoveIndex = 0;
        moveRoot.DOLocalMove(moveBasePos + multMoveIndex * moveDelta, 0.25f);

        multValue = 0;
        for (int i = 0; i < multTexts.Length; i++)
        {
            multTexts[i].text = $"{(multValue % delta[i + 1]) / delta[i]}";
        }

        resultValue = 0;
        UpdateResult();
    }

}
