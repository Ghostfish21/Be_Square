using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemCounter : MonoBehaviour
{
    public TextMeshProUGUI gemCounterText;

    void Update()
    {
        if (GemManager.collected == 12) 
        {
            gemCounterText.text = "All Clear!";
        }
        else
        {
            gemCounterText.text = "(" + GemManager.collected.ToString() + " / 12)";
        }
    }
}
