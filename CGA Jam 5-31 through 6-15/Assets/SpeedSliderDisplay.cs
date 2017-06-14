using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSliderDisplay : MonoBehaviour
{
    Slider slider;
    GameObject player;
    PlayerController playerController;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        slider = GameObject.FindObjectOfType<Slider>();
        playerController = player.GetComponent<PlayerController>();
        Debug.Log(slider.ToString());
        slider.maxValue = playerController.maxSpeedMultipler;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerController.currentSpeedMultiplier;

    }
}
