using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using TMPro;
using System;

/// <summary>
/// Script for the AR plane Manager that store the selected plane
/// and handle the game logic
/// </summary>
public class GameHandle : MonoBehaviour
{
    // Ammo that the player can be use to touch target
    private GameObject ammo;
    // The plane selected by the user
    private static ARPlane planeSelected = null;

    /// <summary>
    /// Number of ammo allowed to the player
    /// </summary>
    public int ammoCount = 7;
    /// <summary>
    /// The ammo prefab we want to instantiate when the user start the game
    /// </summary>
    public GameObject ammoPrefab;
    /// <summary>
    /// The number of ammo display in the UI
    /// </summary>
    public TMP_Text ammoText;
    /// <summary>
    /// Number of target we want in the plane
    /// </summary>
    public int targetCount = 5;
    /// <summary>
    /// The target prefab we want to instantiate when the plane is selected
    /// </summary>
    public GameObject targetPrefab;
    /// <summary>
    /// The play again button display when out of ammo or out of target
    /// </summary>
    public GameObject playAgainButton;
    /// <summary>
    /// Score of the player, incremented when the player hit a target
    /// </summary>
    public int playerScore = 0;
    /// <summary>
    /// The score display in the UI
    /// </summary>
    public TMP_Text scoreText;
    /// <summary>
    /// The start button in the middle screen when the player select plane
    /// </summary>
    public GameObject startButton;
    /// <summary>
    /// The second canvas to display when the game start
    /// </summary>
    public GameObject startedCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerScore.ToString();
        ammoText.text = ammoCount.ToString();

        if (ammoCount == 0 || targetCount == 0)
        {
            playAgainButton.SetActive(true);
            Destroy(ammo);
        }
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

    /// <summary>
    /// Instantiate ammo when the user press the start button and
    /// display the final canvas for the game
    /// </summary>
    public void startGame()
    {
        ammoInstantiation();
        startButton.SetActive(false);
        startedCanvas.SetActive(true);
    }

    /// <summary>
    /// Reload the scene to find new plane for the player when the
    /// restart button is pressed
    /// </summary>
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit the application when the button exit is pressed
    /// </summary>
    public void exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Refill ammo, reinstantiate ammo and targets, reset score
    /// </summary>
    public void playAgain()
    {
        ammoCount = 7;
        targetCount = 5;
        playerScore = 0;

        GameObject[] targetDestroyer = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targetDestroyer)
            GameObject.Destroy(target);

        targetInstantiation();
        ammoInstantiation();
        playAgainButton.SetActive(false);
    }
}
