using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeedMultiplier;
    public float maxSpeedMultipler = 15f;
    public float decelerationIncrement = .5f;
    public float speedPickupIncrease = 2f;




    private float baseSpeed = 1;
    private int maxLives = 5;
    private int currentLives;
    private int score = 0;



    enum moveDirections { Down, Up };
    moveDirections currentDirection;

    private Camera mainCam;



    // player starts at max speed but loses speed every second, pickups bring speed back up, obstacles lower speed further. beat time, autofiring maybe rate of fire based on player speed sure






    // Use this for initialization
    void Awake()
    {
        currentSpeedMultiplier = maxSpeedMultipler / 2;
        currentLives = maxLives;
    }

    void Start()
    {

        currentDirection = moveDirections.Up;
        InvokeRepeating("DecreaseSpeed", 1, 1);
        mainCam = GameObject.FindObjectOfType<Camera>();

    }

    void ResetAfterDeath()
    {

        currentLives--;
        currentSpeedMultiplier = maxSpeedMultipler;
    }
    // Update is called once per frame

    void Update()
    {
        MovePlayer();
    }

    void DecreaseSpeed()
    {

        currentSpeedMultiplier -= decelerationIncrement;

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player collided");

        //        if (collider.gameObject.tag.Contains("Gravity")) { Debug.Log("reversing direction"); ReverseDirection(); currentSpeedMultiplier *= .5f; }

        if (collider.gameObject.tag.Contains("SpeedPickup")) { currentSpeedMultiplier += speedPickupIncrease; Destroy(collider.gameObject); }


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
                transform.Translate(mouseDiff, moveAmount, 0);
                if (currentSpeedMultiplier > baseSpeed) { Debug.Log("Reversing directions from down to up"); ReverseDirection(); }
                break;

            case moveDirections.Up:

                transform.Translate(mouseDiff, moveAmount, 0);
                if (currentSpeedMultiplier <= baseSpeed) { Debug.Log("Reversing directions from up to down"); ReverseDirection(); }
                break;

        }





    }
}