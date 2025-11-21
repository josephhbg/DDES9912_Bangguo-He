using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputationUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI resultText;

    public List<string> lastInfos = new List<string>();

    private int lastMutValue = 0, nowMut = 0;
    private string oldStr = "", nowUseStr = "", resultStr = "";
    private string leftStr, middleStr, rightStr;


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
        leftStr = nowInput.ToString();
        nowUseStr = $"{leftStr}";
        UpdateInput();
    }

    public void UpdateRotate(int mut, int reslut)
    {
        nowMut = mut - lastMutValue;
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
        resultStr = result.ToString();
        resultText.text = resultStr;
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
        resultStr = "";
        infoText.text = "";
        resultText.text = "";
    }

    public void AddSaveInfo()
    {
        if (resultText.text != "0" && resultText.text != "")
        {
            lastInfos.Add($"#{lastInfos.Count + 1:d3}: {infoText.text} = {resultText.text}");
        }
    }

}
