using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play a sound effect when button click
/// </summary>
public class SoundEffect : MonoBehaviour
{
    /// <summary>
    /// Sound of the button click
    /// </summary>
    public AudioSource soundClick;

    public void PlaySoundClick()
    {
        soundClick.Play();
    }
}
