using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
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
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
}
