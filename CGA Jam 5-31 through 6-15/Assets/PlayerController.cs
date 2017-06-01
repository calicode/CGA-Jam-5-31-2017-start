using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float defaultSpeedMultiplier = 1;
    public float currentSpeedMultiplier;
    public float maxSpeedMultipler = 10f;
    public float speedMultiplierIncrement = .0001f;

    private float baseSpeed = 1;
    private int maxLives = 5;
    private int currentLives;
    // Use this for initialization
    void Start()
    {
    }

    void ResetAfterDeath()
    {

        currentLives = maxLives;
        currentSpeedMultiplier = defaultSpeedMultiplier;
    }
    // Update is called once per frame

    void Update()
    {
        IncreaseSpeed();
        MovePlayer();
    }

    void IncreaseSpeed()
    {
        if (currentSpeedMultiplier <= maxSpeedMultipler) { currentSpeedMultiplier += speedMultiplierIncrement; }
    }

    void MovePlayer()
    {
        float moveAmount = Time.deltaTime + currentSpeedMultiplier;
        Debug.Log("Player v3 is" + transform.position.ToString());
        Debug.Log("Moveamoutn is " + moveAmount);
        transform.Translate(Vector3.right * moveAmount);
    }
}