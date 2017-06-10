using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int pickupsInLevel;
    private int pickupsPickedUp = 0;

    public void IncreaseScoreAndUpdateView()
    {
        pickupsPickedUp++;
        scoreText.text = pickupsPickedUp.ToString() + "/" + pickupsInLevel;

    }


    // Use this for initialization
    void Start()
    {
        pickupsInLevel = GameObject.FindGameObjectsWithTag("Pickup").Length;
        scoreText.text = "0/" + pickupsInLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
