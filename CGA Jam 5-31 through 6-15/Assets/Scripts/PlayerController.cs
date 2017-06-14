using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    /* 
DONE player starts at max speed but loses speed every second, 
DONE    pickups bring speed back up,
DONE    obstacles lower speed further. 
DONE IMMUNITY TIMER 
     DONE how does level end, goal line? 
     visual effect to communicate idea of blasting off to next planet, scale player sprite up and background/other stuff down
    DONE how does the player die? run out of time to exit so that will need a countdown timer. 
     paralax backround scrolling 

     blow up obstacles if speed is high enough, score for blowing up more stuff, bigger obstacles have more hp 

full level, 1 minute timer. 
try removing max speed limiter and instead halving speed on obstacle hit
     
    autofiring maybe rate of fire based on player speed sure
     
     
     post jam - have npcs etc in background fighting, it can be animation to start with but a challenge would be implementing rudimentary ai so its different each Time


    */
    public float currentSpeedMultiplier;
    public float maxSpeedMultipler = 25f;
    float decelerationIncrement = .5f;
    float speedPickupIncrease = 1f;
    int immunityTime = 3;

    public bool debugCheat = false;

    private float baseSpeed = 1;
    private bool playerImmune = false;


    private Animator animator;
    private Camera mainCam;








    // Use this for initialization
    void Awake()
    {
        currentSpeedMultiplier = maxSpeedMultipler / 4;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("playerImmune");
        yield return new WaitForSeconds(immunityTime);
        playerImmune = false;
        Debug.Log("End Immunity timer");

    }

    void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.gameObject.tag.Contains("SpeedPickup")) { SpeedIncrease(speedPickupIncrease); Destroy(collider.gameObject); }


        if (collider.gameObject.tag.Contains("Obstacle") && !playerImmune)
        {
            Debug.Log("Hit obstacle at speed of" + currentSpeedMultiplier.ToString());
            ObstacleController obsControl = collider.GetComponent<ObstacleController>();
            float explodeResult = obsControl.ShouldIExplode(currentSpeedMultiplier);
            if (explodeResult > 0)
            {
                // score stuff
                Debug.Log("thing exploded for score of :" + explodeResult.ToString());
                currentSpeedMultiplier += speedPickupIncrease;
            }
            else
            {
                currentSpeedMultiplier -= speedPickupIncrease;
                transform.position = new Vector2(transform.position.x, (transform.position.y - 1));
                StartCoroutine(ImmunityTimer());
            }
        }
    }


    void MovePlayer()
    {

        Vector3 mousePos = (mainCam.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = 0;
        float mouseDiff = mousePos.x - transform.position.x;
        float moveAmount = currentSpeedMultiplier * Time.deltaTime;
        // Debug.Log("2 * mousdiff sign is " + (2 * Mathf.Sign(mouseDiff)).ToString());




        // i can probably remove direction switching as we just go into negative mouve amount

        transform.Translate(mouseDiff, moveAmount, 0);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -12, 12), transform.position.y);





    }
}