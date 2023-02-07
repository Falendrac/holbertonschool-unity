using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using System;

/// <summary>
/// Handle all the game logic for the ammo
/// The drag and drop of the user
/// And the reset of the ammo when collisions is trigger
/// </summary>
public class AmmoHandler : MonoBehaviour
{
    // The position when the user drop the ammo
    private Vector3 endPosition;
    // To access to public methods and variable
    private GameHandle gameHandleScript;
    private LineRenderer line;
    // The plane selected by the user
    private ARPlane plane;
    // The rigdbody of the ammo
    private Rigidbody rb;
    // Catch start position when the player drag the ammo
    private Vector3 startPosition;
    // The main camera
    private Camera xrCamera;

    /// <summary>
    /// The offset of the ammo
    /// </summary>
    public Vector3 offset = new Vector3(0f, 0f, 0.3f);
    /// <summary>
    /// The strength of the ammo when the user drop it
    /// </summary>
    public float strength = 10f;

    // Start is called before the first frame update
    void Start()
    {
        xrCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        transform.parent = xrCamera.transform;
        transform.localPosition = offset;
        rb = GetComponent<Rigidbody>();
        gameHandleScript = GameObject.Find("AR Plane Manager").GetComponent<GameHandle>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < plane.transform.position.y)
        {
            Reset();
        }
    }

    // Return the position of the "mouse" of the user
    Vector3 GetMouseWorldPosition()
    {
        return xrCamera.ScreenToWorldPoint(Input.mousePosition + offset);
    }

    // Overwrite the OnMouseDown method when the user touch the ammo
    // save the world position of the ammo
    void OnMouseDown()
    {
        startPosition = GetMouseWorldPosition();
    }

    // Overwrite the OnMouseDrag method when the user touch and drag
    // the ammo, that change the position of the ammo in the screen
    void OnMouseDrag()
    {
        endPosition = GetMouseWorldPosition();
        transform.position = GetMouseWorldPosition();
        drawTrajectory((startPosition - endPosition).normalized * Vector3.Distance(startPosition, endPosition) * 10);
    }

    // Overwrite the OnMouseUp method when the user drop the ammo
    // to apply force and gravity in the ammo
    void OnMouseUp()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce((startPosition - endPosition).normalized * Vector3.Distance(startPosition, endPosition) * 10 * strength);
        line.enabled = false;
    }

    // Detect the collision with the ammo to reset it
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);
            gameHandleScript.playerScore += 10;
            gameHandleScript.targetCount--;
        }
        Reset();
    }

    // Reset the position and the rigidbody of the ammo
    // Destroy the gameobject if the player is out of ammo
    void Reset()
    {
        transform.localPosition = offset;
        rb.useGravity = false;
        rb.isKinematic = true;
        gameHandleScript.ammoCount--;
    }

    void drawTrajectory(Vector3 potentialForce)
    {
        float maxCurveLength = 5f;
        int maxSegments = 60;
        Vector3 progressWithoutGravity;
        Vector3 currentPosition = startPosition;
        List<Vector3> linePoints = new List<Vector3>();
        float timeOffset;

        line.enabled = true;
        linePoints.Clear();
        linePoints.Add(transform.position);

        for (int segment = 1; segment < maxSegments; segment++)
        {
            timeOffset = (maxCurveLength / maxSegments) * segment;
            progressWithoutGravity = potentialForce * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            currentPosition += progressWithoutGravity - gravityOffset;

            linePoints.Add(currentPosition);
        }

        line.positionCount = linePoints.Count;
        line.SetPositions(linePoints.ToArray());
    }

    /// <summary>
    /// Set the plane selected by the player
    /// Need to be used when the ammo is instantiate
    /// </summary>
    /// <param name="planeSelect">The plane selected by the player</param>
    public void setPlaneSelected(ARPlane planeSelect)
    {
        plane = planeSelect;
    }
}
