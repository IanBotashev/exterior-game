using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }

    void Awake()
    {
        // Allows this class to be scene-wide, anyone can access it.
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }
    
    /// <summary>
    /// Gets the mouse position relative to the world 
    /// </summary>
    /// <returns>Returns mouse position coords in the scene.</returns>
    public Vector2 GetMouseWorldPosition()
    {
        // Get the position of the mouse but with actual coordinates on the scene.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    
    /// <summary>
    /// Gets a game object at position
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <param name="layerMask">Layer mask to filter on.</param>
    /// <returns></returns>
    public GameObject GetGameObjectAtPos(Vector2 position, LayerMask layerMask)
    {
        // Get raycast, applying a certain layer mark to it.
        var hit2D = Physics2D.Raycast(position, Vector3.forward, Mathf.Infinity, layerMask);
        // If it hits something, return the hit game object, otherwise, return null.
        return hit2D.collider != null ? hit2D.transform.gameObject : null;
    }

    /// <summary>
    /// Check if a position is within bounds, a.k.a. if it's on a sector.
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <returns>Returns a boolean value depending on if it's on a sector.</returns>
    public bool WithinBounds(Vector2 position)
    {
        var hit2D = GetGameObjectAtPos(position, GameManager.Instance.sectorLayerMask);
        return hit2D != null;
    }

    /// <summary>
    /// Returns a position centered on a sector
    /// Override, uses vector 2 instead
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2 CenterPosition(Vector2 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }
    
    /// <summary>
    /// Returns a position centered on a sector
    /// Override, uses vector 3 instead
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 CenterPosition(Vector3 position)
    {
        return new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), position.z);
    }
    
    /// <summary>
    /// Method to determine if the mouse pointer is currently over UI.
    /// </summary>
    /// <returns></returns>
    public bool MouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    
    /// <summary>
    /// Checks if a sector at a certain position have an attached object.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool DoesSectorHaveObjectAttached(Vector2 position)
    {
        return GetGameObjectAtPos(position, GameManager.Instance.sectorLayerMask).GetComponent<SectorScript>()
            .attachedObject != null;
    }

    /// <summary>
    /// Checks if you can build on a sector.
    /// Checks if there's already something there, and if the terrain type can have buildings.
    /// Also checks if the current government matches that of the sector.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="currentGovernment"></param>
    /// <returns></returns>
    public bool CanBuildOnSector(Vector2 position, GovernmentScript currentGovernment)
    {
        var sector = GetGameObjectAtPos(position, GameManager.Instance.sectorLayerMask).GetComponent<SectorScript>();
        Debug.Log(sector.GetComponent<GovernmentOwnership>().GetOwner() == currentGovernment);
        return sector.attachedObject == null && 
               sector.terrainType.canBuildOn && 
               sector.GetComponent<GovernmentOwnership>().GetOwner() == currentGovernment;
    }
}