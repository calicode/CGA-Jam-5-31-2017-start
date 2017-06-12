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
    private bool playerImmune = false;


    enum moveDirections { Down, Up };
    moveDirections currentDirection;

    private Camera mainCam;
    private ScoreManager scoreManager;



    /* 
DONE player starts at max speed but loses speed every second, 
DONE    pickups bring speed back up,
DONE    obstacles lower speed further. 
DONE IMMUNITY TIMER 
     how does level end, goal line? visual effect to communicate idea of blasting off to next planet, scale player sprite up and background/other stuff down
    DONE how does the player die? run out of time to exit so that will need a countdown timer. 
     paralax backround scrolling 
     
     
    autofiring maybe rate of fire based on player speed sure
     
     
     post jam - have npcs etc in background fighting, it can be animation to start with but a challenge would be implementing rudimentary ai so its different each Time


    */






    // Use this for initialization
    void Awake()
    {
        currentSpeedMultiplier = maxSpeedMultipler / 2;
    }

    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        currentDirection = moveDirections.Up;
        InvokeRepeating("DecreaseSpeed", 1, 1);
        mainCam = GameObject.FindObjectOfType<Camera>();

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

    IEnumerator ImmunityTimer()
    {
        playerImmune = true;
        // flash player sprite
        yield return new WaitForSeconds(2);
        playerImmune = false;
        // i think this can all be done on a co-routine something like set plkayer to immune, flash sprite, yield for a few seconds, and then remove immune flag


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player collided");

        //        if (collider.gameObject.tag.Contains("Gravity")) { Debug.Log("reversing direction"); ReverseDirection(); currentSpeedMultiplier *= .5f; }

        if (collider.gameObject.tag.Contains("SpeedPickup")) { currentSpeedMultiplier += speedPickupIncrease; Destroy(collider.gameObject); }
        if (collider.gameObject.tag.Contains("Obstacle") && !playerImmune) { currentSpeedMultiplier -= speedPickupIncrease; StartCoroutine(ImmunityTimer()); }
        if (collider.gameObject.tag.Contains("GoalLine")) { scoreManager.NextLevel(); }
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