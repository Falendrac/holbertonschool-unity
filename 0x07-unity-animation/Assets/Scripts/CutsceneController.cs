using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject MainCamera;
    public GameObject TimerCanvas;
    Animator introAnimator;

    void AnimationEnd()
    {
        player.GetComponent<PlayerController>().enabled = true;
        MainCamera.SetActive(true);
        TimerCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        introAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (introAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            AnimationEnd();
        }
    }
}
