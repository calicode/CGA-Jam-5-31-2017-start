using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour
{


    public Camera mainCam;

    float zAngle = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()


    {
        Vector3 mousePos = (mainCam.ScreenToWorldPoint(Input.mousePosition));
        Vector3 eul = transform.eulerAngles;
        float angleBetween = Vector3.Angle(mousePos, eul);
        float otherAngle = Vector3.Angle(Input.mousePosition, eul);

        //        transform.eulerAngles = new Vector3(0, 0, mousePos.);
        //        zAngle += .5f;
    }
}
