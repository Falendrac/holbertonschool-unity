using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the buttons of the business card to open URL
/// </summary>
public class UrlOpener : MonoBehaviour
{
    /// <summary>
    /// Give the URL wanted to open in the Unity.
    /// </summary>
    public string Url;

    /// <summary>
    /// Open the URL given in Unity.
    /// </summary>
    public void Open()
    {
        Application.OpenURL(Url);
    }
}
