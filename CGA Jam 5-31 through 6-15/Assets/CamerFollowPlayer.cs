using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 offset;
    private float cameraOffset = 5f;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - playerObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newX = (playerObject.transform.position.x + offset.x) - cameraOffset;
        transform.position = new Vector3(newX, transform.position.y, 0);
    }
}
