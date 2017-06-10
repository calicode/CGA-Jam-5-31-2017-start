using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinMe : MonoBehaviour
{

    float mouseX, mouseY;
    public Vector2 mousePos;
    public Vector3 screenPos;
    public Camera mainCam;



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()


    {


        mousePos = Input.mousePosition;
        screenPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - mainCam.transform.position.z));

        float newZ = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, newZ);
    }
}
