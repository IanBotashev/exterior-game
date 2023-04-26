using System;
using UnityEngine;

public enum EndGoal {Move, Embark}

public class UnitScript : MonoBehaviour
{
    public bool CanEmbark;
    [SerializeField] private float optimalMovementSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Vector2 moveTarget;
    [SerializeField] private VehicleScript vehicleTarget;
    [SerializeField] private GovernmentOwnership unitOwnership;
    [SerializeField] private EndGoal endGoal;
    private Vector3 worldPosition;
    private Vector3 oldPosition;
    private Vector3 originalScale; // Hack, vehiclescript embarking fucks with the scale for whatever reason, don't care enough to fix normally.

    private void Awake()
    {
        unitOwnership = GetComponent<GovernmentOwnership>();
        moveTarget = transform.position;
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (endGoal == EndGoal.Embark)
        {
            endGoal = EndGoal.Move;
            moveTarget = transform.position;
            transform.localScale = originalScale;
        }
    }

    private void Update()
    {
        // If we're not at our move target, move.
        if (moveTarget != GetTransformPositionVector2())
        {
            if (IsOnNewSector())
            {
                var sector = GetCurrentSector();
                SetSpeedHamper(sector);
                ExpandBorder(sector);
            }
            
            // If our end goal is to embark, and we can embark, embark.
            if (endGoal == EndGoal.Embark && vehicleTarget.CanEmbark(gameObject))
            {
                vehicleTarget.Embark(gameObject);
            }
            
            Move(moveTarget);
        }
    }

    /// <summary>
    /// Use the movetowards function to move to the MoveTarget
    /// </summary>
    /// <param name="target"></param>
    private void Move(Vector2 target)
    {
        
        // Method to move the current object to a certain target.
        transform.position = Vector2.MoveTowards(GetTransformPositionVector2(), target, movementSpeed * Time.deltaTime);
        // Putting this here as we need to do this as it goes on, since it's a lerp.
        TurnTowardsPoint(moveTarget);
    }
    
    /// <summary>
    /// Transform.position with just a function. Turn into a vector2.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetTransformPositionVector2()
    {
        var position = transform.position;
        return new Vector2(position.x, position.y);
    }

    /// <summary>
    /// Set a move target for the unit, so it moves there.
    /// Checks also if it's within bounds or not.
    /// Also sets the end goal, are we trying to embark a vehicle?
    /// </summary>
    /// <param name="newMoveTarget"></param>
    /// <param name="newEndGoal"></param>
    public void SetMoveTarget(Vector2 newMoveTarget, EndGoal newEndGoal)
    {
        // Make sure we're enabled before making a move target.
        if (!this.enabled) return;
        
        if (GridSystem.Instance.WithinBounds(newMoveTarget))
        {
            // Get a new target to move to.
            moveTarget = newMoveTarget;
            endGoal = newEndGoal;
        }
        else
        {
            Debug.LogError("Proposed move target not within the game board.");
        }
    }
    
    /// <summary>
    /// Gradually turns object towards a set point.
    /// </summary>
    /// <param name="point"></param>
    public void TurnTowardsPoint(Vector2 point)
    {
        transform.up = Vector3.Lerp(transform.up, (point - (Vector2)transform.position), turnSpeed);
    }
    

    /// <summary>
    /// When we're selected, and right clicked on something, call this method.
    /// </summary>
    public void OnRightClick()
    {
        var mousePos = GridSystem.Instance.GetMouseWorldPosition();
        EndGoal goal = EndGoal.Move;
        
        // Check if this unit can embark, AND if the target we clicked on is an actual vehicle.
        if (IsTargetVehicle(mousePos) && CanEmbark)
        {
            goal = EndGoal.Embark;
            vehicleTarget = GridSystem.Instance.GetGameObjectAtPos(mousePos, GameManager.Instance.selectableLayerMask)
                .GetComponent<VehicleScript>();
        }
        
        // This goes into the selectable object script. Basically just makes the unit move to the place where the mouse is.
        SetMoveTarget(mousePos, goal);
    }
    
    /// <summary>
    /// Checks if a target position is on a vehicle.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsTargetVehicle(Vector2 target)
    {
        var targetObject = GridSystem.Instance.GetGameObjectAtPos(target, GameManager.Instance.selectableLayerMask);
        return targetObject != null && targetObject.GetComponent<VehicleScript>() != null;
    }

    
    /// <summary>
    /// Checks if we are crossing the border to a new sector.
    /// Basically, stores an our centered position from the last frame, then checks if it's equal to that
    /// of the new centered position.
    /// If not, we are on a new sector.
    /// </summary>
    private bool IsOnNewSector()
    {
        var position = transform.position;
        bool returnval = GridSystem.Instance.CenterPosition(position) != oldPosition;

        oldPosition =  GridSystem.Instance.CenterPosition(position);
        return returnval;
    }
    
    /// <summary>
    /// Sets the speed hamper from a sector.
    /// </summary>
    /// <param name="sector"></param>
    private void SetSpeedHamper(SectorScript sector)
    {
        movementSpeed = sector.terrainType.movementHamper * optimalMovementSpeed;
    }
    
    /// <summary>
    /// Gets current sector we're on and tries to claim it if it's not already ours.
    /// </summary>
    private void ExpandBorder(SectorScript sector)
    {
        var ownership = sector.GetComponent<GovernmentOwnership>();
        
        // If we don't control this sector
        if (ownership.GetOwner() != unitOwnership.GetOwner())
        {
            // Set the owner to be our own.
            ownership.SetOwner(unitOwnership.GetOwner());
        }
    }
    
    /// <summary>
    /// Gets the current sector this unit is on.
    /// </summary>
    /// <returns></returns>
    private SectorScript GetCurrentSector()
    {
        return GridSystem.Instance.GetGameObjectAtPos(transform.position, GameManager.Instance.sectorLayerMask).GetComponent<SectorScript>();
    }
}
