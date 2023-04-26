using System;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Decides what scripts are enabled depending on the mode.
/// i.e. If building, only have the building script enabled.
/// </summary>
public enum PlayerControllerMode {Selection, Building, Demolishing}

public class PlayerController : MonoBehaviour
{
    public PlayerControllerMode mode;
    public PlayerControllerMode defaultMode = PlayerControllerMode.Selection;
    
    [Header("Events")] 
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;

    /// <summary>
    /// Used for outside scripts
    /// </summary>
    [Header("Script References")]
    public PlayerSelectionScript playerSelectionScript;
    public PlayerBuildingScript playerBuildingScript;
    public PlayerDemolishingScript playerDemolishingScript;

    private void Start()
    {
        playerSelectionScript = GetComponent<PlayerSelectionScript>();
        playerBuildingScript = GetComponent<PlayerBuildingScript>();
        playerDemolishingScript = GetComponent<PlayerDemolishingScript>();
        mode = defaultMode;
        ToggleModeScripts();
    }

    private void Update()
    {
        // If player left clicks
        if (Input.GetMouseButtonDown(0))
        {
            onLeftClick?.Invoke();  // Run the left click event
        }
        // If player right clicks
        else if (Input.GetMouseButtonDown(1))
        {
            onRightClick?.Invoke();  // Run the right click event
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        // Keybind to disembark a unit.
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerSelectionScript.UnitDisembark();
        }
        
        // Keybind to toggle demolish mode.
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (mode != PlayerControllerMode.Demolishing)
            {
                ChangeMode(PlayerControllerMode.Demolishing);
            }
            else
            {
                ChangeMode(defaultMode);
            }
        }
    }
    
    
    /// <summary>
    /// Change the current mode of operation.
    /// </summary>
    /// <param name="newMode"></param>
    public void ChangeMode(PlayerControllerMode newMode)
    {
        mode = newMode;
        ToggleModeScripts();
    }

    /// <summary>
    /// Sets the scripts to be enabled depending on the mde.
    /// </summary>
    public void ToggleModeScripts()
    {
        playerBuildingScript.enabled = mode == PlayerControllerMode.Building;
        playerSelectionScript.enabled = mode == PlayerControllerMode.Selection;
        playerDemolishingScript.enabled = mode == PlayerControllerMode.Demolishing;
    }
}
