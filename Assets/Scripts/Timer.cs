using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Public Var

    // Private Var
    private float timeLeft;
    private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Math.Ceiling(timeLeft);
            if(timeLeft < 0 && timeLeft > -1) {
                // Game Over Logic;
                Debug.Log("Game is over");
            }
        }


    }

    public void StartTimer(float time) {
        timeLeft = time;
    }
}
