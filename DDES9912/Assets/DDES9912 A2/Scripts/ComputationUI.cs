using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputationUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI resultText;

    public List<string> lastInfos = new List<string>();

    private int lastMutValue = 0, nowMut = 0, inputNow = 0,resultValue = 0;
    private string oldStr = "", nowUseStr = "";
    private string leftStr, middleStr, rightStr;


    public TextMeshProUGUI historyText;

    public TextMeshProUGUI totalText, firstInputText, averageInputText, averageOutText, highestMultText;
    private int total, firstInput, lastInput, inputAll, inputCount, highestMult = 0;
    private double outAll;

    public void UpdateChangeInput(int nowInput, int nowMut)
    {
        if (lastMutValue != nowMut)
        {
            lastMutValue = nowMut;


            if (oldStr == "")
                oldStr = $"{nowUseStr}";
            else
                oldStr = $"{oldStr} + {nowUseStr}";
        }
        inputNow = nowInput;
        if (inputNow != 0)
        {
            leftStr = inputNow.ToString();
            nowUseStr = $"{leftStr}";
        }
        UpdateInput();
    }

    public void UpdateRotate(int mut, int reslut)
    {
        if (inputNow == 0)
            return;

        if (firstInput == 0 && firstInput != inputNow)
        {
            firstInput = inputNow;
            firstInputText.text = firstInput.ToString();
        }
        if(lastInput != inputNow)
        {
            lastInput = inputNow;
            inputCount++;
            inputAll += lastInput;
            averageInputText.text = ((float)inputAll/inputCount).ToString("f2");
        }

        nowMut = mut - lastMutValue;

        if (highestMult < nowMut)
        {
            highestMult = nowMut;
            highestMultText.text = highestMult.ToString();
        }

        if (nowMut <= 1)
        {
            middleStr = "";
            rightStr = "";
        }
        else
        {
            middleStr = " * ";
            rightStr = $"{nowMut}";
        }
        nowUseStr = $"{leftStr}{middleStr}{rightStr}";
        UpdateInput();
        UpdateResult(reslut);
    }

    private void UpdateResult(int result)
    {
        resultValue = result;
        resultText.text = resultValue.ToString();
    }

    private void UpdateInput()
    {
        if (oldStr != "")
            infoText.text = $"{oldStr} + {nowUseStr}";
        else
            infoText.text = nowUseStr;
    }


    public void NewComputationReset()
    {
        leftStr = "";
        middleStr = "";
        rightStr = "";
        lastMutValue = 0;
        nowMut = 0;
        nowUseStr = "";
        oldStr = "";
        resultValue = 0;
        infoText.text = "";
        resultText.text = "";
    }

    public void AddSaveInfo()
    {
        if (resultText.text != "0" && resultText.text != "")
        {
            string info = $"#{lastInfos.Count + 1:d3}: {infoText.text} = {resultText.text}";
            lastInfos.Add(info);
            historyText.text += $"{info}\n";
            outAll += resultValue;
            total = lastInfos.Count;
            totalText.text = total.ToString();
            averageOutText.text = ((float)outAll/total).ToString("f2");
        }
    }

}
