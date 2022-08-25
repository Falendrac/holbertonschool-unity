using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = playerPosition - player.transform.position;
        playerPosition = player.transform.position;
        newPosition.y = 0;

        transform.position -= newPosition;
    }
}
