using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Catch if the vuforia tracked the image we want
/// </summary>
public class SearchTrack : MonoBehaviour
{
    // Fetch the Animator
    public Animator m_Animator;

    // Start is called
    void start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_Animator);
    }

    public void playAnimation()
    {
        m_Animator.SetBool("OnTracked", true);
    }
}
