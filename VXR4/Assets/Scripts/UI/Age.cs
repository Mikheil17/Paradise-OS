using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Age : MonoBehaviour
{
    public TMP_Text ageText;
    private int age = 0;

    public void Increase()
    {
        if(age == 99)
            return;

        age++;
        ageText.text = age.ToString();
    }

    public void Decrease()
    {
        if(age == 0)
            return;

        age--;
        ageText.text = age.ToString();
    }
}
