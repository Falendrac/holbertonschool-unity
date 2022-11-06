using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// Handle all functionnality of pausemenu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>Get the GameObject pauseCanvas to setactive</summary>
    public GameObject pauseCanvas;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    /// <summary>
    /// Activate the pause menu when the player press ESC
    /// </summary>
    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    /// <summary>
    /// Resume the game when the player press ESC or the resume button
    /// </summary>
    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    /// <summary>
    /// Restart the current level
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Load the MainMenu
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Load the options
    /// </summary>
    public void Options()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                paused.TransitionTo(.001f);
                Pause();
            }
            else
            {
                unpaused.TransitionTo(.01f);
                Resume();
            }
        }
    }
}
