using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Handle all the game logic for the ammo
/// The drag and drop of the user
/// And the reset of the ammo when collisions is trigger
/// </summary>
public class AmmoHandler : MonoBehaviour
{
    // The position when the user drop the ammo
    private Vector3 endPosition;
    // The plane selected by the user
    private ARPlane plane;
    // The rigdbody of the ammo
    private Rigidbody rb;
    // The main camera
    private Camera xrCamera;

    /// <summary>
    /// The offset of the ammo
    /// </summary>
    public Vector3 offset = new Vector3(0f, 0f, 0.3f);
    /// <summary>
    /// The strength of the ammo when the user drop it
    /// </summary>
    public float strength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        xrCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        transform.parent = xrCamera.transform;
        transform.localPosition = offset;
        rb = GetComponent<Rigidbody>();
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

    // Overwrite the OnMouseDrag method when the user touch and drag
    // the ammo, that change the position of the ammo in the screen
    void OnMouseDrag()
    {
        endPosition = GetMouseWorldPosition();
        transform.position = GetMouseWorldPosition();
    }

    // Overwrite the OnMouseUp method when the user drop the ammo
    // to apply force and gravity in the ammo
    void OnMouseUp()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce((offset - endPosition).normalized * strength);
    }

    // Detect the collision with the ammo to reset it
    void OnCollisionEnter(Collision collision)
    {
        Reset();
    }

    // Reset the position and the rigidbody of the ammo
    void Reset()
    {
        transform.localPosition = offset;
        rb.useGravity = false;
        rb.isKinematic = true;
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
