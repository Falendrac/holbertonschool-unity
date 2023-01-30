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
    // Ammo that the player can be use to touch target
    private GameObject ammo;

    /// <summary>
    /// Number of target we want in the plane
    /// </summary>
    public int targetCount = 5;
    /// <summary>
    /// The target prefab we want to instantiate when the plane is selected
    /// </summary>
    public GameObject targetPrefab;
    /// <summary>
    /// The ammo prefab we want to instantiate when the user start the game
    /// </summary>
    public GameObject ammoPrefab;

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

    /// <summary>
    /// Instantiate ammo when the player start
    /// </summary>
    public void ammoInstantiation()
    {
        ammo = Instantiate(ammoPrefab);
        ammo.GetComponent<AmmoHandler>().setPlaneSelected(planeSelected);
    }
}
