using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Handle all interactions with sliders and buttons
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    void Start()
    {
        GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("yInverted") == 1) ? true : false;
        GameObject.Find("BGMSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeBGM");
    }
    /// <summary>
    /// Return to the main menu
    /// Use PlayerPref to get the index of the previous scene
    /// </summary>
    public void Back()
    {
        masterMixer.SetFloat("BGMVolume", LinearToDecibel(PlayerPrefs.GetFloat("volumeBGM")));
        SceneManager.LoadScene(PlayerPrefs.GetInt("PreviousScene"));
    }

    /// <summary>
    /// Apply current settings using the data en playerpref
    /// </summary>
    public void Apply()
    {
        PlayerPrefs.SetInt("yInverted", GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn ? 1 : 0);
        PlayerPrefs.SetFloat("volumeBGM", GameObject.Find("BGMSlider").GetComponent<Slider>().value);
    }

    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    public void changeBGMVolume()
    {
        masterMixer.SetFloat("BGMVolume", LinearToDecibel(GameObject.Find("BGMSlider").GetComponent<Slider>().value));
    }
}
