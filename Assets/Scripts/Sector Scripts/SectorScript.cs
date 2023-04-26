using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorScript : MonoBehaviour
{
    public AttachedToSector attachedObject;
    public TerrainType terrainType;
    [SerializeField] private SpriteRenderer borderHighlighter;
    [SerializeField] private GovernmentOwnership governmentOwnership;
    

    private void Start()
    {
        governmentOwnership = GetComponent<GovernmentOwnership>();
        SetTextureBasedOnTerrain();
    }

    public void OnNewOwner()
    {
        // If we can't be claimed, don't run this.
        if (!terrainType.CanBeClaimed) return;
        
        borderHighlighter.gameObject.SetActive(true);
        var color = governmentOwnership.GetOwner().countryColor;
        color.a = 0.7f;
        borderHighlighter.color = color;
    }
    
    
    /// <summary>
    /// Set new terrain and set the texture to that
    /// </summary>
    /// <param name="newTerrain"></param>
    public void SetTerrainType(TerrainType newTerrain)
    {
        terrainType = newTerrain;
        SetTextureBasedOnTerrain();
    }
    
    /// <summary>
    /// Make our texture be based on the terrain.
    /// </summary>
    public void SetTextureBasedOnTerrain()
    {
        GetComponent<SpriteRenderer>().sprite = terrainType.spriteTerrain;
    }
    
    /// <summary>
    /// Attach an object to this sector
    /// </summary>
    /// <param name="attachee"></param>
    public void AttachObject(AttachedToSector attachee)
    {
        if (!CanAttachObject(attachee)) return;

        attachedObject = attachee;
    }
    
    /// <summary>
    /// Check if an object can be attached to this object
    /// </summary>
    /// <param name="attachee"></param>
    /// <returns></returns>
    public bool CanAttachObject(AttachedToSector attachee)
    {
        return attachedObject == null && terrainType.canBuildOn;
    }
}
