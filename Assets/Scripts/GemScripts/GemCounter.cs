using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemCounter : MonoBehaviour
{
    public TextMeshProUGUI gemCounterText;
    public int goal = 12;

    void Update()
    {
        if (GemManager.collected == goal) 
        {
            gemCounterText.text = "All Clear!";
        }
        else
        {
            gemCounterText.text = "(" + GemManager.collected.ToString() + " /" + goal +")";
        }
    }
}
