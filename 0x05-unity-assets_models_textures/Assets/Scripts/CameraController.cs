using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the control of the camera and follow the player
/// </summary>
public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPosition;
    Vector3 currentEulerAngles;
    private float _distanceCamPlayer = 6.25f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;
        currentEulerAngles = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = playerPosition - player.transform.position;
        playerPosition = player.transform.position;

        transform.position -= newPosition;

        currentEulerAngles += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.localEulerAngles = currentEulerAngles;

        transform.position = playerPosition - transform.forward * _distanceCamPlayer;
    }
}
