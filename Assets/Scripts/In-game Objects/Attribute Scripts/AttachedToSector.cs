using System;
using UnityEngine;

public class AttachedToSector : MonoBehaviour
{
    [SerializeField] private SectorScript sectorAttachedTo;
    public bool CanDemolish;

    private void Start()
    {
        CenterOnSector();
        sectorAttachedTo = FindSectorBuiltOn();
    }
    
    /// <summary>
    /// Center the current object on a sector
    /// </summary>
    private void CenterOnSector() 
    {
        var position = GridSystem.Instance.CenterPosition(transform.position);
        transform.position = position;
    }
    
    /// <summary>
    /// Find the sector this object was placed on
    /// Checks if an object is already attached, if so, logs error and destroys itself.
    /// </summary>
    /// <returns></returns>
    private SectorScript FindSectorBuiltOn()
    {
        var sectorScript = GridSystem.Instance.GetGameObjectAtPos(
            transform.position, 
            GameManager.Instance.sectorLayerMask).GetComponent<SectorScript>();
        if (!sectorScript.CanAttachObject(this))
        {
            Debug.LogWarning("Cannot attach to this sector!");
            Destroy(gameObject);
            return null;
        }
        sectorScript.AttachObject(this);
        return sectorScript;
    }
    
    /// <summary>
    /// When run, this function will demolish the current gameobject.
    /// Will edit references as well to remove itself from attached objects.
    /// </summary>
    public void Demolish()
    {
        if (!CanDemolish) return;
        
        sectorAttachedTo.attachedObject = null;
        Destroy(gameObject);
    }
}