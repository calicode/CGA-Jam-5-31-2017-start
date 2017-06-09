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
    private int score = 0;
    enum moveDirections { Down, Up };
    moveDirections currentDirection;

    private Camera mainCam;

    // Use this for initialization
    void Awake()
    {
        currentSpeedMultiplier = defaultSpeedMultiplier;
    }

    void Start()
    {
        currentDirection = moveDirections.Down;
        InvokeRepeating("IncreaseSpeed", 1, 1);
        mainCam = GameObject.FindObjectOfType<Camera>();

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
        Debug.Log("Score is" + score.ToString());
    }

    void IncreaseSpeed()
    {
        if (currentSpeedMultiplier <= maxSpeedMultipler)
        {
            currentSpeedMultiplier += speedMultiplierIncrement;

        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player collided");
        if (collider.gameObject.tag.Contains("Gravity")) { Debug.Log("reversing direction"); ReverseDirection(); currentSpeedMultiplier *= .5f; }

        if (collider.gameObject.tag.Contains("Pickup")) { score++; Destroy(collider.gameObject); }


    }

    void ReverseDirection()
    {
        if (currentDirection == moveDirections.Down) { currentDirection = moveDirections.Up; } else { currentDirection = moveDirections.Down; }

    }
    void MovePlayer()
    {

        Vector3 mousePos = (mainCam.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = 0;
        float mouseDiff = mousePos.x - transform.position.x;
        float moveAmount = currentSpeedMultiplier * Time.deltaTime;
        //float moveAmount = (Time.deltaTime * currentSpeedMultiplier);
        //Debug.Log("Player v3 is" + transform.position.ToString());
        //Debug.Log("Moveamoutn is " + moveAmount);
        switch (currentDirection)
        {
            case moveDirections.Down:

                transform.Translate(mouseDiff, -1 * moveAmount, 0);
                break;

            case moveDirections.Up:

                transform.Translate(mouseDiff, moveAmount, 0);
                break;

        }





    }
}