using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 50;
	public bool timerIsRunning = false;
	public Text timeText;
	public GameSetupController gameSetup;

	private void Start()
    {
        // Starts the timer automatically
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetupController>();
        timerIsRunning = true;
    }

    void Update()
    {
    	if (timerIsRunning)
        {
	        if (timeRemaining > 0)
	        {
	            timeRemaining -= Time.deltaTime;
	            DisplayTime(timeRemaining);
	        }
			else
			{
			    Debug.Log("Time has run out!");
			    timeRemaining = 0;
                timerIsRunning = false;
                gameSetup.roundFinished = true;
                gameSetup.MakeFinishButtonActive();
			}
        }
    }

    void DisplayTime(float timeToDisplay)
    {
    	timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}