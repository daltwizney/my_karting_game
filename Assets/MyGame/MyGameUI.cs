using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyGameUI : MonoBehaviour
{
    public TextMeshProUGUI gameClockComponent;
    public TextMeshProUGUI gameScoreComponent;

    public Image packageGunEnabledImage;

    public Button unpauseButton;

    public TextMeshProUGUI remainingDeliveryAttemptsComponent;

    public GameObject pauseMenu;

    public GameObject gameOverMenu;

    private int _remainingDeliveryAttempts = -1;

    private int _score = -1;

    // Start is called before the first frame update
    void Start()
    {
        this.SetGameScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPauseMenu()
    {
        this.pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        this.pauseMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        this.gameOverMenu.SetActive(true);
    }

    public void SetPackageGunEnabled(bool enabled)
    {
        this.packageGunEnabledImage.enabled = enabled;
    }

    public void SetRemainingDeliveryAttempts(int value)
    {
        if (value == _remainingDeliveryAttempts)
        {
            return;
        }

        _remainingDeliveryAttempts = value;

        if (_remainingDeliveryAttempts == 0)
        {
            remainingDeliveryAttemptsComponent.text = "Remaining Delivery Attempts: 00";
        }
        else
        {
            remainingDeliveryAttemptsComponent.text = "Remaining Delivery Attempts: " + _remainingDeliveryAttempts.ToString("##");
        }

    }

    public void SetGameScore(int score)
    {
        if (_score == score)
        {
            return;
        }

        _score = score;

        if (_score == 0)
        {
            gameScoreComponent.text = "Score: 000";
        }
        else
        {
            gameScoreComponent.text = "Score: " + score.ToString("###");
        }
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
