using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public Text currentTimeDisplay;
    public Text bestTimeDisplay;
    public Text escapeTimeDisplay;
    private float bestTime;
    const string PLAYER_PREFS_KEY = "CGAJamBestTime";
    private float escapeTime = 30f;

    // Use this for initialization
    void Start()
    {



        bestTime = PlayerPrefs.GetFloat(PLAYER_PREFS_KEY, 0f);

        UpdateBestTimeDisplay();
    }

    void UpdateBestTimeDisplay()
    {

        bestTimeDisplay.text = "Best Time: " + bestTime.ToString("F2");

    }
    public void RecordTime(float levelTime)
    {
        if (levelTime < bestTime) { PlayerPrefs.SetFloat(PLAYER_PREFS_KEY, levelTime); bestTime = levelTime; UpdateBestTimeDisplay(); }

    }
    // Update is called once per frame
    void Update()
    {
        float deltaBetweenTimes = (bestTime - Time.timeSinceLevelLoad);
        CheckEscapeTime();
        currentTimeDisplay.text = "Current Time: " + Time.timeSinceLevelLoad.ToString("F2") + "   (" + deltaBetweenTimes.ToString("F2") + ")";
    }


    void CheckEscapeTime()
    {
        float escapeDelta = escapeTime - Time.timeSinceLevelLoad;

        if (escapeDelta < 0) { GameOver(); } else { escapeTimeDisplay.text = "Time Left: " + escapeDelta.ToString("F2"); }


    }


    void GameOver()
    {
        Debug.Log("Gameover called");
        SceneManager.LoadSceneAsync("01_maingame");

    }

    public void NextLevel()
    {

        // do I want to handle zooming sprites here? 
        Debug.Log("Next level called");

    }

}
