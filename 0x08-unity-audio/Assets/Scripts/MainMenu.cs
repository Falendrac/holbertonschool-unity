using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load the level scene when the player click in the level button
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Load the level selected by the player
    /// </summary>
    /// <param name="level">The level selected</param>
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene("Level0" + level.ToString());
    }

    /// <summary>
    /// Load options menu
    /// Store the current index of the scene in a playerpref
    /// </summary>
    public void Options()
    {
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Options");
    }
}
