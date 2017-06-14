using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleController : MonoBehaviour
{
    SpriteRenderer spriteRender;
    public float explosionSpeed = 5f;
    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
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


            StartCoroutine(FadeSprite());
            return explosionSpeed;
        }
        return 0;

    }

    IEnumerator FadeSprite()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color col = spriteRender.color;
            col.a = f;
            spriteRender.color = col;
            yield return null;

        }
        Destroy(gameObject);
    }


}
