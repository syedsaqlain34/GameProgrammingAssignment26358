using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour
{

    public float FallowSpeed = 2f;
    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FallowSpeed * Time.deltaTime);
    }
}
