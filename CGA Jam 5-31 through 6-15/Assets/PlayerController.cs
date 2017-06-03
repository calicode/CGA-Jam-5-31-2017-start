using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float defaultSpeedMultiplier = 1f;
    public float currentSpeedMultiplier;
    public float maxSpeedMultipler = 10f;
    public float speedMultiplierIncrement = 1.5f;

    private float baseSpeed = 1;
    private int maxLives = 5;
    private int currentLives;
    // Use this for initialization
    void Awake()
    {
        currentSpeedMultiplier = defaultSpeedMultiplier;
    }

    void Start()
    {

        InvokeRepeating("IncreaseSpeed", 1, 1);
    }

    void ResetAfterDeath()
    {

        currentLives = maxLives;
        currentSpeedMultiplier = defaultSpeedMultiplier;
    }
    // Update is called once per frame

    void Update()
    {
        MovePlayer();
    }

    void IncreaseSpeed()
    {
        if (currentSpeedMultiplier <= maxSpeedMultipler)
        {
            currentSpeedMultiplier += speedMultiplierIncrement;
            Debug.Log("Current speed multi is" + currentSpeedMultiplier);

        }
    }

    void MovePlayer()
    {
        //float moveAmount = (Time.deltaTime * currentSpeedMultiplier);
        //Debug.Log("Player v3 is" + transform.position.ToString());
        //Debug.Log("Moveamoutn is " + moveAmount);
        transform.Translate(currentSpeedMultiplier * Time.deltaTime, 0, 0);
    }
}