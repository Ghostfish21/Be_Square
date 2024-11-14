using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class GemCounter : MonoBehaviour
{
    public TextMeshProUGUI gemCounterText;
    public RawImage DiamondIcon;

    public int goal = 12;

    void Update()
    {
        if (GemManager.collected == goal) 
        {
            gemCounterText.text = "All Clear!";
            DiamondIcon.enabled = false;
           
        }
        else
        {
            gemCounterText.text = GemManager.collected.ToString();
        }

        if (GemManager.collected >= 10) 
        {
            DiamondIcon.enabled = false;
        
        }
    }
}
