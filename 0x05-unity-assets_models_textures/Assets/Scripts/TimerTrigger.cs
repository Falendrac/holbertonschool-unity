using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public GameObject playerTimer;

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerTimer.GetComponent<Timer>().enabled = true;
        }
    }
}
