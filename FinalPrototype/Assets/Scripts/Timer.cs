using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _maxTime = 300;
    private float _currTime = 0;
    Text _timerText;
    void Start()
    {
        _currTime = _maxTime;

        _timerText = GameObject.Find("TimerUIText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_currTime <= 0)
        {
            Application.Quit();
        }

        _currTime -= Time.deltaTime;

        // Convert remaining time to minutes and seconds format
        int minutes = Mathf.FloorToInt(_currTime / 60);
        int seconds = Mathf.FloorToInt(_currTime % 60);

        // Format the time as a string in MM:SS format
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        _timerText.text = timeText;
    }



}
