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
    // To access to public methods
    private GameHandle gameHandleScript;
    // Avoid to spam the tap
    private bool isPlaneSelected = false;
    // Plane Manager gameobject that in the scene of unity
    private GameObject planeManager;
    // AR Plane Manager component in the planeManager gameobject
    private ARPlaneManager planeManagerComponent;
    // Canvas of the start button
    private GameObject gameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        planeManager = GameObject.Find("AR Plane Manager");
        planeManagerComponent = planeManager.GetComponent<ARPlaneManager>();
        gameHandleScript = planeManager.GetComponent<GameHandle>();
        gameCanvas = GameObject.Find("GameCanvas");
    }

    // Overwrite OnMouseDown method to detect when the player tap on the screen
    // Disable ARPlaneManager and enable the gamecanvas
    // In line 51 that set inactive the searching text
    // In line 52 that set active the start button
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
            gameCanvas.transform.GetChild(0).gameObject.SetActive(false);
            gameCanvas.transform.GetChild(1).gameObject.SetActive(true);
            gameHandleScript.planeSelection(GetComponent<ARPlane>());
            gameHandleScript.targetInstantiation();
        }
    }
}
