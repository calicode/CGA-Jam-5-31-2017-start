using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text currentTimeDisplay;
    public Text bestTimeDisplay;
    private float bestTime;
    const string PLAYER_PREFS_KEY = "CGAJamBestTime";

    // Use this for initialization
    void Start()
    {
        bestTime = PlayerPrefs.GetFloat(PLAYER_PREFS_KEY, 0f);
        bestTimeDisplay.text = "Best Time: " + bestTime.ToString("F2");

    }
    public void RecordTime(float levelTime)
    {
        if (levelTime < bestTime) { PlayerPrefs.SetFloat(PLAYER_PREFS_KEY, levelTime); }

    }
    // Update is called once per frame
    void Update()
    {
        currentTimeDisplay.text = Time.timeSinceLevelLoad.ToString("F2");
    }
}
