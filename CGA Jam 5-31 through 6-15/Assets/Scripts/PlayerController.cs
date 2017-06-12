using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeedMultiplier;
    public float maxSpeedMultipler = 15f;
    public float decelerationIncrement = .5f;
    public float speedPickupIncrease = 2f;


    public bool debugCheat = false;

    private float baseSpeed = 1;
    private bool playerImmune = false;


    private Camera mainCam;
    private ScoreManager scoreManager;



    /* 
DONE player starts at max speed but loses speed every second, 
DONE    pickups bring speed back up,
DONE    obstacles lower speed further. 
DONE IMMUNITY TIMER 
     DONE how does level end, goal line? 
     visual effect to communicate idea of blasting off to next planet, scale player sprite up and background/other stuff down
    DONE how does the player die? run out of time to exit so that will need a countdown timer. 
     paralax backround scrolling 

full level, 1 minute timer. 
try removing max speed limiter and instead halving speed on obstacle hit
     
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
        if (!debugCheat) { InvokeRepeating("DecreaseSpeed", 1, 1); }
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

    void SpeedIncrease(float amount)
    {
        if (currentSpeedMultiplier < maxSpeedMultipler)
        {
            currentSpeedMultiplier += amount;
        }


    }

    IEnumerator ImmunityTimer()
    {
        Debug.Log("Start immunity timer");
        playerImmune = true;
        // flash player sprite
        yield return new WaitForSeconds(2);
        playerImmune = false;
        // i think this can all be done on a co-routine something like set plkayer to immune, flash sprite, yield for a few seconds, and then remove immune flag
        Debug.Log("End Immunity timer");

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player collided");

        //        if (collider.gameObject.tag.Contains("Gravity")) { Debug.Log("reversing direction"); ReverseDirection(); currentSpeedMultiplier *= .5f; }

        if (collider.gameObject.tag.Contains("SpeedPickup")) { SpeedIncrease(speedPickupIncrease); Destroy(collider.gameObject); }
        if (collider.gameObject.tag.Contains("GoalLine")) { scoreManager.NextLevel(); }
        if (collider.gameObject.tag.Contains("Obstacle") && !playerImmune) { Debug.Log("hit thing reducing speed"); currentSpeedMultiplier -= speedPickupIncrease; StartCoroutine(ImmunityTimer()); }
    }


    void MovePlayer()
    {

        Vector3 mousePos = (mainCam.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = 0;
        float mouseDiff = mousePos.x - transform.position.x;
        float moveAmount = currentSpeedMultiplier * Time.deltaTime;
        Debug.Log("2 * mousdiff sign is " + (2 * Mathf.Sign(mouseDiff)).ToString());




        // i can probably remove direction switching as we just go into negative mouve amount

        transform.Translate(mouseDiff, moveAmount, 0);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -12, 12), transform.position.y);





    }
}