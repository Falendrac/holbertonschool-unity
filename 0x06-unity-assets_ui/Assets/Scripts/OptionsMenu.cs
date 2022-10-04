using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handle all interactions with sliders and buttons
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Return to the main menu
    /// Use PlayerPref to get the index of the previous scene
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("PreviousScene"));
    }
}
