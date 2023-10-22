using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateDisplay : MonoBehaviour
{
    public float updateRepeatRate = 1.0f;

    [SerializeField]private int currentHour = 7;
    [SerializeField]private int currentMinute = 10;

    [SerializeField] private TextMeshProUGUI timeDateDisplay;


    private void Start()
    {
        InvokeRepeating("UpdateTime", updateRepeatRate, updateRepeatRate);
    }

    private void UpdateTime()
    {
        currentMinute += 10;

        if (currentMinute >= 60)
        {
            currentHour++;
            currentMinute = 0;

            if (currentHour >= 12)
            {
                currentHour = 0;
            }
        }

        UpdateUIText();
    }

    private void UpdateUIText()
    {
        string amPm = currentHour < 12 ? "AM" : "PM";
        int displayHour = currentHour % 12 == 0 ? 12 : currentHour % 12;
        string displayString = "Fri. 13 - ";
        displayString += string.Format("{0:00}:{1:00} {2}", displayHour, currentMinute, amPm);
        timeDateDisplay.text = displayString;
    }
}
