using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GovernmentScript PlayerGovernment;
    public static PlayerController PlayerController { get; private set; }
    
    /// <summary>
    /// Easily accessible global layermask for the sector.
    /// </summary>
    public LayerMask sectorLayerMask;
    /// <summary>
    /// Easily accessible global layermask for selectable objects.
    /// </summary>
    public LayerMask selectableLayerMask;
    
    /// <summary>
    /// The parent object which all units should be under.
    /// </summary>
    public static Transform gameContainerParent;
    /// <summary>
    /// The parent object which all units should be under.
    /// Tag version, uses this to find the parent.
    /// </summary>
    public string gameContainerParentTag;
    
    /// <summary>
    /// The parent object which all sectors should be under.
    /// </summary>
    public static Transform sectorContainerParent;
    /// <summary>
    /// The parent object which all sectors should be under.
    /// Tag version, uses this to find the parent.
    /// </summary>
    public string sectorContainerParentTag;

    void Awake()
    {
        PlayerController = GameObject.FindObjectOfType<PlayerController>();
        gameContainerParent = GameObject.FindGameObjectWithTag(gameContainerParentTag).transform;
        sectorContainerParent = GameObject.FindGameObjectWithTag(sectorContainerParentTag).transform;
        
        // Allows this class to be scene-wide, anyone can access it.
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }
    
    /// <summary>
    /// Checks if a passed in object is a vehicle or not.
    /// </summary>
    /// <param name="gameObjectToCheck"></param>
    /// <returns></returns>
    public static bool IsObjectVehicle(GameObject gameObjectToCheck)
    {
        return gameObjectToCheck != null && gameObjectToCheck.GetComponent<VehicleScript>() != null;
    }
}
