using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour
{
    public List<GameObject> passengers = new List<GameObject>();
    public int capacity;
    public float maxEmbarkDistance;
    [SerializeField] private UnitScript unitScript;

    private void Start()
    {
        unitScript = GetComponent<UnitScript>();
        SetMoveAbility();
    }

    /// <summary>
    /// Public function that embarks a gameobject.
    /// 
    /// </summary>
    /// <param name="embarkingObject"></param>
    public void Embark(GameObject embarkingObject)
    {
        if (CanEmbark(embarkingObject))
        {
            embarkingObject.SetActive(false);
            embarkingObject.transform.parent = transform;
            passengers.Add(embarkingObject);
            SetMoveAbility();
        }
        else
        {
            Debug.LogError("Object trying to embark when not allowed to!");
        }
    }
    
    /// <summary>
    /// Disembarks a unit from the vehicle.
    /// </summary>
    /// <param name="disembarkingObject"></param>
    public void Disembark(GameObject disembarkingObject)
    {
        // Checking to see if we actually have that unit
        if (!passengers.Contains(disembarkingObject))
        {
            Debug.LogError("Trying to disembark a unit that's not embarked");
        }
        // Set the units position to ours.
        disembarkingObject.transform.parent = GameManager.gameContainerParent;
        disembarkingObject.gameObject.SetActive(true);
        passengers.Remove(disembarkingObject);
        SetMoveAbility();
    }
    
    /// <summary>
    /// Function to ask the vehicle if a unit can embark.
    /// </summary>
    /// <param name="embarkingObject"></param>
    /// <returns></returns>
    public bool CanEmbark(GameObject embarkingObject)
    {
        var unit = embarkingObject.GetComponent<UnitScript>();
        return passengers.Count + 1 <= capacity &&
               unit != null &&
               Vector2.Distance(transform.position, embarkingObject.transform.position) <= maxEmbarkDistance &&
               unit.CanEmbark;
    }
    
    /// <summary>
    /// Checks if we have passengers.
    /// If so, turns on the unit script.
    /// Otherwise, turns it off.
    /// </summary>
    private void SetMoveAbility()
    {
        unitScript.enabled = passengers.Count > 0;
    }
}
