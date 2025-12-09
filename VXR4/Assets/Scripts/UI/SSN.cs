using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SSN : MonoBehaviour
{
    public TMP_Text ssnText;
    public CanvasInteractionManager canvasManager;
    public int canvasIndex;
    private string ssnFormat = "***-**-****";
    private int cursorIndex = 0;

    void Start()
    {
        ssnText.text = ssnFormat;
        cursorIndex = GetNextEditableIndex(0, 1); // Start at first digit
    }

    public void OnNumberPressed(string number)
    {
        int idx = cursorIndex;
        if (idx < ssnText.text.Length && ssnText.text[idx] == '*')
            ssnText.text = ReplaceAt(ssnText.text, idx, number[0]);
            
        cursorIndex = GetNextEditableIndex(idx + 1, 1);

        if (!ssnText.text.Contains("*"))
            OnSSNComplete();
    }
    private int GetNextEditableIndex(int start, int direction)
    {
        int idx = start;
        while (idx >= 0 && idx < ssnText.text.Length)
        {
            if (ssnText.text[idx] != '-')
                return idx;

            idx += direction;
        }

        return Mathf.Clamp(idx, 0, ssnText.text.Length - 1);
    }

    private string ReplaceAt(string text, int index, char newChar)
    {
        char[] chars = text.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    private void OnSSNComplete()
    {
        canvasManager.setTrue(canvasIndex);
    }
}
