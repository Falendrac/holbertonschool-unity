using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Handle the position and movement of the target
/// </summary>
public class TargetHandler : MonoBehaviour
{
    // The component ARPlane of the plane selected
    private ARPlane plane;
    // Destination of the target
    private Vector3 dest;
    private float threshold = 0.1f;

    /// <summary>
    /// The speed of targets in the plane
    /// </summary>
    public float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        plane = transform.parent.GetComponent<ARPlane>();
        dest = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(dest, transform.localPosition) < threshold)
        {
            int newRandomPos = Random.Range(0, plane.boundary.Length - 1);
            dest = new Vector3(plane.boundary[newRandomPos].x, 0, plane.boundary[newRandomPos].y);
        }

        transform.localPosition += new Vector3(speed * Time.deltaTime * (dest.x - transform.localPosition.x), 0, speed * Time.deltaTime * (dest.z - transform.localPosition.z));
    }
}
