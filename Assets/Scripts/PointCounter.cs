using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    private int maxPoints = 10000;
    private int strikes = 0;
    private int currentPoints = 0;
    
    public void UpdateStrikes()
    { 
        strikes += 1;
        textBox.text = "Strikes: " + strikes.ToString() + "\nPoints: " + currentPoints.ToString();
    }

    public void UpdatePoints()
    {
        strikes += 1;
        currentPoints += maxPoints/strikes;
        textBox.text = "Strikes: " + strikes.ToString() + "\nPoints: " + currentPoints.ToString();
    }
}
