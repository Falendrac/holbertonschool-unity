using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Script for the AR plane Manager that store the selected plane
/// and handle the game logic
/// </summary>
public class GameHandle : MonoBehaviour
{
    // The plane selected by the user
    private static ARPlane planeSelected = null;
    public int targetCount = 5;
    public GameObject targetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set planeSelected
    /// </summary>
    /// <param name="plane">The plane selected by the user</param>
    public void planeSelection(ARPlane plane)
    {
        planeSelected = plane;
    }

    /// <summary>
    /// Instantiate targets in the plane selected
    /// </summary>
    public void targetInstantiation()
    {
        for (int instantiation = 0; instantiation < targetCount; instantiation++)
        {
            GameObject targetInstatiate = Instantiate(targetPrefab, planeSelected.center + new Vector3(0, 0.05f, 0), Quaternion.identity);
            targetInstatiate.transform.parent = planeSelected.transform;
        }
    }
}
