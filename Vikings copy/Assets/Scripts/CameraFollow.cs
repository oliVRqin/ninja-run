using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private float xVelocity = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 cameraposition = transform.position;

        // cameraposition.x = playerposition.x;
        if (playerposition.x > cameraposition.x) { 
            cameraposition.x = Mathf.SmoothDamp(cameraposition.x, playerposition.x, ref xVelocity, 0.5f);
        }

        transform.position = cameraposition;
    }
}
