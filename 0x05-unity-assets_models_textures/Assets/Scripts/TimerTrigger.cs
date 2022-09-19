using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Triger the timer when player move</summary>
public class TimerTrigger : MonoBehaviour
{
    // Get the player gameobject to set timer to false
    public GameObject playerTimer;

    // When the player move, enable the timer
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerTimer.GetComponent<Timer>().enabled = true;
        }
    }
}
