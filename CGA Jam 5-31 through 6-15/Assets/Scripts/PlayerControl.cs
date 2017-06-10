using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public ScoreManager scoreManager;
    void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log("Hit trigger");

        if (collider.tag == "Exit")
        {

            scoreManager.RecordTime(Time.timeSinceLevelLoad);
            SceneManager.LoadScene("01_maingame");


            Debug.Log("Hit exit collider");
        }
    }


}
