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
    private GameHandle gameHandleScript;
    // Avoid to spam the tap
    private bool isPlaneSelected = false;
    // Plane Manager gameobject that in the scene of unity
    private GameObject planeManager;
    // AR Plane Manager component in the planeManager gameobject
    private ARPlaneManager planeManagerComponent;
    // Canvas of the start button
    private GameObject startCanvas;

    // Start is called before the first frame update
    void Start()
    {
        planeManager = GameObject.Find("AR Plane Manager");
        planeManagerComponent = planeManager.GetComponent<ARPlaneManager>();
        gameHandleScript = planeManager.GetComponent<GameHandle>();
        startCanvas = GameObject.Find("StartCanvas");
    }

    // Overwrite OnMousDown method to detect when the player tap on the screen
    // Disable ARPlaneManager and enable the startcanvas
    void OnMouseDown()
    {
        if (!isPlaneSelected)
        {
            isPlaneSelected = true;

            if (planeManagerComponent.enabled)
            {
                foreach (var plane in planeManagerComponent.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                planeManagerComponent.enabled = false;
            }

            gameObject.SetActive(true);
            startCanvas.GetComponent<Canvas>().enabled = true;
            gameHandleScript.planeSelection(GetComponent<ARPlane>());
            gameHandleScript.targetInstantiation();
        }
    }
}
