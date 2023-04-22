using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyGameUI : MonoBehaviour
{
    public TextMeshProUGUI gameClockComponent;
    public TextMeshProUGUI gameScoreComponent;

    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameScore(int score)
    {
        if (_score == score)
        {
            return;
        }

        _score = score;

        gameScoreComponent.text = "Score: " + score.ToString("###");
    }

    public void SetGameClockTime(string message, float time)
    {
        if (time < 0.0f)
        {
            time = 0.0f;
        }

        var ts = TimeSpan.FromSeconds(time);

        gameClockComponent.text = string.Format(message + " {0:00}.{1:00}", ts.Seconds, ts.Milliseconds);
    }

    public void SetPickupClockTime(float time)
    {
        this.SetGameClockTime("Time Remaining For Next Pickup:", time);
    }

    public void SetDeliveryClockTime(float time)
    {
        this.SetGameClockTime("Time Remaining For Next Delivery:", time);
    }
}
