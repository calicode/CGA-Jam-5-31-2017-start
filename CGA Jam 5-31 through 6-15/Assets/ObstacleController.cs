using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    public float explosionSpeed = 10f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public float ShouldIExplode(float impactSpeed)
    {
        if (impactSpeed >= explosionSpeed)
        {
            // play explosion animation

            Destroy(gameObject);
            return explosionSpeed;
        }
        return 0;

    }


}
