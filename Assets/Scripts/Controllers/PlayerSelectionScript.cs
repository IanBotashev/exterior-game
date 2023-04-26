using System;
using UnityEngine;

public class PlayerSelectionScript : PlayerControlModule
{
    [SerializeField] private SelectableObject currentlySelected;
    [SerializeField] private GovernmentOwnership owner;

    private void Start()
    {
        owner = gameObject.GetComponent<GovernmentOwnership>();
    }

    /// <summary>
    /// If this method is called that means we left clicked.
    /// Tries to find and select any selectable objects at the current position of the mouse.
    /// </summary>
    public void TryToSelect()
    {
        if (!CanRun())
        {
            return;
        }
        
        var gottenObject = GridSystem.Instance.GetGameObjectAtPos(
            GridSystem.Instance.GetMouseWorldPosition(),  // Check mouse pos for an object
            GameManager.Instance.selectableLayerMask  // Only get selectables
            );
            
        // If we have something selected, since we tried to select something, deselect others.
        if (currentlySelected != null)
        {
            currentlySelected.Unselect();
            currentlySelected = null; 
        }

        // If we actually hit something, add it's object to the currentlySelected var.
        // Also check if it's our own unit. If it's not, don't go for it.
        if (gottenObject == null ||
            gottenObject.GetComponent<GovernmentOwnership>().GetOwner() != GameManager.Instance.PlayerGovernment) return;
        
        currentlySelected = gottenObject.GetComponent<SelectableObject>();
        currentlySelected.Select();
    }
    
    /// <summary>
    /// Right click event lol
    /// Runs the right click event on all selected objects.
    /// </summary>
    public void OnRightClick()
    {
        if (!CanRun())
        {
            return;
        }
        
        if (currentlySelected != null)
        {
            currentlySelected.RightClicked();
        }
    }
    
    /// <summary>
    /// Tries to disembark the first unit in a vehicle.
    /// </summary>
    public void UnitDisembark()
    {
        // If our current object is not a vehicle, ignore.
        if (!GameManager.IsObjectVehicle(currentlySelected.gameObject)) Debug.LogError("Trying to disembark while not having a vehicle selected!");
        
        var vehicle = currentlySelected.GetComponent<VehicleScript>();
        vehicle.Disembark(vehicle.passengers[0]);
    }
}