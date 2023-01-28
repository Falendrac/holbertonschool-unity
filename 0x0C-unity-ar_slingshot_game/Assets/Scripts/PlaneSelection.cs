using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Handle the plane selection for the user
/// and store the plane selected in a static variable
/// </summary>
public class PlaneSelection : MonoBehaviour
{
    // Plane Manager need to be disable
    private ARPlaneManager planeManager;
    // Current plane selected
    private static ARPlane planeSelected;
    // Canvas of the start button
    private GameObject startCanvas;
    // Avoid to spam the tap
    private bool isPlaneSelected = false;


    // Start is called before the first frame update
    void Start()
    {
        planeManager = GameObject.Find("AR Plane Manager").GetComponent<ARPlaneManager>();
        planeSelected = GetComponent<ARPlane>();
        startCanvas = GameObject.Find("StartCanvas");
    }

    // Overwrite OnMousDown method to detect when the player tap on the screen
    // Disable ARPlaneManager and enable the startcanvas
    void OnMouseDown()
    {
        if (!isPlaneSelected)
        {
            isPlaneSelected = true;

            if (planeManager.enabled)
            {
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                planeManager.enabled = false;
            }

            gameObject.SetActive(true);
            startCanvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
