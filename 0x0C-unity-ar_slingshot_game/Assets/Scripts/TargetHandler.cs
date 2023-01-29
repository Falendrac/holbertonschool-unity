using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Handle the position and movement of the target
/// </summary>
public class TargetHandler : MonoBehaviour
{
    public float speed = 1.0f;
    private float threshold = 0.1f;
    private ARPlane plane;
    private Vector3 originDest;

    // Start is called before the first frame update
    void Start()
    {
        plane = transform.parent.GetComponent<ARPlane>();
        originDest = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(originDest, transform.localPosition) < threshold)
        {
            int newRandomPos = Random.Range(0, plane.boundary.Length - 1);
            originDest = new Vector3(plane.boundary[newRandomPos].x, 0, plane.boundary[newRandomPos].y);
        }

        transform.localPosition += new Vector3(speed * Time.deltaTime * (originDest.x - transform.localPosition.x), 0, speed * Time.deltaTime * (originDest.z - transform.localPosition.z));
    }
}
